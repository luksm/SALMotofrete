using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios.JSON
{
    public class JSONException : Exception
    {
        public JSONException(String mensagem)
            : base(mensagem)
        {

        }
    }
}
