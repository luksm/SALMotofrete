using NHibernate;
using SALClassLib.OS.Model;
using SALClassLib.OS.Model.BO;
using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilitarios.BO;
using Utilitarios.JSON;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using SALMvc.Models;
using Newtonsoft.Json.Linq;

namespace SALMvc.Controllers
{
    public class OrdemServicoController : Controller
    {

        IList<OrdemServico> lista = new List<OrdemServico>();
        private void Listar()
        {
            OrdemServicoBO bo = new OrdemServicoBO();
            lista = bo.Listar();
            bo.Dispose();
        }

        private void PreencherBagDropDownLists()
        {
            EstadoBO boe = new EstadoBO();
            IList<Estado> estados = boe.Listar();
            boe.Dispose();
            boe = null;
            var listaEstados = new List<SelectListItem>();
            foreach (var item in estados)
            {
                listaEstados.Add(
                    new SelectListItem()
                    {
                        Text = item.Sigla,
                        Value = item.Id.ToString()
                    }
                );
            }
            ViewBag.Estados = listaEstados;

            MunicipioBO bom = new MunicipioBO();
            IList<Municipio> municipios = bom.Listar();
            bom.Dispose();
            bom = null;
            var listaMunicipios = new List<SelectListItem>();
            foreach (var item in municipios)
            {
                listaMunicipios.Add(
                    new SelectListItem()
                    {
                        Text = item.Nome,
                        Value = item.Id.ToString()
                    }
                );
            }
            ViewBag.Municipios = listaMunicipios;
        }

        //
        // GET: /OrdemServico/
        public ActionResult Index()
        {
            Listar();
            return View(lista);
        }

        //
        // GET: /OrdemServico/
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OSEnderecoModel osModel)
        {
            String sJson = GMaps.getGeocode(osModel.EnderecoRetirada.Logradouro);
            JToken token = JObject.Parse(sJson);

            EnderecoRetirada end1 = new EnderecoRetirada(token);
            end1.NomeContato = osModel.EnderecoRetirada.NomeContato;
            end1.TelefoneContato = osModel.EnderecoRetirada.TelefoneContato;

            osModel.EnderecoRetirada = end1;

            EnderecoEntrega end2 = new EnderecoEntrega(token);
            end2.NomeContato = osModel.EnderecoRetirada.NomeContato;
            end2.TelefoneContato = osModel.EnderecoRetirada.TelefoneContato;

            osModel.EnderecoEntrega = end2;

            sJson = GMaps.getGeocode(osModel.EnderecoEntrega.Logradouro);
            token = JObject.Parse(sJson);

            osModel.EnderecoEntrega = new EnderecoEntrega(token);

            TempData["OS"] = osModel;

            return RedirectToAction("Step2");
        }

        //
        // GET: /OrdemServico/Step2
        public ActionResult Step2()
        {
            PreencherBagDropDownLists();
            OSEnderecoModel osModel = (OSEnderecoModel)TempData["OS"];
            return View(osModel);
        }

        //
        // POST: /OrdemServico/Step2
        [HttpPost]
        public ActionResult Step2(OSEnderecoModel osModel)
        {
            OrdemServico os = new OrdemServico();

            os.EnderecoEntrega = osModel.EnderecoEntrega;
            os.EnderecoRetirada = osModel.EnderecoRetirada;

            TempData["OS"] = os;
            return RedirectToAction("Step3");
        }

        //
        // POST: /OrdemServico/Create
        [HttpPost]
        public ActionResult Step3(OrdemServico ordemServico)
        {
            if (!ModelState.IsValid)
            {
                return View(ordemServico);
            }

            OrdemServicoBO bo = new OrdemServicoBO();
            ordemServico.Status = new StatusOrdemServico();
            ordemServico.Status.Id = 1;

            ordemServico.Cliente = new Cliente();
            ordemServico.Cliente.Id = 1;

            // Save de Ordem de Serviço
            try
            {
                ulong id = bo.Incluir(ordemServico);
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
                return RedirectToAction("Index");
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            finally
            {
                if (bo != null)
                    bo.Dispose();
            }
            PreencherBagDropDownLists();
            return View();
        }

        //
        // GET: /OrdemServico/Details/#
        public ActionResult Details(ulong Id)
        {
            OrdemServicoBO bo = new OrdemServicoBO();
            OrdemServico ordemServico = new OrdemServico();
            ordemServico = bo.BuscarPeloId(Id);
            bo.Dispose();
            return View(ordemServico);
        }

        //
        // GET: /OrdemServico/Edit/#
        public ActionResult Edit(ulong Id)
        {
            OrdemServicoBO bo = new OrdemServicoBO();
            OrdemServico ordemServico = new OrdemServico();
            ordemServico = bo.BuscarPeloId(Id);
            bo.Dispose();
            return View(ordemServico);
        }

        //
        // POST: /OrdemServico/Edit/#
        [HttpPost]
        public ActionResult Edit(OrdemServico ordemServico)
        {

            if (!ModelState.IsValid)
            {
                return View(ordemServico);
            }

            OrdemServicoBO bo = new OrdemServicoBO();
            try
            {
                bo.Alterar(ordemServico);
                bo.Dispose();
                TempData["flash"] = "Seu cadastro foi editado com sucesso.";
            }
            catch
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            Listar();
            return View("Index", lista);
        }

        //
        // GET: /OrdemServico/Delete/#
        public ActionResult Delete(ulong Id)
        {
            OrdemServicoBO bo = new OrdemServicoBO();
            try
            {
                OrdemServico ordemServico = new OrdemServico();
                ordemServico = bo.BuscarPeloId(Id);
                bo.Excluir(ordemServico);
                bo.Dispose();
                TempData["flash"] = "A ordem de serviço foi excluida com sucesso.";
            }
            catch
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// GET: /OrdemServico/Process/#
        /// 
        /// Processa a Ordem de Serviço atribuindo a Cobrança, procurando qual o 
        /// Entregador que deve receber
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Process(ulong Id)
        {
            String text;
            text = "";
            try
            {
                OrdemServicoBO bo = new OrdemServicoBO();
                OrdemServico ordemServico = new OrdemServico();
                ordemServico = bo.BuscarPeloId(Id);
                bo.Dispose();

                EntregadorBO ebo = new EntregadorBO();
                IList<Entregador> e = new List<Entregador>();
                e = ebo.ListarAtivos();
                ebo.Dispose();

                String url;

                url = "http://maps.googleapis.com/maps/api/geocode/json?sensor=false&address=" +
                        ordemServico.EnderecoRetirada.getEndereco();

                String sJson = GMaps.getGeocode(ordemServico.EnderecoRetirada.getEndereco());

                JToken token = JObject.Parse(sJson);

                Endereco end = new Endereco(token);

                try
                {
                    TempData["flash"] = token.Value<String[]>("results")[0];
                    // TempData["flash"] = GMaps.getGeocode(ordemServico.EnderecoEntrega.getEndereco());
                }
                catch (JSONException jsonex)
                {
                    TempData["Error"] = jsonex.Message;
                }

                IList<String> entregadores = new List<String>();
                IList<String> retirada = new List<String>();

                foreach (var item in e)
                {
                    entregadores.Add(item.PosicaoAtual);
                }

                retirada.Add(ordemServico.EnderecoRetirada.getEndereco());
                    
                try
                {
                    TempData["flash"] = GMaps.getDistanceMatrix(entregadores, retirada);
                }
                catch (JSONException jsonex)
                {
                    TempData["Error"] = jsonex.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente. " + ex.Message;
            }
            return View();
        }
    }
}
