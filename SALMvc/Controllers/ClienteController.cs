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

        #region Cliente
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
        // GET: /Cliente/CreatePJ

        public ActionResult CreatePJ()
        {
            return View();
        }

        //
        // POST: /Cliente/CreatePJ

        [HttpPost]
        public ActionResult CreatePJ(Cliente cliente)
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
        // GET: /Cliente/Edit/#

        public ActionResult Edit(uint id)
        {
            ClienteBO bo = new ClienteBO();
            Cliente cliente = new Cliente();
            cliente = bo.BuscarPeloId(id);
            bo.Dispose();
            Session["cliente"] = cliente;
            if (cliente.Pessoa is PessoaFisica)
            {
                return RedirectToAction("EditPF");
            }
            if (cliente.Pessoa is PessoaJuridica)
            {
                return RedirectToAction("EditPJ");
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Cliente/EditPF/#
        
        public ActionResult EditPF()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return View(cliente);
        }

        //
        // POST: /Cliente/EditPF/#

        [HttpPost]
        public ActionResult EditPF(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            ClienteBO bo = null;
            try
            {
                bo = new ClienteBO();
                bo.Alterar(cliente);
                Session["cliente"] = cliente;
                return RedirectToAction("Enderecos");
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            /*catch(Exception)
            {
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente.");
            }*/
            finally
            {
                if(bo != null)
                    bo.Dispose();
            }
            Listar();
            return View("Index", lista);
        }

        //
        // GET: /Cliente/EditPJ/#

        public ActionResult EditPJ()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return View(cliente);
        }

        //
        // POST: /Cliente/EditPJ/#

        [HttpPost]
        public ActionResult EditPJ(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            ClienteBO bo = null;
            try
            {
                bo = new ClienteBO();
                bo.Alterar(cliente);
                Session["cliente"] = cliente;
                return RedirectToAction("Enderecos");
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            /*catch(Exception)
            {
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente.");
            }*/
            finally
            {
                if (bo != null)
                    bo.Dispose();
            }
            Listar();
            return View("Index", lista);
        }

        //
        // GET: /Cliente/Delete/#

        public ActionResult Delete(uint id)
        {
            ClienteBO bo = new ClienteBO();
            Cliente cliente = null;
            try
            {
                cliente = bo.BuscarPeloId(id);
                Session["cliente"] = cliente;
                if (cliente.Pessoa is PessoaFisica)
                {
                    return RedirectToAction("DeletePF");
                }
                else if (cliente.Pessoa is PessoaJuridica)
                {
                    return RedirectToAction("DeletePJ");
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
            return View(cliente);
        }

        //
        // GET: /Cliente/DeletePF/#

        public ActionResult DeletePF()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return View(cliente);
        }

        //
        // POST: /Cliente/DeletePF/#

        [HttpPost]
        public ActionResult DeletePF(Cliente entregador)
        {
            ClienteBO bo = new ClienteBO();
            try
            {
                entregador = bo.BuscarPeloId(entregador.Id);
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
        // GET: /Cliente/DeletePJ/#

        public ActionResult DeletePJ()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return View(cliente);
        }

        //
        // POST: /Cliente/DeletePJ/#

        [HttpPost]
        public ActionResult DeletePJ(Cliente entregador)
        {
            ClienteBO bo = new ClienteBO();
            try
            {
                entregador = bo.BuscarPeloId(entregador.Id);
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
            Cliente cliente = new Cliente();
            cliente = bo.BuscarPeloId(id);
            bo.Dispose();
            Session["cliente"] = cliente;
            if (cliente.Pessoa is PessoaFisica)
            {
                return RedirectToAction("DetailsPF");
            }
            if (cliente.Pessoa is PessoaJuridica)
            {
                return RedirectToAction("DetailsPJ");
            }
            return RedirectToAction("Index");
        }

        public ActionResult DetailsPF()
        {
            Cliente cliente = (Cliente) Session["cliente"];
            return View(cliente);
        }

        public ActionResult DetailsPJ()
        {
            Cliente cliente = (Cliente)Session["cliente"];
            return View(cliente);
        }

        #endregion

        #region Endereco
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
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente. " + ex.Message);
            }
            finally
            {
                if (bo != null)
                    bo.Dispose();
            }
            return View(endereco);
        }

        //
        // GET: /Cliente/EditEndereco/#

        public ActionResult EditEndereco(uint id)
        {
            PreencherBagDropDownLists();
            EnderecoBO bo = null;
            Endereco endereco = null;

            try
            {
                bo = new EnderecoBO();
                endereco = bo.BuscarPeloId(id);
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente. " + ex.Message);
            }
            finally
            {
                if (bo != null)
                    bo.Dispose();
            }
            return View(endereco);
        }

        //
        // POST: /Cliente/EditEndereco/#

        [HttpPost]
        public ActionResult EditEndereco(Endereco endereco)
        {
            PreencherBagDropDownLists();
            ClienteBO bo = null;

            if (!ModelState.IsValid || Session["cliente"] == null)
            {
                return View(endereco);
            }

            try
            {
                Cliente cliente = (Cliente)Session["cliente"];
                bo = new ClienteBO();
                Endereco aux = cliente.Pessoa.Enderecos.Where(e => e.Id == endereco.Id).First();
                cliente.Pessoa.Enderecos.Remove(aux);
                cliente.Pessoa.Enderecos.Add(endereco);
                bo.Alterar(cliente);
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
            return View(endereco);
        }

        //
        // GET: /Cliente/DeleteEndereco/#

        public ActionResult DeleteEndereco(uint id)
        {
            EnderecoBO endBO = null;

            if (!ModelState.IsValid || Session["cliente"] == null)
            {
                return RedirectToAction("Enderecos");
            }

            try
            {
                Cliente cliente = (Cliente)Session["cliente"];
                endBO = new EnderecoBO();
                Endereco endereco = endBO.BuscarPeloId(id);
                endBO.Excluir(endereco);
                endereco = cliente.Pessoa.Enderecos.Where(e => e.Id == endereco.Id).First();
                cliente.Pessoa.Enderecos.Remove(endereco);
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
                if (endBO != null)
                    endBO.Dispose();
            }
            return RedirectToAction("Enderecos");
        }

        #endregion
    }
}
