using SALClassLib.OS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALMvc.Models
{
    public class OSEnderecoModel
    {
        public EnderecoEntrega EnderecoEntrega { get; set; }
        public EnderecoRetirada EnderecoRetirada { get; set; }
    }
}