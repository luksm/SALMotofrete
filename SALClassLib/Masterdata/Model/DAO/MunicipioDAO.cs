using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.DAO;

namespace SALClassLib.Masterdata.Model.DAO
{
    public class MunicipioDAO : DAO<Municipio>
    {
        public MunicipioDAO(ISession sessao)
            : base(sessao)
        {

        }

        public IList<Municipio> ListarPeloEstado(Estado estado)
        {
            ITransaction tx = Sessao.BeginTransaction();
            IQuery query = Sessao.CreateQuery("from Municipio as mun inner join fetch mun.Estado as est where est.Id=?");
            query.SetInt32(0, estado.Id);
            IList<Municipio> lista = query.List<Municipio>();
            tx.Commit();
            return lista;
        }
    }
        
}
