using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.Masterdata.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace SALClassLib.OS.Model
{
    public class EnderecoRetirada : Endereco
    {
        public EnderecoRetirada()
        {
        }

        public EnderecoRetirada(JToken token)
            : base(token)
        {

        }

        private String nomeContato;

        [DisplayName("Nome do Contato")]
        public virtual String NomeContato
        {
            get { return nomeContato; }
            set { nomeContato = value; }
        }

        private String telefoneContato;

        [DisplayName("Telefone do Contato")]
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

            EnderecoRetirada end = (EnderecoRetirada)obj;

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
