using SALClassLib.Masterdata.Model;
using SALClassLib.OS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALMvcMobile.Models
{
    public class EnderecoMatrizDistancia : Endereco
    {
        public EnderecoMatrizDistancia()
        {

        }

        public EnderecoMatrizDistancia(Endereco e)
        {
            this.Bairro = e.Bairro;
            this.Cep = e.Cep;
            this.Complemento = e.Complemento;
            this.Id = e.Id;
            this.Logradouro = e.Logradouro;
            this.Municipio = e.Municipio;
            this.Numero = e.Numero;
            this.Observacao = e.Observacao;
            this.Pessoa = e.Pessoa;
        }

        public String DistanciaTexto { get; set; }
        public int DistanciaValor { get; set; }

        public String DuracaoTexto { get; set; }
        public int DuracaoValor { get; set; }

        public OrdemServico OrdemServico { get; set; }
    }
}