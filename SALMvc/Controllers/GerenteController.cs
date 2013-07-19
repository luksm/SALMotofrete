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
    public class GerenteController : Controller
    {
        IList<Gerente> lista = null;

        public void Listar()
        {
            GerenteBO bo = new GerenteBO();
            lista = bo.ListarAtivos();
            bo.Dispose();
        }

        //
        // GET: /Gerente/

        public ActionResult Index()
        {
            Listar();
            return View(lista);
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

            GerenteBO bo = new GerenteBO();

            try
            {
                ulong id = bo.Incluir(gerente);
                if (Request.Files["Foto"] != null && !Request.Files["Foto"].FileName.Equals(""))
                {
                    HttpPostedFileBase postedFile = Request.Files["Foto"];
                    gerente.Foto = Server.MapPath("~/Uploads/FotosGerentes/") + String.Format("{0:0000000000}", id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                    postedFile.SaveAs(gerente.Foto);
                    bo.Alterar(gerente);
                }
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
                Listar();
                return View("Index", lista);
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            /*catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente.");
            }*/
            finally
            {
                if (bo != null)
                    bo.Dispose();
            }
            return View(gerente);
        }

        //
        // GET: /Gerente/Delete/#

        public ActionResult Delete(uint id)
        {
            GerenteBO bo = new GerenteBO();
            Gerente gerente = null;
            try
            {
                gerente = bo.BuscarPeloId(id);
                if (System.IO.File.Exists(gerente.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(gerente.Foto);
                    ViewBag.Foto = "/Uploads/FotosGerente/" + info.Name;
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
                    bo.Dispose();
            }
            return View(gerente);
        }

        //
        // POST: /Gerente/Delete/#

        [HttpPost]
        public ActionResult Delete(Gerente gerente)
        {
            GerenteBO bo = new GerenteBO();
            try
            {
                gerente = bo.BuscarPeloId(gerente.Id);
                if (System.IO.File.Exists(gerente.Foto)) System.IO.File.Delete(gerente.Foto);
                bo.Excluir(gerente);
            }
            catch (Exception)
            {
                Listar();
                return View("Index", lista);
            }
            finally
            {
                if (bo != null)
                    bo.Dispose();
            }
            Listar();
            return View("Index", lista);
        }

        //
        // GET: /Gerente/Edit/#

        public ActionResult Edit(uint id)
        {
            EntregadorBO bo = new EntregadorBO();
            Entregador entregador = new Entregador();
            entregador = bo.BuscarPeloId(id);
            bo.Dispose();
            return View(entregador);
        }

        //
        // POST: /Gerente/Edit/#

        [HttpPost]
        public ActionResult Edit(Gerente gerente)
        {

            GerenteBO bo = new GerenteBO();
            try
            {
                HttpPostedFileBase postedFile = null;
                if (Request.Files["Foto"] != null && !Request.Files["Foto"].FileName.Equals(""))
                {
                    if (System.IO.File.Exists(gerente.Foto)) System.IO.File.Delete(gerente.Foto);
                    postedFile = Request.Files["Foto"];
                    gerente.Foto = Server.MapPath("~/Uploads/FotosGerentes/") + String.Format("{0:0000000000}", gerente.Id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                }
                bo.Alterar(gerente);
                if (postedFile != null)
                {
                    postedFile.SaveAs(gerente.Foto);
                }
                bo.Dispose();
                TempData["flash"] = "Seu cadastro foi editado com sucesso.";
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            /*catch(Exception)
            {
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente.");
            }*/
            Listar();
            return View("Index", lista);
        }

        //
        // GET: /Gerente/Details/#

        public ActionResult Details(uint id)
        {
            GerenteBO bo = new GerenteBO();
            Gerente gerente = null;
            try
            {
                gerente = bo.BuscarPeloId(id);
                if (System.IO.File.Exists(gerente.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(gerente.Foto);
                    ViewBag.Foto = "/Uploads/FotosGerentes/" + info.Name;
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
                    bo.Dispose();
            }
            return View(gerente);
        }
    }
}
