using NHibernate;
using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using Utilitarios.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SALMvc.Controllers
{
    public class AtendenteController : Controller
    {
        IList<Atendente> lista = new List<Atendente>();
        String pastaFotos = "/Uploads/Fotos/Funcionarios/Atendente/";

        public void Listar()
        {
            AtendenteBO bo = null;
            try
            {
                bo = new AtendenteBO();
                lista = bo.ListarAtivos();
            }
            catch 
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
            if (!ModelState.IsValid)
            {
                return View(atendente);
            }

            AtendenteBO bo = null;

            try
            {
                bo = new AtendenteBO();
                //se houver foto, inclui o atendente passando os parametros para incluir a foto
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    HttpPostedFileBase postedFile = Request.Files["Foto"];
                    bo.Incluir(atendente, Server.MapPath("~" + pastaFotos), postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4));
                    postedFile.SaveAs(atendente.Foto);
                }
                //caso contrario apenas inclui o atendente
                else
                {
                    bo.Incluir(atendente);
                }
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
            }
            catch (BOException ex)
            {
                ModelState.AddModelError(ex.Message, ex.Message);
                return View(atendente);
            }
            catch (Exception ex)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            finally
            {
                if (bo != null)
                    bo.Dispose();
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Atendente/Details/#
        public ActionResult Details(uint Id)
        {
            AtendenteBO bo = null;
            Atendente atendente = new Atendente();
            try
            {
                bo = new AtendenteBO();
                atendente = bo.BuscarPeloId(Id);
                if (System.IO.File.Exists(atendente.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(atendente.Foto);
                    ViewBag.Foto = pastaFotos + info.Name;
                }
            }
            catch (Exception)
            {
                RedirectToAction("Index");
            }
            finally
            {
                if (bo != null)
                    bo.Dispose();
            }
            return View(atendente);
        }

        //
        // GET: /Atendente/Edit/#
        public ActionResult Edit(uint Id)
        {
            AtendenteBO bo = null;
            try
            {
                bo = new AtendenteBO();
                Atendente am = bo.BuscarPeloId(Id);
                return View(am);
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
        // POST: /Atendente/Edit/#
        [HttpPost]
        public ActionResult Edit(Atendente atendente)
        {
            if (!ModelState.IsValid)
            {
                return View(atendente);
            }

            AtendenteBO bo = null;
            try
            {
                bo = new AtendenteBO();
                HttpPostedFileBase postedFile = null;
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    if (System.IO.File.Exists(atendente.Foto)) System.IO.File.Delete(atendente.Foto);
                    postedFile = Request.Files["Foto"];
                    atendente.Foto = Server.MapPath(pastaFotos) + String.Format("{0:0000000000}", atendente.Id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                }
                bo.Alterar(atendente);
                if (postedFile != null)
                {
                    postedFile.SaveAs(atendente.Foto);
                }
                TempData["flash"] = "Seu cadastro foi atualizado com sucesso.";
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(atendente);
            }
            catch (Exception ex)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            finally
            {
                if (bo != null)
                    bo.Dispose();
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Atendente/Delete/#
        public ActionResult Delete(uint Id)
        {
            AtendenteBO bo = null;
            try
            {
                bo = new AtendenteBO();
                Atendente a = bo.BuscarPeloId(Id);
                if (System.IO.File.Exists(a.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(a.Foto);
                    ViewBag.Foto = pastaFotos + info.Name;
                }
                return View(a);
            }
            catch
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente";
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
        // POST: /Atendente/Delete/#
        [HttpPost]
        public ActionResult Delete(Atendente atendente)
        {
            AtendenteBO bo = null;
            try
            {
                bo = new AtendenteBO();
                atendente = bo.BuscarPeloId(atendente.Id);
                bo.Excluir(atendente);
                TempData["flash"] = "O atendente \"" + atendente.Nome + "\" foi excluido com sucesso.";
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(atendente);
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
    }
}
