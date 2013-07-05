using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SALClassLib.Masterdata.Model
{
    public partial class AparelhoMovel
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }
        

        private TipoAparelhoMovel tipo;

        public virtual TipoAparelhoMovel Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private String modelo;

        [DisplayName("Modelo")]
        [MaxLength(25)]
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

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            AparelhoMovel a = (AparelhoMovel)obj;

            return a.Id == this.Id && a.Tipo.Equals(this.Tipo);
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
