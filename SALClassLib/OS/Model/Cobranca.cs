using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.OS.Model
{
    public class Cobranca
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private Decimal valor;

        public virtual Decimal Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        
    }
}
