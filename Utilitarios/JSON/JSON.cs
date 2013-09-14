using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;namespace Utilitarios.JSON
{
    public sealed class JSON
    {
        public static String Fetch(String url)
        {
            String json;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            //httpWebRequest.Accept = "application/json";
            //httpWebRequest.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }

            return json;
        }
    }
}
