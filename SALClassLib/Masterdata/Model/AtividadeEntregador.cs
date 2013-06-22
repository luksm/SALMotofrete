using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class AtividadeEntregador
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private short statusAtividade;

        public virtual short StatusAtividade
        {
            get { return statusAtividade; }
            set { statusAtividade = value; }
        }

        private DateTime dataHora;

        public virtual DateTime DataHora
        {
            get { return dataHora; }
            set { dataHora = value; }
        }
    }
}