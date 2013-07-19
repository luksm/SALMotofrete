using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.DAO;

namespace SALClassLib.Masterdata.Model.DAO
{
    public class ClienteDAO : DAO<Cliente>
    {
        public ClienteDAO(ISession sessao)
            : base(sessao)
        {

        }

        public IList<Cliente> ListarAtivos()
        {
            IList<Cliente> lista;
            ITransaction tx = Sessao.BeginTransaction();
            IQuery query = Sessao.CreateQuery("from Cliente where Pessoa.StatusExclusao = 0");
            lista = query.List<Cliente>();
            tx.Commit();
            return lista;
        }
    }
}