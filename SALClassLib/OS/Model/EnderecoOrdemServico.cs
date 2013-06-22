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
        
        
    }
}
