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
    public class EntregadorBO : BO<Entregador>
    {
        private ISession sessao = null;
        private EntregadorDAO dao = null;

        public EntregadorBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            dao = new EntregadorDAO(sessao);
            base.Dao = dao;
        }

        ~EntregadorBO()
        {
            Dispose();
        }

        public new void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }

        public override void Excluir(Entregador obj)
        {
            if (obj == null) return;
            obj.StatusExclusao = 1;
            this.Alterar(obj);
        }
    }
        
}
