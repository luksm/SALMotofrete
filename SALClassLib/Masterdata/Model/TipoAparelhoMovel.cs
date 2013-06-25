using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public virtual String Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

    }
}
