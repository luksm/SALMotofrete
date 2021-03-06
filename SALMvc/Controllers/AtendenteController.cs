﻿using NHibernate;
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
    [Authorize]
    public class AtendenteController : Controller
    {
        IList<Atendente> lista = new List<Atendente>();
        String pastaFotos = "/Uploads/Fotos/Funcionarios/Atendente/";

        public void Listar()
        {
            try
            {
                using (AtendenteBO bo = new AtendenteBO())
                {
                    lista = bo.ListarAtivos();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro ao tentar buscar os atendentes" + ex.Message;
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

            try
            {
                //se houver foto, inclui o atendente passando os parametros para incluir a foto
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    HttpPostedFileBase postedFile = Request.Files["Foto"];
                    using (AtendenteBO bo = new AtendenteBO())
                    {
                        bo.Incluir(atendente, Server.MapPath("~" + pastaFotos), postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4));
                    }
                    postedFile.SaveAs(atendente.Foto);
                }
                //caso contrario apenas inclui o atendente
                else
                {
                    using (AtendenteBO bo = new AtendenteBO())
                    {
                        bo.Incluir(atendente);
                    }
                }
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
            }
            catch (BOException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(atendente);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema, tente novamente. " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Atendente/Details/#
        public ActionResult Details(uint Id)
        {
            Atendente atendente = new Atendente();

            try
            {
                using (AtendenteBO bo = new AtendenteBO())
                {
                    atendente = bo.BuscarPeloId(Id);
                }
                if (System.IO.File.Exists(atendente.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(atendente.Foto);
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

            return View(atendente);
        }

        //
        // GET: /Atendente/Edit/#
        public ActionResult Edit(uint Id)
        {
            try
            {
                Atendente am = null;
                using (AtendenteBO bo = new AtendenteBO())
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
        // POST: /Atendente/Edit/#
        [HttpPost]
        public ActionResult Edit(Atendente atendente)
        {
            if (!ModelState.IsValid)
            {
                return View(atendente);
            }

            try
            {
                HttpPostedFileBase postedFile = null;
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    if (System.IO.File.Exists(atendente.Foto)) System.IO.File.Delete(atendente.Foto);
                    postedFile = Request.Files["Foto"];
                    atendente.Foto = Server.MapPath(pastaFotos) + String.Format("{0:0000000000}", atendente.Id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                }
                using (AtendenteBO bo = new AtendenteBO())
                {
                    bo.Alterar(atendente);
                }
                if (postedFile != null)
                {
                    postedFile.SaveAs(atendente.Foto);
                }
                TempData["flash"] = "Seu cadastro foi atualizado com sucesso.";
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
        // GET: /Atendente/Delete/#
        public ActionResult Delete(uint Id)
        {
            try
            {
                Atendente a = null;
                using (AtendenteBO bo = new AtendenteBO())
                {
                    a = bo.BuscarPeloId(Id);
                }
                if (System.IO.File.Exists(a.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(a.Foto);
                    ViewBag.Foto = pastaFotos + info.Name;
                }
                return View(a);
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
        // POST: /Atendente/Delete/#
        [HttpPost]
        public ActionResult Delete(Atendente atendente)
        {
            try
            {
                using (AtendenteBO bo = new AtendenteBO())
                {
                    atendente = bo.BuscarPeloId(atendente.Id);
                    bo.Excluir(atendente);
                }
                TempData["flash"] = "O atendente \"" + atendente.Nome + "\" foi excluído com sucesso.";
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
