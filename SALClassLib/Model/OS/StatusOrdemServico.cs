using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.OS
{
    public class StatusOrdemServico
    {
        private uint id;

        public uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private String descricao;

        public String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        
        
    }
}
