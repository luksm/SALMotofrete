using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Pessoa
    {
        private uint idPessoa;

        public virtual uint IdPessoa
        {
            get { return idPessoa; }
            set { idPessoa = value; }
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
        
    }
}
