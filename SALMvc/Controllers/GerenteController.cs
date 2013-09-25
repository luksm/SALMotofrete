using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilitarios.BO;
using PagedList;
using SALMvc.Helpers;

namespace SALMvc.Controllers
{
    [Authorize]
    public class GerenteController : Controller
    {
        IList<Gerente> lista = null;
        private String pastaFotos = "/Uploads/Fotos/Funcionarios/Gerente/";

        public void Listar()
        {
            try
            {
                using (GerenteBO bo = new GerenteBO())
                {
                    lista = bo.ListarAtivos();
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro ao tentar buscar os atendentes" + ex.Message;
            }
        }

        //
        // GET: /Gerente/
        
        public ActionResult Index(int? page, int? pageSize)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();

            if(Session["RegPagina"] == null)
                Session["RegPagina"] = 10;
            if (pageSize != null) Session["RegPagina"] = pageSize;
            Listar();
            int pageNumber = (page ?? 1);
            return View(lista.ToPagedList(pageNumber, Convert.ToInt32(Session["RegPagina"])));
        }

        //
        // GET: /Gerente/Create
        
        public ActionResult Create()
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();
            return View();
        }

        //
        // POST: /Gerente/Create
        
        [HttpPost]
        public ActionResult Create(Gerente gerente)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();
            if (!ModelState.IsValid)
            {
                return View(gerente);
            }

            try
            {
                if (Request.Files["Foto"] != null && !Request.Files["Foto"].FileName.Equals(""))
                {
                    HttpPostedFileBase postedFile = Request.Files["Foto"];
                    using (GerenteBO bo = new GerenteBO())
                    {
                        bo.Incluir(gerente, Server.MapPath("~" + pastaFotos), postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4));
                    }
                    postedFile.SaveAs(gerente.Foto);
                }
                else
                {
                    using (GerenteBO bo = new GerenteBO())
                    {
                        bo.Incluir(gerente);
                    }
                }
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
                return RedirectToAction("Index");
            }
            catch (BOException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(gerente);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema, tente novamente. " + ex.Message;
            }

            return View(gerente);
        }

        //
        // GET: /Gerente/Delete/#
        
        public ActionResult Delete(uint id)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();
            Gerente gerente = null;
            try
            {
                using (GerenteBO bo = new GerenteBO())
                {
                    gerente = bo.BuscarPeloId(id);
                }
                if (System.IO.File.Exists(gerente.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(gerente.Foto);
                    ViewBag.Foto = "/Uploads/FotosGerente/" + info.Name;
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

            return View(gerente);
        }

        //
        // POST: /Gerente/Delete/#

        [HttpPost]
        public ActionResult Delete(Gerente gerente)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();
            
            try
            {
                using (GerenteBO bo = new GerenteBO())
                {
                    gerente = bo.BuscarPeloId(gerente.Id);
                    bo.Excluir(gerente);
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

            return RedirectToAction("Index");
        }

        //
        // GET: /Gerente/Edit/#

        public ActionResult Edit(uint id)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();
            
            Gerente entregador = null;
            try
            {
                using (GerenteBO bo = new GerenteBO())
                {
                    entregador = bo.BuscarPeloId(id);
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

            return View(entregador);
        }

        //
        // POST: /Gerente/Edit/#

        [HttpPost]
        public ActionResult Edit(Gerente gerente)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();
            if (!ModelState.IsValid)
            {
                return View(gerente);
            }

            try
            {
                HttpPostedFileBase postedFile = null;
                if (Request.Files["Foto"] != null && !Request.Files["Foto"].FileName.Equals(""))
                {
                    if (System.IO.File.Exists(gerente.Foto)) System.IO.File.Delete(gerente.Foto);
                    postedFile = Request.Files["Foto"];
                    gerente.Foto = Server.MapPath("~" + pastaFotos) + String.Format("{0:0000000000}", gerente.Id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                }
                using (GerenteBO bo = new GerenteBO())
                {
                    bo.Alterar(gerente);
                }
                if (postedFile != null)
                {
                    postedFile.SaveAs(gerente.Foto);
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

            return RedirectToAction("Index");
        }

        //
        // GET: /Gerente/Details/#

        public ActionResult Details(uint id)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();
            
            Gerente gerente = null;
            try
            {
                using (GerenteBO bo = new GerenteBO())
                {
                    gerente = bo.BuscarPeloId(id);
                }
                if (System.IO.File.Exists(gerente.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(gerente.Foto);
                    ViewBag.Foto = pastaFotos + info.Name;
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

            return View(gerente);
        }
    }
}
