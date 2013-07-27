using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class PessoaFisica : Pessoa
    {
        private String nome;

        [Required(ErrorMessage = "O nome deve ser preenchido")]
        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private String sobrenome;

        [Required(ErrorMessage = "O sobrenome deve ser preenchido")]
        public virtual String Sobrenome
        {
            get { return sobrenome; }
            set { sobrenome = value; }
        }

        private String cpf;

        [DisplayName("CPF")]
        [Required(ErrorMessage = "O CPF deve ser preenchido")]
        public virtual String Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        private String rg;

        [DisplayName("RG")]
        [Required(ErrorMessage = "O RG deve ser preenchido")]
        public virtual String Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        private String cnh;

        [DisplayName("CNH")]
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

        [DisplayName("Telefone Fixo")]
        public virtual String TelefoneFixo
        {
            get { return telefoneFixo; }
            set { telefoneFixo = value; }
        }

        private String telefoneCelular;

        [DisplayName("Celular")]
        public virtual String TelefoneCelular
        {
            get { return telefoneCelular; }
            set { telefoneCelular = value; }
        }

        private String email;

        [EmailAddress(ErrorMessage="Informe um endereço de email válido")]
        public virtual String Email
        {
            get { return email; }
            set { email = value; }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            PessoaFisica p = (PessoaFisica)obj;
            return p.Id == this.Id;
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
