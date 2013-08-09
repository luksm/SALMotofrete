using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilitarios.BO;
using PagedList;

namespace SALMvc.Controllers
{
    public class GerenteController : Controller
    {
        IList<Gerente> lista = null;
        private String pastaFotos = "/Uploads/Fotos/Funcionarios/Gerente/";

        public void Listar()
        {
            GerenteBO bo = null;
            try
            {
                bo = new GerenteBO();
                lista = bo.ListarAtivos();
            }
            catch(Exception)
            {
                TempData["flash"] = "Ocorreu um erro ao tentar buscar os atendentes";
            }
            finally
            {
                if (bo != null)
                {
                    bo.Dispose();
                    bo = null;
                }
            }
        }

        //
        // GET: /Gerente/

        public ActionResult Index(int? page)
        {
            int pageSize;
            if (Request["RegPagina"] != null) Session["RegPagina"] = Request["RegPagina"];
            if (Session["RegPagina"] == null || !int.TryParse(Session["RegPagina"].ToString(), out pageSize)) pageSize = 20;
            Session["RegPagina"] = pageSize;
            Listar();
            int pageNumber = (page ?? 1);
            return View(lista.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Gerente/Create
        
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Gerente/Create
        
        [HttpPost]
        public ActionResult Create(Gerente gerente)
        {
            if (!ModelState.IsValid)
            {
                return View(gerente);
            }

            GerenteBO bo = null;

            try
            {
                bo = new GerenteBO();
                if (Request.Files["Foto"] != null && !Request.Files["Foto"].FileName.Equals(""))
                {
                    HttpPostedFileBase postedFile = Request.Files["Foto"];
                    bo.Incluir(gerente, Server.MapPath("~" + pastaFotos), postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4));
                    postedFile.SaveAs(gerente.Foto);
                }
                else
                {
                    bo.Incluir(gerente);
                }
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
                return RedirectToAction("Index");
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            finally
            {
                if (bo != null)
                {
                    bo.Dispose();
                    bo = null;
                }
            }
            return View(gerente);
        }

        //
        // GET: /Gerente/Delete/#

        public ActionResult Delete(uint id)
        {
            GerenteBO bo = null;
            Gerente gerente = null;
            try
            {
                bo = new GerenteBO();
                gerente = bo.BuscarPeloId(id);
                if (System.IO.File.Exists(gerente.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(gerente.Foto);
                    ViewBag.Foto = "/Uploads/FotosGerente/" + info.Name;
                }
            }
            catch (Exception)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
                return RedirectToAction("Index");
            }
            finally
            {
                if (bo != null)
                {
                    bo.Dispose();
                    bo = null;
                }
            }
            return View(gerente);
        }

        //
        // POST: /Gerente/Delete/#

        [HttpPost]
        public ActionResult Delete(Gerente gerente)
        {
            GerenteBO bo = null;
            try
            {
                bo = new GerenteBO();
                gerente = bo.BuscarPeloId(gerente.Id);
                bo.Excluir(gerente);
            }
            catch (Exception)
            {
                TempData["flash"] = "Ocorreu um erro, tente novamente.";
            }
            finally
            {
                if (bo != null)
                {
                    bo.Dispose();
                    bo = null;
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Gerente/Edit/#

        public ActionResult Edit(uint id)
        {
            GerenteBO bo = null;
            Gerente entregador = null;
            try
            {
                bo = new GerenteBO();
                entregador = bo.BuscarPeloId(id);
            }
            catch (Exception)
            {
                TempData["flash"] = "Ocorreu um erro, tente novamente.";
                return RedirectToAction("Index");
            }
            finally
            {
                if (bo != null)
                {
                    bo.Dispose();
                    bo = null;
                }
            }
            return View(entregador);
        }

        //
        // POST: /Gerente/Edit/#

        [HttpPost]
        public ActionResult Edit(Gerente gerente)
        {
            if (!ModelState.IsValid)
            {
                return View(gerente);
            }

            GerenteBO bo = null;
            try
            {
                HttpPostedFileBase postedFile = null;
                if (Request.Files["Foto"] != null && !Request.Files["Foto"].FileName.Equals(""))
                {
                    if (System.IO.File.Exists(gerente.Foto)) System.IO.File.Delete(gerente.Foto);
                    postedFile = Request.Files["Foto"];
                    gerente.Foto = Server.MapPath("~" + pastaFotos) + String.Format("{0:0000000000}", gerente.Id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                }
                bo = new GerenteBO();
                bo.Alterar(gerente);
                if (postedFile != null)
                {
                    postedFile.SaveAs(gerente.Foto);
                }
                TempData["flash"] = "Seu cadastro foi editado com sucesso.";
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(gerente);
            }
            catch (Exception)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            finally
            {
                if (bo != null)
                {
                    bo.Dispose();
                    bo = null;
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Gerente/Details/#

        public ActionResult Details(uint id)
        {
            GerenteBO bo = null;
            Gerente gerente = null;
            try
            {
                bo = new GerenteBO();
                gerente = bo.BuscarPeloId(id);
                if (System.IO.File.Exists(gerente.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(gerente.Foto);
                    ViewBag.Foto = pastaFotos + info.Name;
                }
            }
            catch (Exception)
            {
                Listar();
                return View("Index", lista);
            }
            finally
            {
                if (bo != null)
                {
                    bo.Dispose();
                    bo = null;
                }
            }
            return View(gerente);
        }
    }
}
