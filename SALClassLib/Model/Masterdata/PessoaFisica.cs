using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.Masterdata
{
    public abstract class PessoaFisica : Pessoa
    {
        private String nome;

        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private String sobrenome;

        public String Sobrenome
        {
            get { return sobrenome; }
            set { sobrenome = value; }
        }

        private String cpf;

        public String Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        private String rg;

        public String Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        private String cnh;

        public String Cnh
        {
            get { return cnh; }
            set { cnh = value; }
        }

        private String foto;

        public String Foto
        {
            get { return foto; }
            set { foto = value; }
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
