using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.Masterdata
{
    public class PermissaoAcesso
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
        
    }
}
