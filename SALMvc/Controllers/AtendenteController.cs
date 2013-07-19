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
        String photoPath = "~/Uploads/Fotos/Funcionarios/Atendente/";

        public void Listar()
        {
            AtendenteBO bo = new AtendenteBO();
            lista = bo.ListarAtivos();
            bo.Dispose();
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

            AtendenteBO bo = new AtendenteBO();

            //try
            //{
                ulong id = bo.Incluir(atendente);
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    HttpPostedFileBase postedFile = Request.Files["Foto"];
                    atendente.Foto = Server.MapPath(photoPath) + String.Format("{0:0000000000}", id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                    postedFile.SaveAs(atendente.Foto);
                    bo.Alterar(atendente);
                }
                TempData["flash"] = "Seu cadastro foi realisado com sucesso.";
            /*}
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente." + ex.Message);
            }
            finally
            { */
                if (bo != null)
                    bo.Dispose();
            //}

            return RedirectToAction("Index");
        }

        //
        // GET: /Atendente/Details/#
        public ActionResult Details(uint Id)
        {
            AtendenteBO bo = new AtendenteBO();
            Atendente am = new Atendente();
            am = bo.BuscarPeloId(Id);
            bo.Dispose();
            return View(am);
        }

        //
        // GET: /Atendente/Edit/#
        public ActionResult Edit(uint Id)
        {
            AtendenteBO bo = new AtendenteBO();
            Atendente am = new Atendente();
            am = bo.BuscarPeloId(Id);
            bo.Dispose();
            return View(am);
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

            AtendenteBO bo = new AtendenteBO();
            try
            {
                HttpPostedFileBase postedFile = null;
                if (!Request.Files["Foto"].FileName.Equals(""))
                {
                    if (System.IO.File.Exists(atendente.Foto)) System.IO.File.Delete(atendente.Foto);
                    postedFile = Request.Files["Foto"];
                    atendente.Foto = Server.MapPath(photoPath) + String.Format("{0:0000000000}", atendente.Id) + postedFile.FileName.Substring(postedFile.FileName.Length - 4, 4);
                }
                bo.Alterar(atendente);
                if (postedFile != null)
                {
                    postedFile.SaveAs(atendente.Foto);
                }
                bo.Dispose();
                TempData["flash"] = "Seu cadastro foi realisado com sucesso.";
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

            return RedirectToAction("Index");
        }

        //
        // GET: /AparelhoMovel/Delete/#
        public ActionResult Delete(uint Id)
        {
            AtendenteBO bo = new AtendenteBO();
            try
            {
                Atendente a = new Atendente();
                a = bo.BuscarPeloId(Id);
                String nome = a.Nome;
                bo.Excluir(a);
                bo.Dispose();
                TempData["flash"] = "O atendente \"" + nome + " \" foi excluido com sucesso.";
            }
            catch
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
            }
            return RedirectToAction("Index");
        }
    }
}
