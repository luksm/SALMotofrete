using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class AparelhoMovel
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private short tipo;

        public virtual short Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private String modelo;

        public virtual String Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }

        private String marca;

        public virtual String Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        private String numeroTelefone;

        public virtual String NumeroTelefone
        {
            get { return numeroTelefone; }
            set { numeroTelefone = value; }
        }

        private String observacao;

        public virtual String Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }
    }
}
