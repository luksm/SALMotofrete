using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.Masterdata.Model;
using Iesi.Collections.Generic;

namespace SALClassLib.OS.Model
{
    public class OrdemServico
    {
        private ulong idOrdemServico;

        public virtual ulong IdOrdemServico
        {
            get { return idOrdemServico; }
            set { idOrdemServico = value; }
        }

        private ulong numero;

        public virtual ulong Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        private String observacao;

        public virtual String Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        private StatusOrdemServico status;

        public virtual StatusOrdemServico Status
        {
            get { return status; }
            set { status = value; }
        }

        private Cobranca cobranca;

        public virtual Cobranca Cobranca
        {
            get { return cobranca; }
            set { cobranca = value; }
        }

        private ISet<ItemOrdemServico> itens;

        public virtual ISet<ItemOrdemServico> Itens
        {
            get { return itens; }
            set { itens = value; }
        }

        private ISet<EnderecoOrdemServico> enderecosRetirada;

        public virtual ISet<EnderecoOrdemServico> EnderecosRetirada
        {
            get { return enderecosRetirada; }
            set { enderecosRetirada = value; }
        }

        private ISet<EnderecoOrdemServico> enderecosEntrega;

        public virtual ISet<EnderecoOrdemServico> EnderecosEntrega
        {
            get { return enderecosEntrega; }
            set { enderecosEntrega = value; }
        }

        private Cliente cliente;

        public virtual Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            OrdemServico os = (OrdemServico)obj;

            if (os.idOrdemServico == this.idOrdemServico) return true;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = 13 * numero.GetHashCode() + data.GetHashCode();
                return result;
            }
        }
    }
}
