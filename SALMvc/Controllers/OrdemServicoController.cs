using NHibernate;
using SALClassLib.OS.Model;
using SALClassLib.OS.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        //
        // POST: /OrdemServico/Step2
        [HttpPost]
        public ActionResult Step2()
        {
            OrdemServico ordemServico = new OrdemServico();
            ordemServico.EnderecoRetirada = new EnderecoRetirada();
            ordemServico.EnderecoEntrega = new EnderecoEntrega();

            ordemServico.Data = DateTime.Now;

            ordemServico.EnderecoRetirada.Logradouro = Request.Params["Origem[0][Endereco]"];
            // ordemServico.EnderecoRetirada.Complemeto = Request.Params["Origem[0][Complemento]"];
            ordemServico.EnderecoRetirada.NomeContato = Request.Params["Origem[0][Contato]"];

            ordemServico.EnderecoEntrega.Logradouro = Request.Params["Destino[0][Endereco]"];
            // ordemServico.EnderecoEntrega.Complemeto = Request.Params["Destino[0][Complemento]"];
            ordemServico.EnderecoEntrega.NomeContato = Request.Params["Destino[0][Contato]"];

            return View(ordemServico);
        }

        //
        // POST: /OrdemServico/Create
        [HttpPost]
        public ActionResult Create(OrdemServico ordemServico)
        {

            if (!ModelState.IsValid)
            {
                return View(ordemServico);
            }

            OrdemServicoBO bo = new OrdemServicoBO();
            try
            {
                bo.Incluir(ordemServico);
                bo.Dispose();
                TempData["flash"] = "Seu cadastro foi realisado com sucesso.";
            }
            catch
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /OrdemServico/Details/#
        public ActionResult Details(uint Id)
        {
            OrdemServicoBO bo = new OrdemServicoBO();
            OrdemServico ordemServico = new OrdemServico();
            ordemServico = bo.BuscarPeloId(Id);
            bo.Dispose();
            return View(ordemServico);
        }

        //
        // GET: /OrdemServico/Edit/#
        public ActionResult Edit(uint Id)
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
        public ActionResult Delete(uint Id)
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
    }
}
