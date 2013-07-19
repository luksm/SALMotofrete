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
    public class ClienteController : Controller
    {
        IList<Cliente> lista = null;

        public void Listar()
        {
            ClienteBO bo = new ClienteBO();
            lista = bo.ListarAtivos();
            bo.Dispose();
        }

        public void ListarEnderecos()
        {
            ClienteBO bo = new ClienteBO();
            lista = bo.Listar();
            bo.Dispose();
        }

        //
        // GET: /Cliente/

        public ActionResult Index()
        {
            Listar();
            return View(lista);
        }

        //
        // GET: /Cliente/CreatePF

        public ActionResult CreatePF()
        {
            return View();
        }

        //
        // POST: /Cliente/CreatePF

        [HttpPost]
        public ActionResult CreatePF(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            ClienteBO bo = null;

            try
            {
                bo = new ClienteBO();
                bo.Incluir(cliente);
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
                return View("Enderecos");
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente.");
            }
            finally
            {
                if (bo != null)
                    bo.Dispose();
            }
            return View(cliente);
        }

        //
        // GET: /Cliente/Edit/#
        /*
        public ActionResult Edit(uint id)
        {
            ClienteBO bo = new ClienteBO();
            Cliente entregador = new Cliente();
            entregador = bo.BuscarPeloId(id);
            bo.Dispose();
            PreencherBagDropDownLists(entregador);
            return View(entregador);
        }

        //
        // POST: /Cliente/Edit/#

        [HttpPost]
        public ActionResult Edit(Cliente entregador)
        {
            if (!ModelState.IsValid)
            {
                return View(entregador);
            }

            ClienteBO bo = new ClienteBO();
            try
            {
                HttpPostedFileBase postedFile = null;
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    if (System.IO.File.Exists(entregador.Foto)) System.IO.File.Delete(entregador.Foto);
                    postedFile = Request.Files["Foto"];
                    entregador.Foto = Server.MapPath("~/Uploads/FotosClientees/") + String.Format("{0:0000000000}", entregador.Id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                }
                bo.Alterar(entregador);
                if (postedFile != null)
                {
                    postedFile.SaveAs(entregador.Foto);
                }
                bo.Dispose();
                TempData["flash"] = "Seu cadastro foi editado com sucesso.";
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch(Exception)
            {
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente.");
            }
            Listar();
            return View("Index", lista);
        }

        //
        // GET: /Cliente/Delete/#

        public ActionResult Delete(uint id)
        {
            ClienteBO bo = new ClienteBO();
            Cliente entregador = null;
            try
            {
                entregador = bo.BuscarPeloId(id);
                if (System.IO.File.Exists(entregador.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(entregador.Foto);
                    ViewBag.Foto = "/Uploads/FotosClientees/" + info.Name;
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
            return View(entregador);
        }

        //
        // POST: /Cliente/Delete/#

        [HttpPost]
        public ActionResult Delete(Cliente entregador)
        {
            ClienteBO bo = new ClienteBO();
            try
            {
                entregador = bo.BuscarPeloId(entregador.Id);
                if (System.IO.File.Exists(entregador.Foto)) System.IO.File.Delete(entregador.Foto);
                bo.Excluir(entregador);
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
        // GET: /Cliente/Details/#

        public ActionResult Details(uint id)
        {
            ClienteBO bo = new ClienteBO();
            Cliente entregador = null;
            try
            {
                entregador = bo.BuscarPeloId(id);
                if (System.IO.File.Exists(entregador.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(entregador.Foto);
                    ViewBag.Foto = "/Uploads/FotosClientees/" + info.Name;
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
            return View(entregador);
        }*/
    }
}
