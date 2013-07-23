using Iesi.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Pessoa
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private ISet<Endereco> enderecos;

        public virtual ISet<Endereco> Enderecos
        {
            get { return enderecos; }
            set { enderecos = value; }
        }

        private String usuario;

        public virtual String Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private String senha;

        public virtual String Senha
        {
            get { return senha; }
            set { senha = value; }
        }
        

        private short statusExclusao;

        public virtual short StatusExclusao
        {
            get { return statusExclusao; }
            set { statusExclusao = value; }
        }
        

        public Pessoa()
        {
            enderecos = new HashedSet<Endereco>();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Pessoa p = (Pessoa)obj;
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
