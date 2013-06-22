using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Estado
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private String sigla;

        public virtual String Sigla
        {
            get { return sigla; }
            set { sigla = value; }
        }

        private String nome;

        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}