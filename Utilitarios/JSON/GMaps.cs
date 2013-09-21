using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Utilitarios.JSON
{
    public class GMaps
    {
        /// <summary>
        /// Envia a chamada para a API buscando o geocode
        /// </summary>
        /// <param name="address">Endereço de onde se quer retornar os dados</param>
        /// <returns></returns>
        public static JToken getGeocode(String address)
        {
            String url = "http://maps.googleapis.com/maps/api/geocode/json?sensor=false&address=" + address;

            return JSON.Fetch(url);
        }

        /// <summary>
        /// Envia a chamada para a API buscando o geocode
        /// </summary>
        /// <param name="address">Endereço de onde se quer retornar os dados</param>
        /// <returns></returns>
        public static JToken getDistanceMatrix(IList<String> origens, IList<String> destinos)
        {
            String url = "http://maps.googleapis.com/maps/api/distancematrix/json?mode=driving&language=pt-BR&sensor=false&origins=";

            for (var index = 0; index < origens.Count; index++)
            {
                url += origens[index];
                if (index + 1 <= origens.Count)
                    url += "|";
            }

            url += "&destinations=";

            for (var index = 0; index < destinos.Count; index++)
            {
                url += destinos[index];
                if (index + 1 <= destinos.Count)
                    url += "|";
            }

            return JSON.Fetch(url);
        }
    }
}
