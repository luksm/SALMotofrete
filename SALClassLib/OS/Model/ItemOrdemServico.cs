using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.OS.Model
{
    public class ItemOrdemServico
    {
        private String descricao;

        public virtual String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        private Decimal peso;

        public virtual Decimal Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        private Decimal volume;

        public virtual Decimal Volume
        {
            get { return volume; }
            set { volume = value; }
        }
        
    }
}
