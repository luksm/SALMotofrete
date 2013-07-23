using NHibernate;
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
        IList<Endereco> listaEnderecos = null;

        private void Listar()
        {
            ClienteBO bo = new ClienteBO();
            lista = bo.ListarAtivos();
            bo.Dispose();
        }

        private void ListarEnderecos(Cliente c)
        {
            EnderecoBO bo = new EnderecoBO();
            listaEnderecos = bo.ListarEnderecosDoCliente(c);
            bo.Dispose();
            if (listaEnderecos == null) listaEnderecos = new List<Endereco>();
        }

        private void PreencherBagDropDownLists()
        {
            EstadoBO boe = new EstadoBO();
            IList<Estado> estados = boe.Listar();
            boe.Dispose();
            boe = null;
            var listaEstados = new List<SelectListItem>();
            foreach (var item in estados)
            {
                listaEstados.Add(
                    new SelectListItem()
                    {
                        Text = item.Sigla,
                        Value = item.Id.ToString()
                    }
                );
            }
            ViewBag.Estados = listaEstados;

            MunicipioBO bom = new MunicipioBO();
            IList<Municipio> municipios = bom.Listar();
            bom.Dispose();
            bom = null;
            var listaMunicipios = new List<SelectListItem>();
            foreach (var item in municipios)
            {
                listaMunicipios.Add(
                    new SelectListItem()
                    {
                        Text = item.Nome,
                        Value = item.Id.ToString()
                    }
                );
            }
            ViewBag.Municipios = listaMunicipios;
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
                Session["cliente"] = cliente;
                return RedirectToAction("Enderecos");
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
            return View(cliente);
        }

        //
        // GET: /Cliente/Enderecos/#

        public ActionResult Enderecos()
        {
            try
            {
                Cliente c = (Cliente) Session["cliente"];
                if (c == null) return RedirectToAction("Index");
                ListarEnderecos(c);
                return View(listaEnderecos);
            }
            catch (BOException ex)
            {
                TempData["flash"] = ex.Message;
            }
            /*catch (Exception ex)
            {
                TempData["flash"] = "Ocorreu um erro inesperado. Tente novamente.";
            }*/

            return RedirectToAction("Index");
        }

        //
        // GET: /Cliente/CreateEndereco

        public ActionResult CreateEndereco()
        {
            PreencherBagDropDownLists();
            return View();
        }

        //
        // POST: /Cliente/CreateEndereco

        [HttpPost]
        public ActionResult CreateEndereco(Endereco endereco)
        {
            PreencherBagDropDownLists();

            if (!ModelState.IsValid)
            {
                return View(endereco);
            }

            ClienteBO bo = new ClienteBO();

            try
            {
                Cliente c = (Cliente)Session["cliente"];
                endereco.Pessoa = c.Pessoa;
                c.Pessoa.Enderecos.Add(endereco);
                bo.Alterar(c);
                Session["cliente"] = c;
                return RedirectToAction("Enderecos");
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
            return View(endereco);
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
