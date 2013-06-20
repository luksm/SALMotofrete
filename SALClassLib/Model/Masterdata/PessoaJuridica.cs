using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.Masterdata
{
    public abstract class PessoaJuridica : Pessoa
    {
        private String nomeFantasia;

        public String NomeFantasia
        {
            get { return nomeFantasia; }
            set { nomeFantasia = value; }
        }

        private String razaoSocial;

        public String RazaoSocial
        {
            get { return razaoSocial; }
            set { razaoSocial = value; }
        }

        private String cnpj;

        public String Cnpj
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

        private String inscricaoEstadual;

        public String ÌnscricaoEstadual
        {
            get { return inscricaoEstadual; }
            set { inscricaoEstadual = value; }
        }

        private String website;

        public String Website
        {
            get { return website; }
            set { website = value; }
        }
        
        
        
    }
}
