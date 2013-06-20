using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Model.Masterdata
{
    public class AparelhoMovel
    {
        private uint id;

        public uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private short tipo;

        public short Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private String modelo;

        public String Model
        {
            get { return modelo; }
            set { modelo = value; }
        }

        private String marca;

        public String Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        private String numeroTelefone;

        public String NumeroTelefone
        {
            get { return numeroTelefone; }
            set { numeroTelefone = value; }
        }

        private String observacao;

        public String Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }
        
        
        
    }
}
