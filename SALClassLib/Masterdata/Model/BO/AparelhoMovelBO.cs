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

        /// <summary>
        /// Lista os aparelhos moveis que estao disponiveis, ou seja, nao estao vinculados a nenhum entregador.
        /// </summary>
        /// <returns>Lista de aparelhos moveis disponiveis</returns>
        public IList<AparelhoMovel> ListarDisponiveis()
        {
            IList<AparelhoMovel> lista;
            ITransaction tx = Dao.Sessao.BeginTransaction();
            ISQLQuery query = Dao.Sessao.CreateSQLQuery("SELECT {AM.*} FROM TB_APARELHO_MOVEL {AM} " +
                "LEFT JOIN TB_ENTREGADOR E ON AM.ID_APARELHO_MOVEL = E.ID_APARELHO_MOVEL " +
                "WHERE E.ID_PESSOA IS NULL");
            query.AddEntity("AM", typeof(AparelhoMovel));
            lista = query.List<AparelhoMovel>();
            tx.Commit();
            return lista;
        }
    }
}
