using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Endereco
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
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

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Endereco e = (Endereco)obj;
            return e.Id == this.Id;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                return base.GetHashCode();
            }
        }
    }
}