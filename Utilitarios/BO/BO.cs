using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.DAO;

namespace Utilitarios.BO
{
    public abstract class BO<T> : IDisposable
    {
        private DAO<T> dao = null;

        protected DAO<T> Dao
        {
            get { return dao; }
            set { dao = value; }
        }
        

        public ulong Incluir(T obj)
        {
            ITransaction tx = dao.Sessao.BeginTransaction();
            ulong id = dao.Incluir(obj);
            tx.Commit();
            return id;
        }

        public IList<T> Listar()
        {
            ITransaction tx = dao.Sessao.BeginTransaction();
            IList<T> lista = dao.Listar();
            tx.Commit();
            return lista;
        }

        public T BuscarPeloId(Object id)
        {
            ITransaction tx = dao.Sessao.BeginTransaction();
            T obj = dao.BuscarPeloId(id);
            tx.Commit();
            return obj;
        }

        public void Alterar(T obj)
        {
            ITransaction tx = dao.Sessao.BeginTransaction();
            dao.Alterar(obj);
            tx.Commit();
        }

        public void Excluir(T obj)
        {
            ITransaction tx = dao.Sessao.BeginTransaction();
            dao.Excluir(obj);
            tx.Commit();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
