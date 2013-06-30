using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Contato
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }
        
        private String nome;

        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private String telefoneFixo;

        public virtual String TelefoneFixo
        {
            get { return telefoneFixo; }
            set { telefoneFixo = value; }
        }

        private String telefoneCelular;

        public virtual String TelefoneCelular
        {
            get { return telefoneCelular; }
            set { telefoneCelular = value; }
        }

        private String email;

        public virtual String Email
        {
            get { return email; }
            set { email = value; }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Contato c = (Contato)obj;

            return c.Id == this.Id && c.Nome.Equals(this.Nome);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                return base.GetHashCode() * 13;
            }
        }
        
    }
}
