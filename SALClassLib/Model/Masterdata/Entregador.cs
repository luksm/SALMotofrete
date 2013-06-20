using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.Masterdata
{
    public abstract class Entregador : PessoaFisica 
    {
        private String placaMoto;

        public String PlacaMoto
        {
            get { return placaMoto; }
            set { placaMoto = value; }
        }

        private String modeloMoto;

        public String ModeloMoto
        {
            get { return modeloMoto; }
            set { modeloMoto = value; }
        }

        private short statusExclusao;

        public short StatusExclusao
        {
            get { return statusExclusao; }
            set { statusExclusao = value; }
        }

        private short statusAtividade;

        public short StatusAtividade
        {
            get { return statusAtividade; }
            set { statusAtividade = value; }
        }
        
        
        
    }
}
