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
        [MaxLength(25, ErrorMessage="O nome deve conter no máximo 25 caracteres")]
        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private String sobrenome;

        [Required(ErrorMessage = "O sobrenome deve ser preenchido")]
        [MaxLength(80, ErrorMessage = "O sobrenome deve conter no máximo 80 caracteres")]
        public virtual String Sobrenome
        {
            get { return sobrenome; }
            set { sobrenome = value; }
        }

        private String cpf;

        [DisplayName("CPF")]
        [Required(ErrorMessage = "O CPF deve ser preenchido")]
        [MaxLength(14, ErrorMessage = "O CPF deve conter 11 dígitos")]
        [MinLength(14, ErrorMessage = "O CPF deve conter 11 dígitos")]
        public virtual String Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        private String rg;

        [DisplayName("RG")]
        [Required(ErrorMessage = "O RG deve ser preenchido")]
        [MaxLength(12, ErrorMessage = "O RG deve conter no máximo 12 caracteres")]
        public virtual String Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        private String cnh;

        [DisplayName("CNH")]
        [MaxLength(14, ErrorMessage = "A CNH deve conter no máximo 14 dígitos")]
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
        [MaxLength(15, ErrorMessage = "O telefone fixo deve conter no máximo 15 caracteres")]
        public virtual String TelefoneFixo
        {
            get { return telefoneFixo; }
            set { telefoneFixo = value; }
        }

        private String telefoneCelular;

        [DisplayName("Celular")]
        [MaxLength(16, ErrorMessage = "O telefone celular deve conter no máximo 16 caracteres")]
        public virtual String TelefoneCelular
        {
            get { return telefoneCelular; }
            set { telefoneCelular = value; }
        }

        private String email;

        [EmailAddress(ErrorMessage="Informe um endereço de email válido")]
        [MaxLength(80, ErrorMessage = "O email deve conter no máximo 80 caracteres")]
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
