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
        public Endereco Origem { get; set; }

        public IList<EnderecoMatrizDistancia> Destinos { get; set; }

        public void CalcularDistancia()
        {
            IList<String> enderecos = new List<String>();

            foreach (var item in Destinos)
            {
                enderecos.Add(item.getEndereco());
            }

            JToken token = GMaps.getDistanceMatrix(Origem.getEndereco(), enderecos);

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
            if (Destinos != null && Destinos.Count > 0)
                return Destinos.Where(d => d.DistanciaValor == Destinos.Min(de => de.DistanciaValor)).First();
            else return null;
        }
    }
}