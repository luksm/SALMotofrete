using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Municipio
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private uint codigo;

        public virtual uint Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        

        private String nome;

        public virtual String Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private Estado estado;

        public virtual Estado Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        
    }
}