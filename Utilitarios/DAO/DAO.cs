using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Utilitarios.DAO
{
    public class DAO<T> : IDAO<T>
    {
        private ISession sessao;

        protected ISession Sessao { get { return sessao; } }

        public DAO(ISession sessao)
        {
            this.sessao = sessao;
        }

        public IList<T> Listar()
        {
            ICriteria criteria = sessao.CreateCriteria(typeof(T));
            criteria.SetMaxResults(1000);
            return criteria.List<T>();
        }

        public T BuscarPeloId(Object id)
        {
            return sessao.Get<T>(id);
        }

        public ulong Incluir(T obj)
        {
            return Convert.ToUInt64(sessao.Save(obj));
        }

        public void Alterar(T obj)
        {
            sessao.Update(obj);
        }

        public void Excluir(T obj)
        {
            sessao.Delete(obj);
        }
    }
}
