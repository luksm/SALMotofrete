using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model.BO
{
    public class LoginBO : IDisposable
    {
        private ISession sessao = null;

        public  LoginBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
        }

        ~ LoginBO()
        {
            Dispose();
        }

        public void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }

        public Pessoa RealizarLogin(Pessoa pessoa)
        {
            Pessoa p = null;
            using (ITransaction tx = sessao.BeginTransaction())
            {
                ICriteria crit = sessao.CreateCriteria("Pessoa");
                crit.Add(Restrictions.Eq("Usuario", pessoa.Usuario));
                crit.Add(Restrictions.Eq("Senha", pessoa.Senha));
                IList<Pessoa> lista = crit.List<Pessoa>();
                if (lista.Count > 0)
                    p = lista.First();
            }
            return p;
        }

        public Pessoa BuscarPeloUsuario(String usuario)
        {
            Pessoa p = null;
            using (ITransaction tx = sessao.BeginTransaction())
            {
                ICriteria crit = sessao.CreateCriteria("Pessoa");
                crit.Add(Restrictions.Eq("Usuario", usuario));
                IList<Pessoa> lista = crit.List<Pessoa>();
                if (lista.Count > 0)
                    p = lista.First();
            }
            return p;
        }
    }
}
