using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios.DAO
{
    public class DAOException : Exception
    {
        public DAOException(String mensagem)
            : base(mensagem)
        {

        }
    }
}
