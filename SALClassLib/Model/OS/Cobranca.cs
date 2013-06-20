using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.OS
{
    public class Cobranca
    {
        private uint id;

        public uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private Decimal valor;

        public Decimal Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        
    }
}
