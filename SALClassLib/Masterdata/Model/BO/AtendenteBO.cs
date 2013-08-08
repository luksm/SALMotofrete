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
    public class AtendenteBO : BO<Atendente>
    {
        private ISession sessao = null;
        private AtendenteDAO dao = null;

        public AtendenteBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            dao = new AtendenteDAO(sessao);
            base.Dao = dao;
        }

        ~AtendenteBO()
        {
            Dispose();
        }

        public override void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }

        public ulong Incluir(Atendente obj, String caminhoFotos = null, String extensaoFoto = null)
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

        public override void Excluir(Atendente obj)
        {
            if (obj == null) return;
            obj.StatusExclusao = 1;
            this.Alterar(obj);
        }
        /// <summary>
        /// Retorna todos os Atendentes com StatusExclusão zerado
        /// </summary>
        /// <returns>Ilist Listagem de atendentes</returns>
        public IList<Atendente> ListarAtivos()
        {
            IList<Atendente> lista;
            ITransaction tx = Dao.Sessao.BeginTransaction();
            IQuery query = Dao.Sessao.CreateQuery("from Atendente where StatusExclusao = 0");
            lista = query.List<Atendente>();
            tx.Commit();
            return lista;
        }

    }
}
