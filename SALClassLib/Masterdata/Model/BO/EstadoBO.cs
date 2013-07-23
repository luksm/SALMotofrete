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
    public class EstadoBO : BO<Estado>
    {
        private ISession sessao = null;
        private EstadoDAO dao = null;

        public EstadoBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            dao = new EstadoDAO(sessao);
            base.Dao = dao;
        }

        ~EstadoBO()
        {
            Dispose();
        }

        public new void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }
    }
        
}
