using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Estado
    {
        private ushort id;

        public virtual ushort Id
        {
            get { return id; }
            set { id = value; }
        }

        private ushort codigo;

        public virtual ushort Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        

        private String sigla;

        public virtual String Sigla
        {
            get { return sigla; }
            set { sigla = value; }
        }

        private String nome;

        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Estado e = (Estado)obj;
            return e.Id == this.Id && e.Codigo == this.Codigo && e.Nome.Equals(this.Nome) && e.Sigla.Equals(this.Sigla);
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