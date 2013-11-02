using NHibernate;
using NHibernate.Criterion;
using SALClassLib.Masterdata.Model;
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

        public override void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }

        public override ulong Incluir(OrdemServico obj)
        {
            ulong id;
            using (ITransaction tx = sessao.BeginTransaction())
            {
                Cobranca c = new Cobranca();
                c.Valor = 0;
                obj.Cobranca = c;
                id = (ulong) sessao.Save(obj);
                tx.Commit();
            }
            return id;
        }

        public void AlterarStatusParaEmRetirada(OrdemServico os)
        {
            if (os.Status.Id != (uint)EStatusOS.EmAguardo)
            {
                throw new BOException("A OS não pode ir para \"Em Retirada\" a partir de seu status atual");
            }

            using (ITransaction tx = sessao.BeginTransaction())
            {
                OrdemServico osAux = sessao.Get<OrdemServico>(os.Id);
                StatusOrdemServico status = sessao.Get<StatusOrdemServico>((uint)EStatusOS.EmRetirada);
                osAux.Status = status;
                sessao.Update(osAux);
                tx.Commit();
            }
        }

        public void AlterarStatusParaACaminhoDaEntrega(OrdemServico os)
        {
            if (os.Status.Id != (uint)EStatusOS.EmRetirada)
            {
                throw new BOException("A OS não pode ir para \"A Caminho da Entrega\" a partir de seu status atual");
            }

            using (ITransaction tx = sessao.BeginTransaction())
            {
                OrdemServico osAux = sessao.Get<OrdemServico>(os.Id);
                StatusOrdemServico status = sessao.Get<StatusOrdemServico>((uint)EStatusOS.ACaminhoDaEntrega);
                osAux.Status = status;
                sessao.Update(osAux);
                tx.Commit();
            }
        }

        public void AlterarStatusParaFinalizada(OrdemServico os)
        {
            if (os.Status.Id != (uint)EStatusOS.ACaminhoDaEntrega)
            {
                throw new BOException("A OS não pode ir para \"Finalizada\" a partir de seu status atual");
            }

            using (ITransaction tx = sessao.BeginTransaction())
            {
                OrdemServico osAux = sessao.Get<OrdemServico>(os.Id);
                StatusOrdemServico status = sessao.Get<StatusOrdemServico>((uint)EStatusOS.Finalizada);
                osAux.Status = status;
                sessao.Update(osAux);
                tx.Commit();
            }
        }

        public void AlterarStatusParaCancelada(OrdemServico os)
        {
            if (os.Status.Id != (uint)EStatusOS.EmAguardo)
            {
                throw new BOException("A OS não pode ser cancelada a partir de seu status atual");
            }

            using (ITransaction tx = sessao.BeginTransaction())
            {
                OrdemServico osAux = sessao.Get<OrdemServico>(os.Id);
                StatusOrdemServico status = sessao.Get<StatusOrdemServico>((uint)EStatusOS.Cancelada);
                osAux.Status = status;
                sessao.Update(osAux);
                tx.Commit();
            }
        }

        public OrdemServico BuscarOSDoEntregador(Entregador entregador)
        {
            OrdemServico os = null;
            using (ITransaction tx = sessao.BeginTransaction())
            {
                IQuery query = sessao.CreateQuery("from OrdemServico as os join fetch os.Entregador as ent join fetch " +
                    "os.Status as status join fetch os.EnderecoEntrega as endent join fetch os.EnderecoRetirada as endret join fetch " +
                    "os.Cobranca as cob join fetch endent.Municipio as entmun join fetch entmun.Estado as est1 join fetch " +
                    "endret.Municipio as retmun join fetch retmun.Estado as est2 " +
                    "where ent.Id=" + entregador.Id + " and status.Id in (2,3)");
                IList<OrdemServico> oss = query.List<OrdemServico>();
                if (oss.Count > 0)
                {
                    os = oss.First();
                }
                tx.Commit();
            }

            return os;
        }

        public IList<OrdemServico> BuscarOSsDisponiveisParaEntregadores()
        {
            IList<OrdemServico> ordensServico = null;
            using (ITransaction tx = sessao.BeginTransaction())
            {
                ICriteria crit = sessao.CreateCriteria(typeof(OrdemServico));
                crit.CreateAlias("Status", "sts");
                crit.Add(Restrictions.Eq("sts.Id", (uint)1));
                crit.Add(Restrictions.IsNull("Entregador"));
                ordensServico = crit.List<OrdemServico>();
            }
            return ordensServico;
        }

        public IList<OrdemServico> BuscarPeloEntregador(Entregador entregador)
        {
            IList<OrdemServico> ordensServico = null;
            using (ITransaction tx = sessao.BeginTransaction())
            {
                ICriteria crit = sessao.CreateCriteria(typeof(OrdemServico));
                crit.CreateAlias("Entregador", "ent");
                crit.Add(Restrictions.Eq("ent.Id", entregador.Id));
                ordensServico = crit.List<OrdemServico>();
            }
            return ordensServico;
        }
    }
}
