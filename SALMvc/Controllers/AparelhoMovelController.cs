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
        //
        // GET: /AparelhoMovel/

        IList<AparelhoMovel> lista = new List<AparelhoMovel>();

        public void Listar()
        {
            AparelhoMovelBO bo = new AparelhoMovelBO();
            lista = bo.Listar();
            bo.Dispose();
        }

        public ActionResult Index()
        {
            Listar();
            return View(lista);
        }

        public ActionResult Create()
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(AparelhoMovel aparelhoMovel)
        {
            AparelhoMovelBO bo = new AparelhoMovelBO();
            try
            {
                bo.Incluir(aparelhoMovel);
                bo.Dispose();
                TempData["flash"] = "Seu cadastro foi realisado com sucesso.";
            }
            catch
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            return View("Index");
        }

        public ActionResult Edit(uint Id)
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


        [HttpPost]
        public ActionResult Edit(AparelhoMovel aparelhoMovel)
        {
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
    }
}
