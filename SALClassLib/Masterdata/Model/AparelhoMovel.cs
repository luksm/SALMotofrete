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

        [DisplayName("ID")]
        [DisplayFormat(DataFormatString="{0:0000}")]
        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }
        
        private TipoAparelhoMovel tipo;

        [Required(ErrorMessage="Informe o tipo do aparelho móvel")]
        public virtual TipoAparelhoMovel Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private String modelo;

        [MaxLength(25, ErrorMessage="O modelo deve ter no máximo 25 caracteres")]
        [Required(ErrorMessage="O modelo deve ser preenchido")]
        public virtual String Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }

        private String marca;

        [MaxLength(25, ErrorMessage = "A marca deve ter no máximo 25 caracteres")]
        [Required(ErrorMessage = "A marca deve ser preenchida")]
        public virtual String Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        private String numeroTelefone;

        [MaxLength(30, ErrorMessage = "O telefone deve ter no máximo 30 caracteres")]
        [DisplayName("Número de telefone")]
        public virtual String NumeroTelefone
        {
            get { return numeroTelefone; }
            set { numeroTelefone = value; }
        }

        private String observacao;

        [MaxLength(2000, ErrorMessage = "A observação deve ter no máximo 2000 caracteres")]
        [DisplayName("Observação")]
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

            return a.Id == this.Id;
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
