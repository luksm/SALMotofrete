using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model.DAO
{
    public class TipoAparelhoMovelDAO
    {
        private ISession sessao = null;
        public TipoAparelhoMovelDAO(ISession sessao)
        {
            this.sessao = sessao;
        }

        public IList<TipoAparelhoMovel> Listar()
        {
            return sessao.CreateQuery("from TipoAparelhoMovel").List<TipoAparelhoMovel>();
        }
    }
}
