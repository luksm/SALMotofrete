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

            using (LoginBO login = new LoginBO())
            {
                pessoa = login.RealizarLogin(pessoa);
            }

            if (pessoa == null)
            {
                ModelState.AddModelError("", "Usuário ou senha inválidos");
                return View(pessoa);
            }

            FormsAuthentication.SetAuthCookie(pessoa.Id.ToString(), false);

            return View();
        }
    }
}
