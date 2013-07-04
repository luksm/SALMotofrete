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
    public class AparelhoMovelBO : BO<AparelhoMovel>
    {
        private ISession sessao;

        public AparelhoMovelBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            AparelhoMovelDAO dao = new AparelhoMovelDAO(sessao);
            base.Dao = dao;
        }

        ~AparelhoMovelBO()
        {
            Dispose();
        }

        public new void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }
    }
}
