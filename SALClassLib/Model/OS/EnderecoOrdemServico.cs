using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.Model.Masterdata;

namespace SALClassLib.Model.OS
{
    public class EnderecoOrdemServico : Endereco
    {
        private String nomeContato;

        public String NomeContato
        {
            get { return nomeContato; }
            set { nomeContato = value; }
        }

        private String telefoneContato;

        public String TelefoneContato
        {
            get { return telefoneContato; }
            set { telefoneContato = value; }
        }
        
        
    }
}
