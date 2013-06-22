using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class PessoaFisica : Pessoa
    {
        private String nome;

        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private String sobrenome;

        public virtual String Sobrenome
        {
            get { return sobrenome; }
            set { sobrenome = value; }
        }

        private String cpf;

        public virtual String Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        private String rg;

        public virtual String Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        private String cnh;

        public virtual String Cnh
        {
            get { return cnh; }
            set { cnh = value; }
        }

        private String foto;

        public virtual String Foto
        {
            get { return foto; }
            set { foto = value; }
        }

        private String telefoneFixo;

        public virtual String TelefoneFixo
        {
            get { return telefoneFixo; }
            set { telefoneFixo = value; }
        }

        private String telefoneCelular;

        public virtual String TelefoneCelular
        {
            get { return telefoneCelular; }
            set { telefoneCelular = value; }
        }

        private String email;

        public virtual String Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}
