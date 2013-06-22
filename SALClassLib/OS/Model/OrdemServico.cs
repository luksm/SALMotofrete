using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.Masterdata.Model;

namespace SALClassLib.OS.Model
{
    public class OrdemServico
    {
        private ulong id;

        public virtual ulong Id
        {
            get { return id; }
            set { id = value; }
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

        public virtual ISet<EnderecoOrdemServico> EnderecoRetirada
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
    }
}
