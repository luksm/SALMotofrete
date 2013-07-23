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
    public class EnderecoBO : BO<Endereco>
    {
        private ISession sessao = null;
        private EnderecoDAO dao = null;

        public EnderecoBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            dao = new EnderecoDAO(sessao);
            base.Dao = dao;
        }

        ~EnderecoBO()
        {
            Dispose();
        }

        public new void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }

        public IList<Endereco> ListarEnderecosDoCliente(Cliente cliente)
        {
            if (cliente == null) return null;
            return dao.ListarEnderecosDoCliente(cliente);
        }
    }
        
}
