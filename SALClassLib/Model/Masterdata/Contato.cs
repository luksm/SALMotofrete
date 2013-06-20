using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.Masterdata
{
    public class Contato
    {
        private String nome;

        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private String telefoneFixo;

        public String TelefoneFixo
        {
            get { return telefoneFixo; }
            set { telefoneFixo = value; }
        }

        private String telefoneCelular;

        public String TelefoneCelular
        {
            get { return telefoneCelular; }
            set { telefoneCelular = value; }
        }

        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        
    }
}
