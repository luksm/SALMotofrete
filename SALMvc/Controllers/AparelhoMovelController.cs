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
    public class AparelhoMovelController : Controller
    {
        IList<AparelhoMovel> lista = new List<AparelhoMovel>();

        public void Listar()
        {
            AparelhoMovelBO bo = new AparelhoMovelBO();
            lista = bo.Listar();
            bo.Dispose();
        }

        private void PreencherBagDropDownLists()
        {
            TipoAparelhoMovelBO bo = new TipoAparelhoMovelBO();
            IList<TipoAparelhoMovel> tipos = bo.Listar();
            bo.Dispose();
            bo = null;
            var listaTipos = new List<SelectListItem>();
            foreach (var tipo in tipos)
            {
                listaTipos.Add(
                    new SelectListItem() { Text = tipo.Descricao, Value = tipo.Id.ToString() }
                );
            }
            ViewBag.TipoAparelhoMovel = listaTipos;
        }

        //
        // GET: /AparelhoMovel/
        public ActionResult Index()
        {
            Listar();
            return View(lista);
        }

        //
        // GET: /AparelhoMovel/Create
        public ActionResult Create()
        {
            PreencherBagDropDownLists();
            return View();
        }

        //
        // POST: /AparelhoMovel/Create
        [HttpPost]
        public ActionResult Create(AparelhoMovel aparelhoMovel)
        {

            if (!ModelState.IsValid)
            {
                PreencherBagDropDownLists();
                return View(aparelhoMovel);
            }

            AparelhoMovelBO bo = new AparelhoMovelBO();
            try
            {
                bo.Incluir(aparelhoMovel);
                bo.Dispose();
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
            }
            catch
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /AparelhoMovel/Details/#
        public ActionResult Details(uint Id)
        {
            TipoAparelhoMovelBO bo2 = new TipoAparelhoMovelBO();
            IList<TipoAparelhoMovel> tipos = bo2.Listar();
            bo2.Dispose();
            bo2 = null;
            var listaTipos = new List<SelectListItem>();
            foreach (var tipo in tipos)
            {
                listaTipos.Add(
                    new SelectListItem() { Text = tipo.Descricao, Value = tipo.Id.ToString() }
                );
            }
            ViewBag.TipoAparelhoMovel = listaTipos;
            AparelhoMovelBO bo = new AparelhoMovelBO();
            AparelhoMovel am = new AparelhoMovel();
            am = bo.BuscarPeloId(Id);
            bo.Dispose();
            return View(am);
        }

        //
        // GET: /AparelhoMovel/Edit/#
        public ActionResult Edit(uint Id)
        {
            PreencherBagDropDownLists();
            AparelhoMovelBO bo = new AparelhoMovelBO();
            AparelhoMovel am = new AparelhoMovel();
            am = bo.BuscarPeloId(Id);
            bo.Dispose();
            return View(am);
        }

        //
        // POST: /AparelhoMovel/Edit/#
        [HttpPost]
        public ActionResult Edit(AparelhoMovel aparelhoMovel)
        {

            if (!ModelState.IsValid)
            {
                PreencherBagDropDownLists();
                return View(aparelhoMovel);
            }

            AparelhoMovelBO bo = new AparelhoMovelBO();
            try
            {
                bo.Alterar(aparelhoMovel);
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
            AparelhoMovelBO bo = new AparelhoMovelBO();
            try
            {
                AparelhoMovel am = new AparelhoMovel();
                am = bo.BuscarPeloId(Id);
                String modelo = am.Modelo;
                bo.Excluir(am);
                bo.Dispose();
                TempData["flash"] = "O aparelho movel \"" + modelo + "\" excluido com sucesso.";
            }
            catch
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            return RedirectToAction("Index");
        }
    }
}
