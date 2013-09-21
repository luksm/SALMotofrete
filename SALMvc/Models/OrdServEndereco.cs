using SALClassLib.OS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SALMvc.Models
{
    public class OrdServEndereco
    {

        private EnderecoRetirada enderecoRetirada;

        public virtual EnderecoRetirada EnderecoRetirada
        {
            get { return enderecoRetirada; }
            set { enderecoRetirada = value; }
        }

        private EnderecoEntrega enderecoEntrega;

        public virtual EnderecoEntrega EnderecoEntrega
        {
            get { return enderecoEntrega; }
            set { enderecoEntrega = value; }
        }

    }
}