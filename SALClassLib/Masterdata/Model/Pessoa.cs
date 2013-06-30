using Iesi.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Pessoa
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private ISet<Endereco> enderecos;

        public virtual ISet<Endereco> Enderecos
        {
            get { return enderecos; }
            set { enderecos = value; }
        }

        private Login login;

        public virtual Login Login
        {
            get { return login; }
            set { login = value; }
        }

        private short statusExclusao;

        public short StatusExclusao
        {
            get { return statusExclusao; }
            set { statusExclusao = value; }
        }
        

        public Pessoa()
        {
            enderecos = new HashedSet<Endereco>();
            Login = new Login();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Pessoa p = (Pessoa)obj;
            return p.Id == this.Id && p.Login.Equals(this.Login);
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
