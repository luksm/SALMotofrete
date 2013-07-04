using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using SALClassLib.Masterdata.Model.DAO;
using SALClassLib.Masterdata.Model;
using System.Collections.Generic;
using Utilitarios.DAO;

namespace SALUnitTest
{
    [TestClass]
    public class TestesMasterdata
    {
        static TestesMasterdata()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ITransaction tx = session.BeginTransaction();
            Estado estado = new Estado() { Codigo = 35, Sigla = "SP", Nome = "Sao Paulo" };
            session.SaveOrUpdate(estado);
            Municipio municipio = new Municipio() { Codigo = 9312, Estado = estado, Nome = "Sao Paulo" };
            session.SaveOrUpdate(municipio);
            TipoAparelhoMovel tipoAparelhoMovel = new TipoAparelhoMovel() { Descricao = "Tablet" };
            session.SaveOrUpdate(tipoAparelhoMovel);
            tipoAparelhoMovel = new TipoAparelhoMovel() { Descricao = "Celular" };
            session.SaveOrUpdate(tipoAparelhoMovel);
            PermissaoAcesso permissao = new PermissaoAcesso() { Nome = "Permissao de Teste" };
            session.SaveOrUpdate(permissao);
            tx.Commit();
            NHibernateHelper.CloseSession(session);
        }

