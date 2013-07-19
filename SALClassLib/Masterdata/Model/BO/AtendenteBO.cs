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

        public override void Excluir(Atendente obj)
        {
            if (obj == null) return;
            obj.StatusExclusao = 1;
            this.Alterar(obj);
        }
        /// <summary>
        /// Retorna todos os Atendentes com StatusExclusão zerado
        /// </summary>
        /// <returns>Ilist Listagem de atendentes</returns>
        public IList<Atendente> ListarAtivos()
        {
            IList<Atendente> lista;
            ITransaction tx = Dao.Sessao.BeginTransaction();
            IQuery query = Dao.Sessao.CreateQuery("from Atendente where StatusExclusao = 0");
            lista = query.List<Atendente>();
            tx.Commit();
            return lista;
        }

    }
}
