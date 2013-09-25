using SALClassLib.Masterdata.Model;
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
                IList<OrdemServico> ordensDisponiveis = null;
                using (OrdemServicoBO bo = new OrdemServicoBO())
                {
                    ordensDisponiveis = bo.BuscarOSsDisponiveisParaEntregadores();
                }

                MatrizDistancia mdis = new MatrizDistancia();
                
                EnderecoMatrizDistancia maisProximo = mdis.GetDestinoMaisProximo();
            }
            catch (Exception)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            return View();
        }
    }
}
