using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Endereco
    {
        private uint idEndereco;

        public virtual uint IdEndereco
        {
            get { return idEndereco; }
            set { idEndereco = value; }
        }

        private String logradouro;

        public virtual String Logradouro
        {
            get { return logradouro; }
            set { logradouro = value; }
        }

        private String bairro;

        public virtual String Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        private uint numero;

        public virtual uint Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private String observacao;

        public virtual String Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        private Municipio municipio;

        public virtual Municipio Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }
        
    }
}