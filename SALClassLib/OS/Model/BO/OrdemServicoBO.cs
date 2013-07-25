using NHibernate;
using SALClassLib.OS.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.BO;

namespace SALClassLib.OS.Model.BO
{
    public class OrdemServicoBO : BO<OrdemServico>
    {
        private ISession sessao;

        public OrdemServicoBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            OrdemServicoDAO dao = new OrdemServicoDAO(sessao);
            base.Dao = dao;
        }

        ~OrdemServicoBO()
        {
            Dispose();
        }

        public new void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }
    }
}