        #region Atendente
        [TestMethod]
        public void Deve_incluir_um_novo_atendente_no_banco_de_dados_e_validar_se_todos_os_campos_foram_incluidos()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Atendente atendente = new Atendente();
            AtendenteDAO dao = new AtendenteDAO(session);
            atendente.Nome = "Rafael";
            atendente.Sobrenome = "Silva";
            atendente.Cpf = "41645654856";
            atendente.Rg = "387875432";
            atendente.Cnh = "192094318";
            atendente.Foto = "/fotos/atendente/21.jpg";
            atendente.TelefoneFixo = "1126710452";
            atendente.TelefoneCelular = "11994567842";
            atendente.Email = "rafael.silva@emailquente.com.br";
            atendente.StatusExclusao = 0;
            atendente.Login.Permissoes.Add(session.Get<PermissaoAcesso>(Convert.ToUInt32(1)));
            atendente.Login.Usuario = "abc";
            atendente.Login.Senha = "123";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(atendente);
            tx.Commit();
            Atendente atendente2 = null;
            tx = session.BeginTransaction();
            atendente2 = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsTrue(atendente.Nome.Equals(atendente2.Nome) && atendente.Sobrenome.Equals(atendente2.Sobrenome) &&
                atendente.Cpf.Equals(atendente2.Cpf) && atendente.Rg.Equals(atendente2.Rg) &&
                atendente.Cnh.Equals(atendente2.Cnh) && atendente.Foto.Equals(atendente2.Foto) &&
                atendente.TelefoneFixo.Equals(atendente2.TelefoneFixo) && atendente.TelefoneCelular.Equals(atendente2.TelefoneCelular) &&
                atendente.Email.Equals(atendente2.Email) && atendente.StatusExclusao == atendente2.StatusExclusao);
        }

        [TestMethod]
        public void Deve_incluir_um_novo_atendente_no_banco_de_dados_depois_alterar_os_dados_e_verificar_se_foram_alterados()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Atendente atendente = new Atendente();
            AtendenteDAO dao = new AtendenteDAO(session);
            //inclui o atendente
            atendente.Nome = "Rafael";
            atendente.Sobrenome = "Silva";
            atendente.Cpf = "41645654856";
            atendente.Rg = "387875432";
            atendente.Cnh = "192094318";
            atendente.Foto = "/fotos/atendente/21.jpg";
            atendente.TelefoneFixo = "1126710452";
            atendente.TelefoneCelular = "11994567842";
            atendente.Email = "rafael.silva@emailquente.com.br";
            atendente.StatusExclusao = 0;
            Endereco e = new Endereco();
            e.Logradouro = "rua abc";
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            e.Numero = 13;
            e.Bairro = "asas";
            atendente.Enderecos.Add(e);
            atendente.Login.Permissoes.Add(session.Get<PermissaoAcesso>(Convert.ToUInt32(1)));
            atendente.Login.Usuario = "abc";
            atendente.Login.Senha = "123";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(atendente);
            tx.Commit();
            //altera o atendente
            atendente.Id = Convert.ToUInt32(id);
            atendente.Nome = "Alterado";
            atendente.Sobrenome = "Alterado";
            atendente.Cpf = "0000000000";
            atendente.Rg = "0000000000";
            atendente.Cnh = "00000000";
            atendente.Foto = "xxxxxxxxxxxxxx";
            atendente.TelefoneFixo = "00000000000";
            atendente.TelefoneCelular = "000000000000";
            atendente.Email = "xxxxxxxxxxxxxxxx";
            atendente.StatusExclusao = 1;
            e = new Endereco();
            e.Logradouro = "rua alterado";
            e.Numero = 412;
            e.Bairro = "aterado";
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            atendente.Enderecos.Clear();
            atendente.Enderecos.Add(e);
            atendente.Login.Usuario = "xxx";
            atendente.Login.Senha = "xxx";
            tx = session.BeginTransaction();
            dao.Alterar(atendente);
            tx.Commit();
            //consulta para ver se os dados foram alterados
            Atendente atendente2 = null;
            tx = session.BeginTransaction();
            atendente2 = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsTrue(atendente.Nome.Equals(atendente2.Nome) && atendente.Sobrenome.Equals(atendente2.Sobrenome) &&
                atendente.Cpf.Equals(atendente2.Cpf) && atendente.Rg.Equals(atendente2.Rg) &&
                atendente.Cnh.Equals(atendente2.Cnh) && atendente.Foto.Equals(atendente2.Foto) &&
                atendente.TelefoneFixo.Equals(atendente2.TelefoneFixo) && atendente.TelefoneCelular.Equals(atendente2.TelefoneCelular) &&
                atendente.Email.Equals(atendente2.Email) && atendente.StatusExclusao == atendente2.StatusExclusao &&
                atendente.Login.Equals(atendente2.Login));
        }

        [TestMethod]
        public void Deve_incluir_um_novo_atendente_no_banco_de_dados_e_excluir_o_atendente_incluido()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Atendente atendente = new Atendente();
            AtendenteDAO dao = new AtendenteDAO(session);
            atendente.Nome = "Rafael";
            atendente.Sobrenome = "Silva";
            atendente.Cpf = "41645654856";
            atendente.Rg = "387875432";
            atendente.Cnh = "192094318";
            atendente.Foto = "/fotos/atendente/21.jpg";
            atendente.TelefoneFixo = "1126710452";
            atendente.TelefoneCelular = "11994567842";
            atendente.Email = "rafael.silva@emailquente.com.br";
            atendente.StatusExclusao = 0;
            Endereco e = new Endereco();
            e.Logradouro = "rua abc";
            e.Municipio = new Municipio();
            e.Municipio.Id = 1;
            e.Numero = 13;
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            e.Bairro = "asas";
            atendente.Enderecos.Add(e);
            atendente.Login.Usuario = "xxx";
            atendente.Login.Senha = "xxx";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(atendente);
            tx.Commit();
            //para consultar o endereco
            tx = session.BeginTransaction();
            atendente = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            tx = session.BeginTransaction();
            dao.Excluir(atendente);
            tx.Commit();
            Atendente atendente2 = null;
            tx = session.BeginTransaction();
            atendente2 = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsNull(atendente2);
        }

        [TestMethod]
        public void Deve_incluir_varios_atendentes_e_listar_todos_os_atendentes()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Atendente atendente = null;
            AtendenteDAO dao = new AtendenteDAO(session);
            ITransaction tx = session.BeginTransaction();
            for (int i = 0; i < 20; i++)
            {
                atendente = new Atendente();
                atendente.Nome = "Rafael";
                atendente.Sobrenome = "Silva";
                atendente.Cpf = "41645654856";
                atendente.Rg = "387875432";
                atendente.Cnh = "192094318";
                atendente.Foto = "/fotos/atendente/21.jpg";
                atendente.TelefoneFixo = "1126710452";
                atendente.TelefoneCelular = "11994567842";
                atendente.Email = "rafael.silva@emailquente.com.br";
                atendente.StatusExclusao = 0;
                Endereco e = new Endereco();
                e.Logradouro = "rua abc";
                e.Municipio = new Municipio();
                e.Municipio.Id = 1;
                e.Numero = 13;
                e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
                e.Bairro = "asas";
                atendente.Enderecos.Add(e);
                atendente.Login.Usuario = "xxx";
                atendente.Login.Senha = "xxx";
                dao.Incluir(atendente);
            }
            tx.Commit();
            tx = session.BeginTransaction();
            IList<Atendente> atendentes = dao.Listar();
            tx.Commit();

            /*ClienteDAO cdao = new ClienteDAO(session);
            tx = session.BeginTransaction();
            Cliente c = new Cliente();
            PessoaFisica pf = new PessoaFisica();
            pf.Cpf = "415654";
            pf.Rg = "312513";
            pf.Nome = "abc123";
            pf.Sobrenome = "hjga";
            c.Pessoa = pf;
            ulong id = cdao.Incluir(c);
            c = cdao.BuscarPeloId(id);
            PessoaJuridica pj = new PessoaJuridica();
            pj.NomeFantasia = "fafafafa";
            pj.RazaoSocial = "sasasasasasa";
            pj.Cnpj = "91919191";
            pj.InscricaoEstadual = "12121212";
            c.Pessoa = pj;
            cdao.Alterar(c);
            tx.Commit();*/

            NHibernateHelper.CloseSession(session);
            Assert.IsTrue(atendentes.Count >= 20);
        }
        #endregion

        #region Cliente
        [TestMethod]
        public void Deve_incluir_um_novo_cliente_pessoa_fisica_no_banco_de_dados_e_validar_se_todos_os_campos_foram_incluidos()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Cliente cliente = new Cliente();
            PessoaFisica pf = new PessoaFisica();
            ClienteDAO dao = new ClienteDAO(session);
            pf.Nome = "Rafael";
            pf.Sobrenome = "Silva";
            pf.Cpf = "41645654856";
            pf.Rg = "387875432";
            pf.Foto = "/fotos/atendente/21.jpg";
            pf.TelefoneFixo = "1126710452";
            pf.TelefoneCelular = "11994567842";
            pf.Email = "rafael.silva@emailquente.com.br";
            cliente.Pessoa = pf;
            cliente.Pessoa.Login.Usuario = "abc";
            cliente.Pessoa.Login.Senha = "123";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(cliente);
            tx.Commit();
            Cliente cliente2 = null;
            tx = session.BeginTransaction();
            cliente2 = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            PessoaFisica pf2 = (PessoaFisica)cliente2.Pessoa;
            Assert.IsTrue(pf.Nome.Equals(pf2.Nome) && pf.Sobrenome.Equals(pf2.Sobrenome) &&
                pf.Cpf.Equals(pf2.Cpf) && pf.Rg.Equals(pf2.Rg) && pf.Foto.Equals(pf2.Foto) &&
                pf.TelefoneFixo.Equals(pf2.TelefoneFixo) && pf.TelefoneCelular.Equals(pf2.TelefoneCelular) &&
                pf.Email.Equals(pf2.Email));
        }
        #endregion
    }
}
