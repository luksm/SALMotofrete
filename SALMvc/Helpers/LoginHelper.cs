using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SALMvc.Helpers
{
    public class LoginHelper
    {
        public static bool ValidarTipoUsuarioLogado(Controller controller, params Type[] tipo)
        {
            Pessoa p;
            using (LoginBO bo = new LoginBO())
            {
                p = bo.BuscarPeloUsuario(controller.User.Identity.Name);
            }
            foreach (var item in tipo)
            {
                if (p.GetType().Equals(item))
                    return true;
            }
            return false;
        }

        public static Pessoa GetUsuarioLogado(HttpSessionStateBase session)
        {
            if (session["userLogin"] == null)
                return null;
            else
                return (Pessoa)session["userLogin"];
        }
    }
}