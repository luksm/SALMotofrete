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
    public class GerenteBO : BO<Gerente>
    {
        private ISession sessao = null;
        private GerenteDAO dao = null;

        public GerenteBO()
        {
            sessao = NHibernateHelper.GetCurrentSession();
            dao = new GerenteDAO(sessao);
            base.Dao = dao;
        }

        ~GerenteBO()
        {
            Dispose();
        }

        public override void Dispose()
        {
            NHibernateHelper.CloseSession(sessao);
        }

        public ulong Incluir(Gerente obj, String caminhoFotos = null, String extensaoFoto = null)
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

        public override void Excluir(Gerente obj)
        {
            if (obj == null) return;
            obj.StatusExclusao = 1;
            this.Alterar(obj);
        }

        public IList<Gerente> ListarAtivos()
        {
            IList<Gerente> lista;
            ITransaction tx = Dao.Sessao.BeginTransaction();
            IQuery query = Dao.Sessao.CreateQuery("from Gerente where StatusExclusao = 0");
            lista = query.List<Gerente>();
            tx.Commit();
            return lista;
        }
    }
}
