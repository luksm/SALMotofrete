using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.Masterdata.Model;
using Utilitarios.DAO;
using NHibernate;

namespace SALClassLib.Masterdata.Model.DAO
{
    public class AparelhoMovelDAO : DAO<AparelhoMovel>
    {
        public AparelhoMovelDAO(ISession sessao)
            : base(sessao)
        {

        }
    }
}
