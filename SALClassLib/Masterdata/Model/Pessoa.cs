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


        public Pessoa()
        {
            enderecos = new HashedSet<Endereco>();
            Login = new Login();
        }
    }
}
