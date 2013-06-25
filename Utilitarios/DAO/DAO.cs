using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Utilitarios.DAO
{
    public abstract class DAO<T> : IDAO<T>
    {
        private ISession sessao;

        protected ISession Sessao { get { return sessao; } }

        public DAO(ISession sessao)
        {
            this.sessao = sessao;
        }

        public ISet<T> Listar()
        {
            throw new NotImplementedException();
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
