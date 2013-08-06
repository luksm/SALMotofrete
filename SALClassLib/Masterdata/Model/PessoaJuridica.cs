using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iesi.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SALClassLib.Masterdata.Model
{
    public class PessoaJuridica : Pessoa
    {
        private String nomeFantasia;

        [DisplayName("Nome Fantasia")]
        [Required(ErrorMessage="O nome fantasia deve ser preenchido")]
        [MaxLength(40, ErrorMessage = "O nome fantasia deve conter no máximo 40 caracteres")]
        public virtual String NomeFantasia
        {
            get { return nomeFantasia; }
            set { nomeFantasia = value; }
        }

        private String razaoSocial;

        [DisplayName("Razão Social")]
        [Required(ErrorMessage = "A razão social deve ser preenchido")]
        [MaxLength(100, ErrorMessage = "A razão social deve conter no máximo 100 caracteres")]
        public virtual String RazaoSocial
        {
            get { return razaoSocial; }
            set { razaoSocial = value; }
        }

        private String cnpj;

        [DisplayName("CNPJ")]
        [Required(ErrorMessage = "O CNPJ deve ser preenchido")]
        [MaxLength(20, ErrorMessage = "O CNPJ deve conter no máximo 20 caracteres")]
        public virtual String Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

        private String inscricaoEstadual;

        [DisplayName("Inscrição Estadual")]
        [MaxLength(20, ErrorMessage = "A inscrição estadual deve conter no máximo 20 caracteres")]
        public virtual String InscricaoEstadual
        {
            get { return inscricaoEstadual; }
            set { inscricaoEstadual = value; }
        }

        private String webSite;

        [DisplayName("Web Site")]
        [MaxLength(40, ErrorMessage = "O web site deve conter no máximo 40 caracteres")]
        public virtual String WebSite
        {
            get { return webSite; }
            set { webSite = value; }
        }

        private ISet<Contato> contatos;

        public virtual ISet<Contato> Contatos
        {
            get { return contatos; }
            set { contatos = value; }
        }

        public PessoaJuridica()
        {
            contatos = new HashedSet<Contato>();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            PessoaJuridica p = (PessoaJuridica)obj;
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
