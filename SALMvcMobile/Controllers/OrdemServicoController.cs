using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using SALClassLib.OS.Model;
using SALClassLib.OS.Model.BO;
using SALMvcMobile.Helpers;
using SALMvcMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SALMvcMobile.Controllers
{
    public class OrdemServicoController : Controller
    {
        //
        // GET: /OrdemServico/

        public ActionResult Index()
        {
            Entregador entregador = (Entregador) LoginHelper.GetUsuarioLogado(this);
            OrdemServico os = null;

            using(OrdemServicoBO bo = new OrdemServicoBO())
            {
                os = bo.BuscarOSDoEntregador(entregador);
            }

            if (os != null)
            {
                if (os.Status.Id == (ushort)EStatusOS.EmRetirada)
                    TempData["botao"] = "A caminho da entrega";
                else if (os.Status.Id == (ushort)EStatusOS.ACaminhoDaEntrega)
                    TempData["botao"] = "Entregue no destino";
            }

            return View(os);
        }

        //
        // POST: /OrdemServico/

        [HttpPost]
        public ActionResult Index(OrdemServico os)
        {
            if (Request.Params["VerificarRota"] != null)
            {
                Entregador entregador = (Entregador)LoginHelper.GetUsuarioLogado(this);
                OrdemServico ordemServico = null;

                using (OrdemServicoBO bo = new OrdemServicoBO())
                {
                    ordemServico = bo.BuscarOSDoEntregador(entregador);
                }

                if(ordemServico == null) return RedirectToAction("Index");

                Session["OSRota"] = ordemServico;

                return Redirect("~/OrdemServico/VerificarRota");
            }

            using (OrdemServicoBO bo = new OrdemServicoBO())
            {
                if (os.Status.Id == (ushort)EStatusOS.EmRetirada)
                    bo.AlterarStatusParaACaminhoDaEntrega(os);
                else if (os.Status.Id == (ushort)EStatusOS.ACaminhoDaEntrega)
                    bo.AlterarStatusParaFinalizada(os);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AtribuirOS(String geolocation)
        {
            try
            {
                Entregador entregador = null;
                Pessoa p = LoginHelper.GetUsuarioLogado(this);
                if (p is Entregador)
                {
                    entregador = (Entregador)p;
                }
                else
                    return Redirect("~/Login/Index");
                
                IList<OrdemServico> ordensDisponiveis = null;
                using (OrdemServicoBO bo = new OrdemServicoBO())
                {
                    ordensDisponiveis = bo.BuscarOSsDisponiveisParaEntregadores();
                }

                MatrizDistancia mdis = new MatrizDistancia();
                mdis.Origem = geolocation;

                mdis.Destinos = new List<EnderecoMatrizDistancia>();
                foreach (var item in ordensDisponiveis)
                {
                    EnderecoMatrizDistancia e = new EnderecoMatrizDistancia(item.EnderecoRetirada);
                    e.OrdemServico = item;
                    mdis.Destinos.Add(e);
                }
                
                EnderecoMatrizDistancia maisProximo = mdis.GetDestinoMaisProximo();

                using (EntregadorBO bo = new EntregadorBO())
                {
                    bo.AtribuirOSAoEntregador(maisProximo.OrdemServico, entregador);
                }
            }
            catch (Exception ex)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            return RedirectToAction("Index");
        }

        public ActionResult VerificarRota()
        {
            OrdemServico os = (OrdemServico)Session["OSRota"];
            VerificarRotaModel model = new VerificarRotaModel();

            if (os.Status.Id == (ushort)EStatusOS.EmRetirada)
                model.EnderecoDestino = os.EnderecoRetirada.getEndereco();
            else if (os.Status.Id == (ushort)EStatusOS.ACaminhoDaEntrega)
                model.EnderecoDestino = os.EnderecoEntrega.getEndereco();

            return View(model);
        }
    }
}
