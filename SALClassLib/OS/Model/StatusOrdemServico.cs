using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.OS.Model
{
    public class StatusOrdemServico
    {
        private uint id;

        public virtual uint Id
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

    public enum EStatusOS
    {
        EmAguardo = 1,
        EmRetirada = 2,
        ACaminhoDaEntrega = 3,
        Finalizada = 4,
        Cancelada = 5
    }
}
