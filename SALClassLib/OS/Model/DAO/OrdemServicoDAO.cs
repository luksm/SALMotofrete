using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.DAO;

namespace SALClassLib.OS.Model.DAO
{
    public class OrdemServicoDAO : DAO<OrdemServico>
    {
        public OrdemServicoDAO(ISession sessao)
            : base(sessao)
        {

        }
    }  
}
