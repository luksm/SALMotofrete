using NHibernate;
using SALClassLib.Masterdata.Model.DAO;
using SALClassLib.OS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.BO;

namespace SALClassLib.Masterdata.Model.BO
{
    public class EntregadorBO : BO<Entregador>
    {
        private ISession sessao = null;
        private EntregadorDAO dao = null;

        public EntregadorBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            dao = new EntregadorDAO(sessao);
            base.Dao = dao;
        }

        ~EntregadorBO()
        {
            Dispose();
        }

        public override void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }

        public ulong Incluir(Entregador obj, String caminhoFotos = null, String extensaoFoto = null)
        {
            obj.StatusExclusao = 0;
            ulong id = 0;
            using (ITransaction tx = dao.Sessao.BeginTransaction())
            {
                id = Convert.ToUInt64(dao.Sessao.Save(obj));
                //após incluir o atendente, se houver foto, faz o cadastro com o caminho da foto
                if (caminhoFotos != null && !caminhoFotos.Equals("") &&
                    extensaoFoto != null && !extensaoFoto.Equals(""))
                {
                    //adiciona \ no final do caminho caso nao tenha
                    if (!caminhoFotos.EndsWith("\\")) caminhoFotos += "\\";
                    //a foto ficará na pasta informada, com o ID formatado com 10 digitos como nome do arquivo
                    obj.Foto = caminhoFotos + String.Format("{0:0000000000}", id) + extensaoFoto;
                    dao.Sessao.Update(obj);
                }
                tx.Commit();
            }
            return id;
        }

        public override void Excluir(Entregador obj)
        {
            if (obj == null) return;
            obj.StatusExclusao = 1;
            obj.AparelhoMovel = null;
            this.Alterar(obj);
        }

        public IList<Entregador> ListarAtivos()
        {
            IList<Entregador> lista;
            ITransaction tx = Dao.Sessao.BeginTransaction();
            IQuery query = Dao.Sessao.CreateQuery("from Entregador where StatusExclusao = 0");
            lista = query.List<Entregador>();
            tx.Commit();
            return lista;
        }

        public void AtribuirOSAoEntregador(OrdemServico os, Entregador entregador)
        {
            using (ITransaction tx = sessao.BeginTransaction())
            {
                OrdemServico osAux = sessao.Get<OrdemServico>(os.Id);
                Entregador eAux = sessao.Get<Entregador>(entregador.Id);
                osAux.Entregador = eAux;
                osAux.Status = new StatusOrdemServico() { Id=2 };
                sessao.Update(osAux);
                tx.Commit();
            }
        }
    }
}
