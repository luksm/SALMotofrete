using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.Masterdata
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

        public ISet<Endereco> Enderecos
        {
            get { return enderecos; }
            set { enderecos = value; }
        }
        
    }
}
