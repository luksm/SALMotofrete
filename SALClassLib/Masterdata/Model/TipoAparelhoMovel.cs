using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SALClassLib.Masterdata.Model
{
    public class TipoAparelhoMovel
    {
        private ushort id;

        public virtual ushort Id
        {
            get { return id; }
            set { id = value; }
        }

        private String descricao;

        [DisplayName("Tipo do aparelho móvel")]
        public virtual String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            TipoAparelhoMovel t = (TipoAparelhoMovel)obj;

            return t.Id == this.Id;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                return base.GetHashCode() * 13 * id;
            }
        }
    }
}
