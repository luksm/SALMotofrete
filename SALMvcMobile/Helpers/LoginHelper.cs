using SALClassLib.Masterdata.Model;
using SALClassLib.Masterdata.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SALMvc.Helpers
{
    public class LoginHelper
    {
        public static bool ValidarTipoUsuarioLogado(Controller controller, params Type[] tipo)
        {
            Pessoa p = GetUsuarioLogado(controller);
            if (p == null) return false;

            foreach (var item in tipo)
            {
                if (p.GetType().Equals(item))
                    return true;
            }
            return false;
        }

        public static Pessoa GetUsuarioLogado(Controller controller)
        {
            Pessoa pessoa = null;
            using (LoginBO bo = new LoginBO())
            {
                pessoa = bo.BuscarPeloUsuario(controller.User.Identity.Name);
            }
            return pessoa;
        }
    }
}