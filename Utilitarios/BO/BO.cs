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
        
        /// <summary>
        /// Inclui um novo registro do objeto
        /// </summary>
        /// <param name="obj">Objeto a ser salvo na base</param>
        /// <returns>Id do objeto incluido</returns>
        public virtual ulong Incluir(T obj)
        {
            ITransaction tx = dao.Sessao.BeginTransaction();
            ulong id = dao.Incluir(obj);
            tx.Commit();
            return id;
        }

        /// <summary>
        /// Retona uma lista de objetos
        /// </summary>
        /// <returns>List de objetos</returns>
        public virtual IList<T> Listar()
        {
            ITransaction tx = dao.Sessao.BeginTransaction();
            IList<T> lista = dao.Listar();
            tx.Commit();
            return lista;
        }
        /// <summary>
        /// Busca registros pelo # do Id
        /// </summary>
        /// <param name="id"># do id do registro desejado</param>
        /// <returns>Object com o id desejado ou NULL</returns>
        public virtual T BuscarPeloId(Object id)
        {
            ITransaction tx = dao.Sessao.BeginTransaction();
            T obj = dao.BuscarPeloId(id);
            tx.Commit();
            return obj;
        }
        /// <summary>
        /// Altera o registro do objeto
        /// </summary>
        /// <param name="obj">Objeto com os dados a serem alterados</param>
        public virtual void Alterar(T obj)
        {
            ITransaction tx = dao.Sessao.BeginTransaction();
            dao.Alterar(obj);
            tx.Commit();
        }
        /// <summary>
        /// Remove o registro do objeto
        /// </summary>
        /// <param name="obj">Objeto a ser removido</param>
        public virtual void Excluir(T obj)
        {
            ITransaction tx = dao.Sessao.BeginTransaction();
            dao.Excluir(obj);
            tx.Commit();
        }

        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
