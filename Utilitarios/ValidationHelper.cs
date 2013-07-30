using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public sealed class ValidationHelper
    {
        public static void RemoverValidacaoDoModelState(System.Web.Mvc.ModelStateDictionary modelState, params String[] keys)
        {
            foreach (var item in keys)
            {
                if (modelState.ContainsKey(item)) modelState[item].Errors.Clear();
            }
        }
    }
}
