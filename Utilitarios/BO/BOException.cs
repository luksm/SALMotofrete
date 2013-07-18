using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios.BO
{
    public class BOException : Exception
    {
        public BOException(String mensagem)
            : base(mensagem)
        {
            
        }
    }
}
