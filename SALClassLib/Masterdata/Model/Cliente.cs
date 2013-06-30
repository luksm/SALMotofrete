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

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Cliente c = (Cliente)obj;

            if (c.Pessoa is PessoaFisica && this.Pessoa is PessoaFisica)
            {
                PessoaFisica pf = (PessoaFisica)c.Pessoa;
                return c.Id == this.Id && pf.Equals((PessoaFisica)this.Pessoa);
            }
            else if (c.Pessoa is PessoaJuridica && this.Pessoa is PessoaJuridica)
            {
                PessoaJuridica pj = (PessoaJuridica)c.Pessoa;
                return c.Id == this.Id && pj.Equals((PessoaJuridica)this.Pessoa);
            }
            return false;
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