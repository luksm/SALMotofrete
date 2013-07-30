using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilitarios;
using Utilitarios.BO;

namespace SALMvc.Controllers
{
    public class EntregadorController : Controller
    {
        IList<Entregador> lista = null;
        private String pastaFotos = "/Uploads/Fotos/Funcionarios/Entregador/";

        public void Listar()
        {
            EntregadorBO bo = null;
            try
            {
                bo = new EntregadorBO();
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

        public void PreencherBagDropDownLists(Entregador e = null)
        {
            AparelhoMovelBO bo = null;
            IList<AparelhoMovel> aparelhos = null;
            try
            {
                bo = new AparelhoMovelBO();
                aparelhos = bo.ListarDisponiveis();
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                if (bo != null)
                {
                    bo.Dispose();
                    bo = null;
                }
            }

            var listaAparelhos = new List<SelectListItem>();
            foreach (var item in aparelhos)
            {
                listaAparelhos.Add(
                    new SelectListItem()
                    {
                        Text = item.Id + " (" + item.Marca + " " + item.Modelo + ")",
                        Value = item.Id.ToString()
                    }
                );
            }
            //adiciona no dropdownlist de aparelho movel tambem o aparelho movel atual do entregador
            if (e != null)
            {
                listaAparelhos.Add(new SelectListItem()
                {
                    Text = e.AparelhoMovel.Id + " (" + e.AparelhoMovel.Modelo + " " + e.AparelhoMovel.Marca + ")",
                    Value = e.AparelhoMovel.Id.ToString(),
                    Selected = true
                });
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
            PreencherBagDropDownLists();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Entregador entregador)
        {
            ValidationHelper.RemoverValidacaoDoModelState(ModelState, 
                "AparelhoMovel.Tipo", "AparelhoMovel.Modelo", "AparelhoMovel.Marca");
            if (!ModelState.IsValid)
            {
                PreencherBagDropDownLists();
                return View(entregador);
            }

            EntregadorBO bo = null;
            
            try
            {
                bo = new EntregadorBO();
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    HttpPostedFileBase postedFile = Request.Files["Foto"];
                    bo.Incluir(entregador, Server.MapPath("~" + pastaFotos), postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4));
                    postedFile.SaveAs(entregador.Foto);
                }
                else
                {
                    PreencherBagDropDownLists();
                    ModelState.AddModelError("", "Escolha uma foto para o entregador");
                    return View(entregador);
                }
                TempData["flash"] = "Seu cadastro foi realizado com sucesso.";
            }
            catch (BOException ex)
            {
                PreencherBagDropDownLists();
                ModelState.AddModelError("", ex.Message);
                return View(entregador);
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
            return RedirectToAction("Index");
        }

        public ActionResult Edit(uint id)
        {
            EntregadorBO bo = null;
            Entregador entregador = null;
            try
            {
                bo = new EntregadorBO();
                entregador = new Entregador();
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
            PreencherBagDropDownLists(entregador);
            return View(entregador);
        }

        [HttpPost]
        public ActionResult Edit(Entregador entregador)
        {
            ValidationHelper.RemoverValidacaoDoModelState(ModelState,
                "AparelhoMovel.Tipo", "AparelhoMovel.Modelo", "AparelhoMovel.Marca");
            if (!ModelState.IsValid)
            {
                PreencherBagDropDownLists(entregador);
                return View(entregador);
            }
            
            EntregadorBO bo = null;
            try
            {
                HttpPostedFileBase postedFile = null;
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    if (System.IO.File.Exists(entregador.Foto)) System.IO.File.Delete(entregador.Foto);
                    postedFile = Request.Files["Foto"];
                    entregador.Foto = Server.MapPath("~" + pastaFotos) + String.Format("{0:0000000000}", entregador.Id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                }
                bo = new EntregadorBO();
                bo.Alterar(entregador);
                if (postedFile != null)
                {
                    postedFile.SaveAs(entregador.Foto);
                }
                TempData["flash"] = "Seu cadastro foi atualizado com sucesso.";
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(entregador);
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

        public ActionResult Delete(uint id)
        {
            EntregadorBO bo = null;
            Entregador entregador = null;
            try
            {
                bo = new EntregadorBO();
                entregador = bo.BuscarPeloId(id);
                if (System.IO.File.Exists(entregador.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(entregador.Foto);
                    ViewBag.Foto = pastaFotos + info.Name;
                }
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

        [HttpPost]
        public ActionResult Delete(Entregador entregador)
        {
            EntregadorBO bo = null;
            try
            {
                bo = new EntregadorBO();
                entregador = bo.BuscarPeloId(entregador.Id);
                bo.Excluir(entregador);
                TempData["flash"] = "O entregador \"" + entregador.Nome + "\" foi excluído com sucesso.";
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

        public ActionResult Details(uint id)
        {
            EntregadorBO bo = null;
            Entregador entregador = null;
            try
            {
                bo = new EntregadorBO();
                entregador = bo.BuscarPeloId(id);
                if (System.IO.File.Exists(entregador.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(entregador.Foto);
                    ViewBag.Foto = pastaFotos + info.Name;
                }
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
    }
}
