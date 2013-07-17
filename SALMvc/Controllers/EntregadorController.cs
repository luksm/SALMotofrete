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
    public class EntregadorController : Controller
    {
        IList<Entregador> lista = null;

        public void Listar()
        {
            EntregadorBO bo = new EntregadorBO();
            lista = bo.ListarAtivos();
            bo.Dispose();
        }

        public void PreencherBagAparelhoMovel()
        {
            AparelhoMovelBO bo = new AparelhoMovelBO();
            IList<AparelhoMovel> aparelhos = bo.ListarDisponiveis();
            bo.Dispose();
            bo = null;
            var listaAparelhos = new List<SelectListItem>();
            foreach (var item in aparelhos)
            {
                listaAparelhos.Add(
                    new SelectListItem()
                    {
                        Text = item.Id + " (" + item.Modelo + " " + item.Marca + ")",
                        Value = item.Id.ToString()
                    }
                );
            }
            ViewBag.AparelhoMovel = listaAparelhos;
        }
        //
        // GET: /Entregador/

        public ActionResult Index()
        {
            Listar();
            return View(lista);
        }

        public ActionResult Create()
        {
            PreencherBagAparelhoMovel();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Entregador entregador)
        {
            PreencherBagAparelhoMovel();

            if (!ModelState.IsValid)
            {
                return View(entregador);
            }

            EntregadorBO bo = new EntregadorBO();
            
            try
            {
                ulong id = bo.Incluir(entregador);
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    HttpPostedFileBase postedFile = Request.Files["Foto"];
                    entregador.Foto = Server.MapPath("~/Uploads/FotosEntregadores/") + String.Format("{0:0000000000}", id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                    postedFile.SaveAs(entregador.Foto);
                    bo.Alterar(entregador);
                }
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
                Listar();
                return View("Index", lista);
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
                if(bo != null)
                    bo.Dispose();
            }
            return View(entregador);
        }

        public ActionResult Edit(uint id)
        {
            PreencherBagAparelhoMovel();
            EntregadorBO bo = new EntregadorBO();
            Entregador entregador = new Entregador();
            entregador = bo.BuscarPeloId(id);
            bo.Dispose();
            return View(entregador);
        }

        [HttpPost]
        public ActionResult Edit(Entregador entregador)
        {
            EntregadorBO bo = new EntregadorBO();
            try
            {
                HttpPostedFileBase postedFile = null;
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    if (System.IO.File.Exists(entregador.Foto)) System.IO.File.Delete(entregador.Foto);
                    postedFile = Request.Files["Foto"];
                    entregador.Foto = Server.MapPath("~/Uploads/FotosEntregadores/") + String.Format("{0:0000000000}", entregador.Id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
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
            /*catch(Exception)
            {
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente.");
            }*/
            Listar();
            return View("Index", lista);
        }

        public ActionResult Delete(int id)
        {
            EntregadorBO bo = new EntregadorBO();
            Entregador entregador = null;
            try
            {
                entregador = bo.BuscarPeloId(id);
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

        [HttpPost]
        public ActionResult Delete(Entregador entregador)
        {
            EntregadorBO bo = new EntregadorBO();
            try
            {
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
    }
}
