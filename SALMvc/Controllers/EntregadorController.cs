using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using SALMvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilitarios;
using Utilitarios.BO;

namespace SALMvc.Controllers
{
    [Authorize]
    public class EntregadorController : Controller
    {
        IList<Entregador> lista = null;
        private String pastaFotos = "/Uploads/Fotos/Funcionarios/Entregador/";

        public void Listar()
        {
            try
            {
                using (EntregadorBO bo = new EntregadorBO())
                {
                    lista = bo.ListarAtivos();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um erro ao tentar buscar os atendentes" + ex.Message;
            }
        }

        public void PreencherBagDropDownLists(Entregador e = null)
        {
            IList<AparelhoMovel> aparelhos = null;
            try
            {
                using (AparelhoMovelBO bo = new AparelhoMovelBO())
                {
                    aparelhos = bo.ListarDisponiveis();
                }
            }
            catch (BOException ex)
            {
                TempData["flash"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
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
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();

            Listar();
            return View(lista);
        }

        public ActionResult Create()
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();

            PreencherBagDropDownLists();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Entregador entregador)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();

            ValidationHelper.RemoverValidacaoDoModelState(ModelState, 
                "AparelhoMovel.Tipo", "AparelhoMovel.Modelo", "AparelhoMovel.Marca");
            if (!ModelState.IsValid)
            {
                PreencherBagDropDownLists();
                return View(entregador);
            }

            try
            {
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    HttpPostedFileBase postedFile = Request.Files["Foto"];
                    using (EntregadorBO bo = new EntregadorBO())
                    {
                        bo.Incluir(entregador, Server.MapPath("~" + pastaFotos), postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4));
                    }
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
                TempData["ErrorMessage"] = ex.Message;
                return View(entregador);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocorreu um problema, tente novamente. " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(uint id)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();

            Entregador entregador = null;
            try
            {
                using (EntregadorBO bo = new EntregadorBO())
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

            PreencherBagDropDownLists(entregador);
            return View(entregador);
        }

        [HttpPost]
        public ActionResult Edit(Entregador entregador)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();

            ValidationHelper.RemoverValidacaoDoModelState(ModelState,
                "AparelhoMovel.Tipo", "AparelhoMovel.Modelo", "AparelhoMovel.Marca");
            if (!ModelState.IsValid)
            {
                PreencherBagDropDownLists(entregador);
                return View(entregador);
            }
            
            try
            {
                HttpPostedFileBase postedFile = null;
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    if (System.IO.File.Exists(entregador.Foto)) System.IO.File.Delete(entregador.Foto);
                    postedFile = Request.Files["Foto"];
                    entregador.Foto = Server.MapPath("~" + pastaFotos) + String.Format("{0:0000000000}", entregador.Id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                }
                using (EntregadorBO bo = new EntregadorBO())
                {
                    bo.Alterar(entregador);
                }

                if (postedFile != null)
                {
                    postedFile.SaveAs(entregador.Foto);
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

        public ActionResult Delete(uint id)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();

            Entregador entregador = null;
            try
            {
                using (EntregadorBO bo = new EntregadorBO())
                {
                    entregador = bo.BuscarPeloId(id);
                }
                if (System.IO.File.Exists(entregador.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(entregador.Foto);
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

            return View(entregador);
        }

        [HttpPost]
        public ActionResult Delete(Entregador entregador)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();

            try
            {
                using (EntregadorBO bo = new EntregadorBO())
                {
                    entregador = bo.BuscarPeloId(entregador.Id);
                    bo.Excluir(entregador);
                }
                TempData["flash"] = "O entregador \"" + entregador.Nome + "\" foi excluído com sucesso.";
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

        public ActionResult Details(uint id)
        {
            if (!LoginHelper.ValidarTipoUsuarioLogado(this, typeof(Gerente)))
                return new HttpNotFoundResult();

            Entregador entregador = null;
            try
            {
                using (EntregadorBO bo = new EntregadorBO())
                {
                    entregador = bo.BuscarPeloId(id);
                }
                if (System.IO.File.Exists(entregador.Foto))
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(entregador.Foto);
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

            return View(entregador);
        }
    }
}
