using NHibernate;
using SALClassLib.Masterdata.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.BO;

namespace SALClassLib.Masterdata.Model.BO
{
    public class TipoAparelhoMovelBO : BO<TipoAparelhoMovel>
    {
        private ISession sessao;

        public TipoAparelhoMovelBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            TipoAparelhoMovelDAO dao = new TipoAparelhoMovelDAO(sessao);
            base.Dao = dao;
        }

        ~TipoAparelhoMovelBO()
        {
            Dispose();
        }

        public new void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }
    }
}
