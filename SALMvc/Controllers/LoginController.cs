using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SALMvc.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Pessoa pessoa, String returnUrl)
        {
            if (pessoa.Usuario.Equals("") || pessoa.Senha.Equals(""))
            {
                ModelState.AddModelError("", "Usuário ou senha inválidos");
                return View(pessoa);
            }

            Pessoa p = null;

            using (LoginBO login = new LoginBO())
            {
                p = login.RealizarLogin(pessoa);
            }

            if (p == null)
            {
                ModelState.AddModelError("", "Usuário ou senha inválidos");
                return View(pessoa);
            }

            FormsAuthentication.SetAuthCookie(p.Usuario, false);

            if (returnUrl == null)
                return RedirectToAction("Index", "Home");
            else
                return Redirect(returnUrl);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }
    }
}
