using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Municipio
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private uint codigo;

        public virtual uint Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        

        private String nome;

        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private Estado estado;

        public virtual Estado Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public Municipio()
        {
            estado = new Estado();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Municipio m = (Municipio)obj;
            return m.Id == this.Id && m.Nome.Equals(this.Nome) && m.Codigo == this.Codigo && m.Estado.Equals(this.Estado);
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