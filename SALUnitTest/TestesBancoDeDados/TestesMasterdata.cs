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
            atendente.Usuario = "abc";
            atendente.Senha = "123";
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
            e.Numero = "13";
            e.Bairro = "asas";
            atendente.Enderecos.Add(e);
            atendente.Usuario = "abc";
            atendente.Senha = "123";
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
            e.Numero = "412";
            e.Bairro = "aterado";
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            atendente.Enderecos.Clear();
            atendente.Enderecos.Add(e);
            atendente.Usuario = "xxx";
            atendente.Senha = "xxx";
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
                atendente.Usuario.Equals(atendente2.Usuario) && atendente.Senha.Equals(atendente2.Senha));
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
            e.Numero = "13";
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            e.Bairro = "asas";
            atendente.Enderecos.Add(e);
            atendente.Usuario = "xxx";
            atendente.Senha = "xxx";
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
                e.Numero = "13";
                e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
                e.Bairro = "asas";
                atendente.Enderecos.Add(e);
                atendente.Usuario = "xxx";
                atendente.Senha = "xxx";
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

        #region Gerente
        [TestMethod]
        public void Deve_incluir_um_novo_gerente_no_banco_de_dados_e_validar_se_todos_os_campos_foram_incluidos()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Gerente gerente = new Gerente();
            GerenteDAO dao = new GerenteDAO(session);
            gerente.Nome = "João";
            gerente.Sobrenome = "Pé de Feijão";
            gerente.Cpf = "42756765967";
            gerente.Rg = "498986543";
            gerente.Cnh = "192083314";
            gerente.Foto = "/fotos/gerente/21.jpg";
            gerente.TelefoneFixo = "1144715552";
            gerente.TelefoneCelular = "11993267322";
            gerente.Email = "joao.feijao@emailfrio.com.br";
            gerente.Usuario = "abcd";
            gerente.Senha = "1234";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(gerente);
            tx.Commit();
            Gerente gerente2 = null;
            tx = session.BeginTransaction();
            gerente2 = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsTrue(gerente.Nome.Equals(gerente2.Nome) && gerente.Sobrenome.Equals(gerente2.Sobrenome) &&
                gerente.Cpf.Equals(gerente2.Cpf) && gerente.Rg.Equals(gerente2.Rg) &&
                gerente.Cnh.Equals(gerente2.Cnh) && gerente.Foto.Equals(gerente2.Foto) &&
                gerente.TelefoneFixo.Equals(gerente2.TelefoneFixo) && gerente.TelefoneCelular.Equals(gerente2.TelefoneCelular) &&
                gerente.Email.Equals(gerente2.Email));
        }

        [TestMethod]
        public void Deve_incluir_um_novo_gerente_no_banco_de_dados_depois_alterar_os_dados_e_verificar_se_foram_alterados()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Gerente gerente = new Gerente();
            GerenteDAO dao = new GerenteDAO(session);

            //inclui o gerente
            gerente.Nome = "João";
            gerente.Sobrenome = "Pé de Feijão";
            gerente.Cpf = "42756765967";
            gerente.Rg = "498986543";
            gerente.Cnh = "192083314";
            gerente.Foto = "/fotos/gerente/21.jpg";
            gerente.TelefoneFixo = "1144715552";
            gerente.TelefoneCelular = "11993267322";
            gerente.Email = "joao.feijao@emailfrio.com.br";
            Endereco e = new Endereco();
            e.Logradouro = "rua abcd";
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            e.Numero = "13";
            e.Bairro = "asas";
            gerente.Enderecos.Add(e);
            gerente.Usuario = "abcd";
            gerente.Senha = "1234";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(gerente);
            tx.Commit();

            //altera o gerente
            gerente.Id = Convert.ToUInt32(id);
            gerente.Nome = "Alterado";
            gerente.Sobrenome = "Alterado";
            gerente.Cpf = "0000000000";
            gerente.Rg = "0000000000";
            gerente.Cnh = "00000000";
            gerente.Foto = "xxxxxxxxxxxxxx";
            gerente.TelefoneFixo = "00000000000";
            gerente.TelefoneCelular = "000000000000";
            gerente.Email = "xxxxxxxxxxxxxxxx";
            e = new Endereco();
            e.Logradouro = "rua alterado";
            e.Numero = "421";
            e.Bairro = "aterado";
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            gerente.Enderecos.Clear();
            gerente.Enderecos.Add(e);
            gerente.Usuario = "xxx";
            gerente.Senha = "xxx";
            tx = session.BeginTransaction();
            dao.Alterar(gerente);
            tx.Commit();

            //consulta para ver se os dados foram alterados
            Gerente gerente2 = null;
            tx = session.BeginTransaction();
            gerente2 = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsTrue(gerente.Nome.Equals(gerente2.Nome) && gerente.Sobrenome.Equals(gerente2.Sobrenome) &&
                gerente.Cpf.Equals(gerente2.Cpf) && gerente.Rg.Equals(gerente2.Rg) &&
                gerente.Cnh.Equals(gerente2.Cnh) && gerente.Foto.Equals(gerente2.Foto) &&
                gerente.TelefoneFixo.Equals(gerente2.TelefoneFixo) && gerente.TelefoneCelular.Equals(gerente2.TelefoneCelular) &&
                gerente.Email.Equals(gerente2.Email) && gerente.Usuario.Equals(gerente2.Usuario) && gerente.Senha.Equals(gerente2.Senha));
        }

        [TestMethod]
        public void Deve_incluir_um_novo_gerente_no_banco_de_dados_e_excluir_o_gerente_incluido()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Gerente gerente = new Gerente();
            GerenteDAO dao = new GerenteDAO(session);
            gerente.Nome = "João";
            gerente.Sobrenome = "Pé de Feijão";
            gerente.Cpf = "42756765967";
            gerente.Rg = "498986543";
            gerente.Cnh = "192083314";
            gerente.Foto = "/fotos/gerente/21.jpg";
            gerente.TelefoneFixo = "1144715552";
            gerente.TelefoneCelular = "11993267322";
            gerente.Email = "joao.feijao@emailfrio.com.br";
            gerente.StatusExclusao = 0;
            Endereco e = new Endereco();
            e.Logradouro = "rua abcd";
            e.Municipio = new Municipio();
            e.Municipio.Id = 1;
            e.Numero = "13";
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            e.Bairro = "asas";
            gerente.Enderecos.Add(e);
            gerente.Usuario = "xxx";
            gerente.Senha = "xxx";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(gerente);
            tx.Commit();

            //para consultar o endereco
            tx = session.BeginTransaction();
            gerente = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            tx = session.BeginTransaction();
            dao.Excluir(gerente);
            tx.Commit();
            Gerente gerente2 = null;
            tx = session.BeginTransaction();
            gerente2 = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsNull(gerente2);
        }

        [TestMethod]
        public void Deve_incluir_varios_gerentes_e_listar_todos_os_gerentes()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Gerente gerente = null;
            GerenteDAO dao = new GerenteDAO(session);
            ITransaction tx = session.BeginTransaction();
            for (int i = 0; i < 20; i++)
            {
                gerente = new Gerente();
                gerente.Nome = "João";
                gerente.Sobrenome = "Pé de Feijão";
                gerente.Cpf = "42756765967";
                gerente.Rg = "498986543";
                gerente.Cnh = "192083314";
                gerente.Foto = "/fotos/gerente/21.jpg";
                gerente.TelefoneFixo = "1144715552";
                gerente.TelefoneCelular = "11993267322";
                gerente.Email = "joao.feijao@emailfrio.com.br";
                gerente.StatusExclusao = 0;
                Endereco e = new Endereco();
                e.Logradouro = "rua abcd";
                e.Municipio = new Municipio();
                e.Municipio.Id = 1;
                e.Numero = "13";
                e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
                e.Bairro = "asas";
                gerente.Enderecos.Add(e);
                gerente.Usuario = "xxx";
                gerente.Senha = "xxx";
                dao.Incluir(gerente);
            }
            tx.Commit();
            tx = session.BeginTransaction();
            IList<Gerente> gerentes = dao.Listar();
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsTrue(gerentes.Count >= 20);
        }
        #endregion

        #region Entregador
        [TestMethod]
        public void Deve_incluir_um_novo_entregador_no_banco_de_dados_e_validar_se_todos_os_campos_foram_incluidos()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Entregador entregador = new Entregador();
            EntregadorDAO dao = new EntregadorDAO(session);
            entregador.Nome = "Pedro";
            entregador.Sobrenome = "Pedroca";
            entregador.Cpf = "52756755922";
            entregador.Rg = "398386321";
            entregador.Cnh = "122481319";
            entregador.Foto = "/fotos/entregador/23.jpg";
            entregador.TelefoneFixo = "1143916739";
            entregador.TelefoneCelular = "11987614572";
            entregador.Email = "pedro.pedroca@emailmorno.com.br";
            entregador.PlacaMoto = "ABC1234";
            entregador.ModeloMoto = "CB-300";
            entregador.StatusExclusao = 0;
            entregador.StatusAtividade = 1;
            entregador.Usuario = "entreg";
            entregador.Senha = "1234";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(entregador);
            tx.Commit();
            Entregador entregador2 = null;
            tx = session.BeginTransaction();
            entregador2 = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsTrue(entregador.Nome.Equals(entregador2.Nome) && entregador.Sobrenome.Equals(entregador2.Sobrenome) &&
                entregador.Cpf.Equals(entregador2.Cpf) && entregador.Rg.Equals(entregador2.Rg) &&
                entregador.Cnh.Equals(entregador2.Cnh) && entregador.Foto.Equals(entregador2.Foto) &&
                entregador.TelefoneFixo.Equals(entregador2.TelefoneFixo) && entregador.TelefoneCelular.Equals(entregador2.TelefoneCelular) &&
                entregador.Email.Equals(entregador2.Email) && entregador.StatusExclusao == entregador2.StatusExclusao &&
                entregador.StatusAtividade == entregador2.StatusAtividade && entregador.PlacaMoto == entregador2.PlacaMoto &&
                entregador.ModeloMoto == entregador2.ModeloMoto);
        }

        [TestMethod]
        public void Deve_incluir_um_novo_entregador_no_banco_de_dados_depois_alterar_os_dados_e_verificar_se_foram_alterados()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Entregador entregador = new Entregador();
            EntregadorDAO dao = new EntregadorDAO(session);

            //inclui o entregador
            entregador.Nome = "Pedro";
            entregador.Sobrenome = "Pedroca";
            entregador.Cpf = "52756755922";
            entregador.Rg = "398386321";
            entregador.Cnh = "122481319";
            entregador.Foto = "/fotos/entregador/23.jpg";
            entregador.TelefoneFixo = "1143916739";
            entregador.TelefoneCelular = "11987614572";
            entregador.Email = "pedro.pedroca@emailmorno.com.br";
            entregador.PlacaMoto = "ABC1234";
            entregador.ModeloMoto = "CB-300";
            entregador.StatusExclusao = 0;
            entregador.StatusAtividade = 1;
            Endereco e = new Endereco();
            e.Logradouro = "rua abcd";
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            e.Numero = "13";
            e.Bairro = "asas";
            entregador.Enderecos.Add(e);
            entregador.Usuario = "entreg";
            entregador.Senha = "1234";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(entregador);
            tx.Commit();

            //altera o entregador
            entregador.Id = Convert.ToUInt32(id);
            entregador.Nome = "yyyyyyyy";
            entregador.Sobrenome = "yyyyyyyyyyyyyyyyyyy";
            entregador.Cpf = "000000000";
            entregador.Rg = "1111111111";
            entregador.Cnh = "22222222222";
            entregador.Foto = "/fotos/entregador/00.jpg";
            entregador.TelefoneFixo = "4444444444";
            entregador.TelefoneCelular = "5555555555";
            entregador.Email = "teste@teste.com.br";
            entregador.PlacaMoto = "AAA0000";
            entregador.ModeloMoto = "CB-400";
            entregador.StatusExclusao = 1;
            entregador.StatusAtividade = 2;
            e = new Endereco();
            e.Logradouro = "rua alterado";
            e.Numero = "412";
            e.Bairro = "aterado";
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            entregador.Enderecos.Clear();
            entregador.Enderecos.Add(e);
            entregador.Usuario = "xxx";
            entregador.Senha = "xxx";
            tx = session.BeginTransaction();
            dao.Alterar(entregador);
            tx.Commit();

            //consulta para ver se os dados foram alterados
            Entregador entregador2 = null;
            tx = session.BeginTransaction();
            entregador2 = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsTrue(entregador.Nome.Equals(entregador2.Nome) && entregador.Sobrenome.Equals(entregador2.Sobrenome) &&
                entregador.Cpf.Equals(entregador2.Cpf) && entregador.Rg.Equals(entregador2.Rg) &&
                entregador.Cnh.Equals(entregador2.Cnh) && entregador.Foto.Equals(entregador2.Foto) &&
                entregador.TelefoneFixo.Equals(entregador2.TelefoneFixo) && entregador.TelefoneCelular.Equals(entregador2.TelefoneCelular) &&
                entregador.Email.Equals(entregador2.Email) && entregador.StatusExclusao == entregador2.StatusExclusao &&
                entregador.StatusAtividade == entregador2.StatusAtividade && entregador.PlacaMoto == entregador2.PlacaMoto &&
                entregador.ModeloMoto == entregador2.ModeloMoto && entregador.Usuario.Equals(entregador2.Usuario) && entregador.Senha.Equals(entregador2.Senha));
        }

        [TestMethod]
        public void Deve_incluir_um_novo_entregador_no_banco_de_dados_e_excluir_o_entregador_incluido()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Entregador entregador = new Entregador();
            EntregadorDAO dao = new EntregadorDAO(session);
            entregador.Nome = "Pedro";
            entregador.Sobrenome = "Pedroca";
            entregador.Cpf = "52756755922";
            entregador.Rg = "398386321";
            entregador.Cnh = "122481319";
            entregador.Foto = "/fotos/entregador/23.jpg";
            entregador.TelefoneFixo = "1143916739";
            entregador.TelefoneCelular = "11987614572";
            entregador.Email = "pedro.pedroca@emailmorno.com.br";
            entregador.PlacaMoto = "ABC1234";
            entregador.ModeloMoto = "CB-300";
            entregador.StatusExclusao = 0;
            entregador.StatusAtividade = 1;
            Endereco e = new Endereco();
            e.Logradouro = "rua abcd";
            e.Municipio = new Municipio();
            e.Municipio.Id = 1;
            e.Numero = "13";
            e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
            e.Bairro = "asas";
            entregador.Enderecos.Add(e);
            entregador.Usuario = "xxx";
            entregador.Senha = "xxx";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(entregador);
            tx.Commit();

            //para consultar o endereco
            tx = session.BeginTransaction();
            entregador = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            tx = session.BeginTransaction();
            dao.Excluir(entregador);
            tx.Commit();
            Entregador entregador2 = null;
            tx = session.BeginTransaction();
            entregador2 = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsNull(entregador2);
        }

        [TestMethod]
        public void Deve_incluir_varios_entregadors_e_listar_todos_os_entregadors()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Entregador entregador = null;
            EntregadorDAO dao = new EntregadorDAO(session);
            ITransaction tx = session.BeginTransaction();
            for (int i = 0; i < 20; i++)
            {
                entregador = new Entregador();
                entregador.Nome = "Pedro";
                entregador.Sobrenome = "Pedroca";
                entregador.Cpf = "52756755922";
                entregador.Rg = "398386321";
                entregador.Cnh = "122481319";
                entregador.Foto = "/fotos/entregador/23.jpg";
                entregador.TelefoneFixo = "1143916739";
                entregador.TelefoneCelular = "11987614572";
                entregador.Email = "pedro.pedroca@emailmorno.com.br";
                entregador.PlacaMoto = "ABC1234";
                entregador.ModeloMoto = "CB-300";
                entregador.StatusExclusao = 0;
                entregador.StatusAtividade = 1;
                Endereco e = new Endereco();
                e.Logradouro = "rua abcd";
                e.Municipio = new Municipio();
                e.Municipio.Id = 1;
                e.Numero = "13";
                e.Municipio = session.Get<Municipio>(Convert.ToUInt32(1));
                e.Bairro = "asas";
                entregador.Enderecos.Add(e);
                entregador.Usuario = "xxx";
                entregador.Senha = "xxx";
                dao.Incluir(entregador);
            }
            tx.Commit();
            tx = session.BeginTransaction();
            IList<Entregador> entregadors = dao.Listar();
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            Assert.IsTrue(entregadors.Count >= 20);
        }
        #endregion

        #region Cliente
        [TestMethod]
        public void Deve_incluir_um_novo_cliente_pessoa_fisica_no_banco_de_dados_e_validar_se_todos_os_campos_foram_incluidos()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Cliente cliente = new Cliente();
            ClienteDAO dao = new ClienteDAO(session);
            PessoaFisica pf = new PessoaFisica();
            pf.Nome = "Rafael";
            pf.Sobrenome = "Silva";
            pf.Cpf = "41645654856";
            pf.Rg = "387875432";
            pf.Foto = "/fotos/atendente/21.jpg";
            pf.TelefoneFixo = "1126710452";
            pf.TelefoneCelular = "11994567842";
            pf.Email = "rafael.silva@emailquente.com.br";
            cliente.Pessoa = pf;
            cliente.Pessoa.Usuario = "abc";
            cliente.Pessoa.Senha = "123";
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
