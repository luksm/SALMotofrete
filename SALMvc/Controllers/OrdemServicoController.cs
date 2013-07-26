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

        //
        // GET: /OrdemServico/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /OrdemServico/
        public ActionResult Create()
        {
            return View();
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
        // GET: /OrdemServico/List
        public void Listar()
        {
            OrdemServicoBO bo = new OrdemServicoBO();
            lista = bo.Listar();
            bo.Dispose();
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
