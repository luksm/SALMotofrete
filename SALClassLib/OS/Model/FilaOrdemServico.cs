using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.Masterdata.Model;

namespace SALClassLib.OS.Model
{
    public class FilaOrdemServico
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private ushort posicao;

        public virtual ushort Posicao
        {
            get { return posicao; }
            set { posicao = value; }
        }

        private OrdemServico ordemServico;

        public virtual OrdemServico OrdemServico
        {
            get { return ordemServico; }
            set { ordemServico = value; }
        }
        
    }
}
