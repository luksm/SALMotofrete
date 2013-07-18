using NHibernate;
using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SALMvc.Controllers
{
    public class AtendenteController : Controller
    {
        //
        // GET: /Atendente/
        IList<Atendente> lista = new List<Atendente>();

        public void Listar()
        {
            AtendenteBO bo = new AtendenteBO();
            lista = bo.Listar();
            bo.Dispose();
        }

        //
        // GET: /Atendente/
        public ActionResult Index()
        {
            Listar();
            return View(lista);
        }

        //
        // GET: /Atendente/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Atendente/Create
        [HttpPost]
        public ActionResult Create(Atendente atendente)
        {
            AtendenteBO bo = new AtendenteBO();
            try
            {
                bo.Incluir(atendente);
                bo.Dispose();
                TempData["flash"] = "Seu cadastro foi realisado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
                TempData["ErrorMessage"] = ex.Message;
                TempData["ErrorStackTrace"] = ex.StackTrace;
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Atendente/Details/#
        public ActionResult Details(uint Id)
        {
            AtendenteBO bo = new AtendenteBO();
            Atendente am = new Atendente();
            am = bo.BuscarPeloId(Id);
            bo.Dispose();
            return View(am);
        }

        //
        // GET: /Atendente/Edit/#
        public ActionResult Edit(uint Id)
        {
            AtendenteBO bo = new AtendenteBO();
            Atendente am = new Atendente();
            am = bo.BuscarPeloId(Id);
            bo.Dispose();
            return View(am);
        }

        //
        // POST: /Atendente/Edit/#
        [HttpPost]
        public ActionResult Edit(Atendente atendente)
        {
            AtendenteBO bo = new AtendenteBO();
            try
            {
                bo.Alterar(atendente);
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
        // GET: /AparelhoMovel/Delete/#
        public ActionResult Delete(uint Id)
        {
            AtendenteBO bo = new AtendenteBO();
            try
            {
                Atendente a = new Atendente();
                a = bo.BuscarPeloId(Id);
                String nome = a.Nome;
                bo.Excluir(a);
                bo.Dispose();
                TempData["flash"] = "O atendente \"" + nome + " \" foi excluido com sucesso.";
            }
            catch
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            return RedirectToAction("Index");
        }
    }
}
