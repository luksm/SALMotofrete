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
        public String DistanciaTexto { get; set; }
        public int DistanciaValor { get; set; }

        public String DuracaoTexto { get; set; }
        public int DuracaoValor { get; set; }

        public OrdemServico OrdemServico { get; set; }
    }
}