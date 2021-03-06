﻿using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.DAO;
using MySql.Data.MySqlClient;

namespace SALClassLib.Masterdata.Model.DAO
{
    public class EnderecoDAO : DAO<Endereco>
    {
        public EnderecoDAO(ISession sessao)
            : base(sessao)
        {

        }

        //@TODO
        public IList<Endereco> ListarEnderecosDoCliente(Cliente cliente)
        {
            IList<Endereco> enderecos = new List<Endereco>();
            MySqlConnection conn = new MySqlConnection("Database=SAL;Server=localhost;Uid=root;Pwd=123");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT ENDE.*, MUN.NOME AS NOMEMUNICIPIO, MUN.ID_ESTADO," +
                "EST.SIGLA AS SIGLAESTADO FROM TB_ENDERECO ENDE " +
                "INNER JOIN TB_MUNICIPIO MUN ON MUN.ID_MUNICIPIO = ENDE.ID_MUNICIPIO " +
                "INNER JOIN TB_ESTADO EST ON EST.ID_ESTADO = MUN.ID_ESTADO " +
                "WHERE ENDE.ID_PESSOA = @PESSOA", conn);
            cmd.Parameters.AddWithValue("PESSOA", cliente.Pessoa.Id);
            MySqlDataReader dr = cmd.ExecuteReader();
            Endereco end;
            while (dr.Read())
            {
                end = new Endereco();
                end.Id = Convert.ToUInt32(dr["ID_ENDERECO"]);
                end.Logradouro = dr["LOGRADOURO"].ToString();
                end.Bairro = dr["BAIRRO"].ToString();
                end.Numero = dr["NUMERO"].ToString();
                end.Observacao = dr["OBSERVACAO"].ToString();
                end.Municipio.Id = Convert.ToUInt32(dr["ID_MUNICIPIO"]);
                end.Municipio.Nome = dr["NOMEMUNICIPIO"].ToString();
                end.Municipio.Estado.Id = Convert.ToUInt16(dr["ID_ESTADO"]);
                end.Municipio.Estado.Sigla = dr["SIGLAESTADO"].ToString();
                enderecos.Add(end);
            }
            dr.Close();
            conn.Close();
            return enderecos;
        }
    }
}
