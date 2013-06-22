using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Atendente : PessoaFisica
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }
        
        private short statusExclusao;

        public virtual short StatusExclusao
        {
            get { return statusExclusao; }
            set { statusExclusao = value; }
        }
    }
}
