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
    public class GerenteBO : BO<Gerente>
    {
        private ISession sessao = null;
        private GerenteDAO dao = null;

        public GerenteBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            dao = new GerenteDAO(sessao);
            base.Dao = dao;
        }

        ~GerenteBO()
        {
            Dispose();
        }

        public new void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }

        public override void Excluir(Gerente obj)
        {
            if (obj == null) return;
            obj.StatusExclusao = 1;
            this.Alterar(obj);
        }

        public IList<Gerente> ListarAtivos()
        {
            IList<Gerente> lista;
            ITransaction tx = Dao.Sessao.BeginTransaction();
            IQuery query = Dao.Sessao.CreateQuery("from Gerente where StatusExclusao = 0");
            lista = query.List<Gerente>();
            tx.Commit();
            return lista;
        }
    }
}
