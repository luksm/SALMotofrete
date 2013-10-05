using Newtonsoft.Json.Linq;
using SALClassLib.Masterdata.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilitarios.JSON;

namespace SALMvcMobile.Models
{
    public class MatrizDistancia
    {
        public String Origem { get; set; }

        public IList<EnderecoMatrizDistancia> Destinos { get; set; }

        private void CalcularDistancia()
        {
            IList<String> enderecos = new List<String>();

            foreach (var item in Destinos)
            {
                enderecos.Add(item.getEndereco());
            }

            JToken token = GMaps.getDistanceMatrix(Origem, enderecos);

            JToken elements = token.SelectToken("rows").First().SelectToken("elements");

            for (int i = 0; i < elements.Count(); i++)
            {
                Destinos[i].DistanciaTexto = elements[i].SelectToken("distance").SelectToken("text").ToString();
                Destinos[i].DistanciaValor = int.Parse(elements[i].SelectToken("distance").SelectToken("value").ToString());
                Destinos[i].DuracaoTexto = elements[i].SelectToken("duration").SelectToken("text").ToString();
                Destinos[i].DuracaoValor = int.Parse(elements[i].SelectToken("duration").SelectToken("value").ToString());
            }
        }

        public EnderecoMatrizDistancia GetDestinoMaisProximo()
        {
            var ordensPrioritarias = Destinos.Where(e => e.OrdemServico.Data.AddMinutes(15) <= DateTime.Now);
            
            if (ordensPrioritarias.Count() > 0)
            {
                return ordensPrioritarias.Where(e => e.OrdemServico.Data == Destinos.Max(d => d.OrdemServico.Data)).First();
            }

            CalcularDistancia();

            if (Destinos != null && Destinos.Count > 0)
                return Destinos.Where(d => d.DistanciaValor == Destinos.Min(de => de.DistanciaValor)).First();
            else return null;
        }
    }
}