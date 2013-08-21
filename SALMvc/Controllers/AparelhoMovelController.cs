using NHibernate;
using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilitarios.BO;

namespace SALMvc.Controllers
{
    public class AparelhoMovelController : Controller
    {
        IList<AparelhoMovel> lista = new List<AparelhoMovel>();

        public void Listar()
        {
            try
            {
                using (AparelhoMovelBO bo = new AparelhoMovelBO())
                {
                    lista = bo.Listar();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro ao tentar buscar os aparelhos móveis " + ex.Message;
            }
        }

        private void PreencherBagDropDownLists()
        {
            IList<TipoAparelhoMovel> tipos = null;
            try
            {
                using (TipoAparelhoMovelBO bo = new TipoAparelhoMovelBO())
                {
                    tipos = bo.Listar();
                }
            }
            catch (BOException ex)
            {
                TempData["flash"] = ex.Message;
                return;
            }
            catch (Exception)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
                return;
            }

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

            try
            {
                using (AparelhoMovelBO bo = new AparelhoMovelBO())
                {
                    bo.Incluir(aparelhoMovel);
                }
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
            }
            catch (BOException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(aparelhoMovel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema, tente novamente. " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /AparelhoMovel/Details/#
        public ActionResult Details(uint Id)
        {
            AparelhoMovel am = null;
            try
            {
                using (AparelhoMovelBO bo = new AparelhoMovelBO())
                {
                    am = bo.BuscarPeloId(Id);
                }
            }
            catch (BOException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema, tente novamente. " + ex.Message;
                return RedirectToAction("Index");
            }

            return View(am);
        }

        //
        // GET: /AparelhoMovel/Edit/#
        public ActionResult Edit(uint Id)
        {
            PreencherBagDropDownLists();
            AparelhoMovel am = null;
            try
            {
                using (AparelhoMovelBO bo = new AparelhoMovelBO())
                {
                    am = bo.BuscarPeloId(Id);
                }
            }
            catch (BOException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema, tente novamente. " + ex.Message;
                return RedirectToAction("Index");
            }
            
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

            try
            {
                using (AparelhoMovelBO bo = new AparelhoMovelBO())
                {
                    bo.Alterar(aparelhoMovel);
                }
                TempData["flash"] = "Seu cadastro foi editado com sucesso.";
            }
            catch (BOException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema, tente novamente. " + ex.Message;
                return RedirectToAction("Index");
            }

            Listar();
            return View("Index", lista);
        }

        //
        // GET: /AparelhoMovel/Delete/#
        public ActionResult Delete(uint Id)
        {
            try
            {
                AparelhoMovel am = null;
                using (AparelhoMovelBO bo = new AparelhoMovelBO())
                {
                    am = bo.BuscarPeloId(Id);
                }
                return View(am);
            }
            catch (BOException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema, tente novamente. " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /AparelhoMovel/Delete/#
        [HttpPost]
        public ActionResult Delete(AparelhoMovel aparelhoMovel)
        {
            try
            {
                using (AparelhoMovelBO bo = new AparelhoMovelBO())
                {
                    aparelhoMovel = bo.BuscarPeloId(aparelhoMovel.Id);
                    bo.Excluir(aparelhoMovel);
                }
                
                TempData["flash"] = "O aparelho movel \"" + aparelhoMovel.Modelo + "\" excluído com sucesso.";
            }
            catch (BOException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema, tente novamente. " + ex.Message;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
