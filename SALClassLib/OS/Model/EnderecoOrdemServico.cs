using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.Masterdata.Model;

namespace SALClassLib.OS.Model
{
    public class EnderecoOrdemServico : Endereco
    {

        private String nomeContato;

        public virtual String NomeContato
        {
            get { return nomeContato; }
            set { nomeContato = value; }
        }

        private String telefoneContato;

        public virtual String TelefoneContato
        {
            get { return telefoneContato; }
            set { telefoneContato = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            EnderecoOrdemServico end = (EnderecoOrdemServico)obj;

            if (end.Id == this.Id && end.nomeContato.Equals(this.nomeContato)
                && end.telefoneContato.Equals(this.telefoneContato)) return true;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = 13 * this.Id.GetHashCode();
                return result;
            }
        }
    }
}
