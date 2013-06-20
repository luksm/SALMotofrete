using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.Masterdata
{
    public abstract class Atendente : PessoaFisica
    {
        private short statusExclusao;

        public short StatusExclusao
        {
            get { return statusExclusao; }
            set { statusExclusao = value; }
        }
        
    }
}
