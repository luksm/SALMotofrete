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

        List<Atendente> lista = new List<Atendente>();

        public ActionResult Index()
        {
            lista.Add(new Atendente() { });
            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Atendente atendente)
        {
            AtendenteBO bo = new AtendenteBO();
            bo.Incluir(atendente);
            bo.Dispose();
            return View("Index");
        }
    }
}
