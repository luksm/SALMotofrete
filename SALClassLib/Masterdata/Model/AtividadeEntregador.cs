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

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            AtividadeEntregador a = (AtividadeEntregador)obj;

            return a.Id == this.Id;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                return base.GetHashCode() * 13;
            }
        }
    }
}