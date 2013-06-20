using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.Masterdata
{
    public class Login
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
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

        private ISet<PermissaoAcesso> permissoes;

        public ISet<PermissaoAcesso> Permissoes
        {
            get { return permissoes; }
            set { permissoes = value; }
        }
        
    }
}
