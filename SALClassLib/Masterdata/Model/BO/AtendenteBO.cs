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
    public class AtendenteBO : BO<Atendente>
    {
        private ISession sessao = null;
        private AtendenteDAO dao = null;

        public AtendenteBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            dao = new AtendenteDAO(sessao);
            base.Dao = dao;
        }

        ~AtendenteBO()
        {
            Dispose();
        }

        public new void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }
    }
}
