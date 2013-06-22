using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.OS.Model;

namespace SALClassLib.Masterdata.Model
{
    public class Cliente
    {
        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private Pessoa pessoa;

        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
        }

        private ISet<OrdemServico> ordensServico;

        public virtual ISet<OrdemServico> OrdensServico
        {
            get { return ordensServico; }
            set { ordensServico = value; }
        }
    }
}