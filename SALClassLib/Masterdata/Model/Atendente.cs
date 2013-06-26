using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Atendente : PessoaFisica
    {        
        private short statusExclusao;

        /// <summary>
        /// 0 - Nao excluido
        /// 1 - Excluido
        /// </summary>
        public virtual short StatusExclusao
        {
            get { return statusExclusao; }
            set { statusExclusao = value; }
        }
    }
}
