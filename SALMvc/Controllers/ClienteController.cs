using NHibernate;
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
    public class ClienteController : Controller
    {
        IList<Cliente> lista = null;
        IList<Endereco> listaEnderecos = null;

        private void Listar()
        {
            try
            {
                using (ClienteBO bo = new ClienteBO())
                {
                    lista = bo.ListarAtivos();
                }
            }
            catch (Exception ex)
            {
                TempData["flash"] = "Ocorreu um erro ao tentar buscar os atendentes";
            }
        }

        private void ListarEnderecos(Cliente c)
        {
            try
            {
                using (EnderecoBO bo = new EnderecoBO())
                {
                    listaEnderecos = bo.ListarEnderecosDoCliente(c);
                }
                if (listaEnderecos == null) listaEnderecos = new List<Endereco>();
            }
            catch(Exception)
            {
                TempData["flash"] = "Ocorreu um erro ao tentar buscar os endereços";
            }
        }

        private void PreencherBagDropDownLists()
        {
            IList<Estado> estados = null;
            try
            {
                using (EstadoBO boe = new EstadoBO())
                {
                    estados = boe.Listar();
                }
            }
            catch (Exception)
            {
                return;
            }

            var listaEstados = new List<SelectListItem>();
            listaEstados.Add(
                    new SelectListItem()
                    {
                        Text = "-- Selecione --",
                        Value = "0"
                    }
                );
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
        }

        [HttpPost]
        public ActionResult BuscarMunicipios(ushort idEstado)
        {
            try
            {
                using (MunicipioBO bom = new MunicipioBO())
                {
                    return Json(bom.ListarPeloEstado(new Estado() { Id = idEstado }));
                }
            }
            catch (Exception)
            {
                return null;
            }
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
            ValidationHelper.RemoverValidacaoDoModelState(ModelState, "PessoaFisica.Usuario", "PessoaFisica.Senha");
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            try
            {
                using (ClienteBO bo = new ClienteBO())
                {
                    bo.Incluir(cliente);
                }
                Session["cliente"] = cliente;
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
            ValidationHelper.RemoverValidacaoDoModelState(ModelState, "PessoaJuridica.Usuario", "PessoaJuridica.Senha");
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            try
            {
                using (ClienteBO bo = new ClienteBO())
                {
                    bo.Incluir(cliente);
                }
                Session["cliente"] = cliente;
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
            
            return View(cliente);
        }

        //
        // GET: /Cliente/Edit/#

        public ActionResult Edit(uint id)
        {
            Cliente cliente = new Cliente();
            using (ClienteBO bo = new ClienteBO())
            {
                cliente = bo.BuscarPeloId(id);
            }
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
            ValidationHelper.RemoverValidacaoDoModelState(ModelState, "PessoaFisica.Usuario", "PessoaFisica.Senha");
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            try
            {
                ListarEnderecos(cliente);
                cliente.Pessoa.Enderecos = new Iesi.Collections.Generic.HashedSet<Endereco>(listaEnderecos);
                using (ClienteBO bo = new ClienteBO())
                {
                    bo.Alterar(cliente);
                }
                Session["cliente"] = cliente;
                return RedirectToAction("Enderecos");
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
            ValidationHelper.RemoverValidacaoDoModelState(ModelState, "PessoaJuridica.Usuario", "PessoaJuridica.Senha");
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            try
            {
                using (ClienteBO bo = new ClienteBO())
                {
                    bo.Alterar(cliente);
                }
                Session["cliente"] = cliente;
                return RedirectToAction("Enderecos");
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
            Cliente cliente = null;
            try
            {
                using (ClienteBO bo = new ClienteBO())
                {
                    cliente = bo.BuscarPeloId(id);
                }
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
        public ActionResult DeletePF(Cliente cliente)
        {
            try
            {
                using (ClienteBO bo = new ClienteBO())
                {
                    cliente = bo.BuscarPeloId(cliente.Id);
                    bo.Excluir(cliente);
                }
            }
            catch (Exception)
            {
                Listar();
                return View("Index", lista);
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
        public ActionResult DeletePJ(Cliente cliente)
        {
            try
            {
                using (ClienteBO bo = new ClienteBO())
                {
                    cliente = bo.BuscarPeloId(cliente.Id);
                    bo.Excluir(cliente);
                }
            }
            catch (Exception)
            {
                Listar();
                return View("Index", lista);
            }

            Listar();
            return View("Index", lista);
        }

        //
        // GET: /Cliente/Details/#

        public ActionResult Details(uint id)
        {
            Cliente cliente = new Cliente();
            using (ClienteBO bo = new ClienteBO())
            {
                cliente = bo.BuscarPeloId(id);
            }
            
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
            catch (Exception ex)
            {
                TempData["flash"] = "Ocorreu um erro inesperado. Tente novamente.";
            }

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

            if (endereco.Municipio.Estado.Id == 0)
            {
                ModelState["Municipio.Estado.Id"].Errors.Add("Selecione um estado");
                return View(endereco);
            }

            if (Request.Form["selMunicipio"] == null || Request.Form["selMunicipio"].Equals("0"))
            {
                ViewBag.ErrMunicipio = "Selecione um município";
                return View(endereco);
            }

            try
            {
                endereco.Municipio.Id = Convert.ToUInt32(Request.Form["selMunicipio"]);
                Cliente c = (Cliente)Session["cliente"];
                c.Pessoa.Enderecos.Add(endereco);
                endereco.Pessoa = c.Pessoa;
                using (EnderecoBO bo = new EnderecoBO())
                {
                    bo.Incluir(endereco);
                }
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

            return View(endereco);
        }

        //
        // GET: /Cliente/EditEndereco/#

        public ActionResult EditEndereco(uint id)
        {
            PreencherBagDropDownLists();
            Endereco endereco = null;

            try
            {
                using (EnderecoBO bo = new EnderecoBO())
                {
                    endereco = bo.BuscarPeloId(id);
                }
                ViewBag.IdMunicipio = endereco.Municipio.Id;
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                TempData["flash"] = "Ocorreu um problema, tente novamente.";
                return RedirectToAction("Index");
            }

            return View(endereco);
        }

        //
        // POST: /Cliente/EditEndereco/#

        [HttpPost]
        public ActionResult EditEndereco(Endereco endereco)
        {
            PreencherBagDropDownLists();
            ValidationHelper.RemoverValidacaoDoModelState(ModelState, "Pessoa.Usuario", "Pessoa.Senha");

            if (!ModelState.IsValid || Session["cliente"] == null)
            {
                ViewBag.IdMunicipio = Request.Form["selMunicipio"];
                return View(endereco);
            }

            if (endereco.Municipio.Estado.Id == 0)
            {
                ModelState["Municipio.Estado.Id"].Errors.Add("Selecione um estado");
                return View(endereco);
            }

            if (Request.Form["selMunicipio"] == null || Request.Form["selMunicipio"].Equals("0"))
            {
                ViewBag.ErrMunicipio = "Selecione um município";
                return View(endereco);
            }

            try
            {
                endereco.Municipio.Id = Convert.ToUInt32(Request.Form["selMunicipio"]);
                Cliente cliente = (Cliente)Session["cliente"];
                Endereco aux = cliente.Pessoa.Enderecos.Where(e => e.Id == endereco.Id).First();
                cliente.Pessoa.Enderecos.Remove(aux);
                cliente.Pessoa.Enderecos.Add(endereco);
                using (EnderecoBO bo = new EnderecoBO())
                {
                    bo.Alterar(endereco);
                }
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

            return View(endereco);
        }

        //
        // GET: /Cliente/DeleteEndereco/#

        public ActionResult DeleteEndereco(uint id)
        {
            if (!ModelState.IsValid || Session["cliente"] == null)
            {
                return RedirectToAction("Enderecos");
            }

            try
            {
                Cliente cliente = (Cliente)Session["cliente"];
                using (EnderecoBO endBO = new EnderecoBO())
                {
                    Endereco endereco = endBO.BuscarPeloId(id);
                    endBO.Excluir(endereco);
                    cliente.Pessoa.Enderecos.Remove(endereco);
                }
            }
            catch (BOException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um problema, tente novamente.");
            }

            return RedirectToAction("Enderecos");
        }

        #endregion
    }
}
