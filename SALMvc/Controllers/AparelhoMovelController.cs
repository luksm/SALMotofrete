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

        List<AparelhoMovel> lista = new List<AparelhoMovel>();

        public ActionResult Index()
        {
            lista.Add(new AparelhoMovel() { Marca = "marca teste", Modelo = "teste modelo" });
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
            bo.Incluir(aparelhoMovel);
            bo.Dispose();
            return View("Index");
        }
    }
}
