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
    public class MunicipioBO : BO<Municipio>
    {
        private ISession sessao = null;
        private MunicipioDAO dao = null;

        public MunicipioBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            dao = new MunicipioDAO(sessao);
            base.Dao = dao;
        }

        ~MunicipioBO()
        {
            Dispose();
        }

        public new void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }

        public IList<Municipio> ListarPeloEstado(Estado estado)
        {
            return dao.ListarPeloEstado(estado);
        }
    }
        
}
