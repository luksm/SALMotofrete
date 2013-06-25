using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using SALClassLib.Masterdata.Model.DAO;
using SALClassLib.Masterdata.Model;

namespace SALUnitTest
{
    [TestClass]
    public class TestesMasterdata
    {
        [TestMethod]
        public void Deve_inserir_um_novo_aparelho_movel_no_banco_de_dados_e_consultar_para_ver_se_foi_inserido()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            AparelhoMovelDAO dao = new AparelhoMovelDAO(session);
            AparelhoMovel am = new AparelhoMovel();
            am.Tipo = null;
            am.NumeroTelefone = "991460739";
            am.Marca = "teste";
            ITransaction tx = session.BeginTransaction();
            ulong id = dao.Incluir(am);
            tx.Commit();
            tx = session.BeginTransaction();
            am = dao.BuscarPeloId(Convert.ToUInt32(id));
            tx.Commit();
            NHibernateHelper.CloseSession(session);
            NHibernateHelper.CloseSessionFactory();
            Assert.IsNotNull(am);
        }
    }
}
