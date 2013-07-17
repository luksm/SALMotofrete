using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iesi.Collections.Generic;

namespace SALClassLib.Masterdata.Model
{
    public class PessoaJuridica : Pessoa
    {
        private String nomeFantasia;

        public virtual String NomeFantasia
        {
            get { return nomeFantasia; }
            set { nomeFantasia = value; }
        }

        private String razaoSocial;

        public virtual String RazaoSocial
        {
            get { return razaoSocial; }
            set { razaoSocial = value; }
        }

        private String cnpj;

        public virtual String Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

        private String inscricaoEstadual;

        public virtual String InscricaoEstadual
        {
            get { return inscricaoEstadual; }
            set { inscricaoEstadual = value; }
        }

        private String webSite;

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
