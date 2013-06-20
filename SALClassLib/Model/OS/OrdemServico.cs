using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.Model.Masterdata;

namespace SALClassLib.Model.OS
{
    public class OrdemServico
    {
        private ulong id;

        public ulong Id
        {
            get { return id; }
            set { id = value; }
        }

        private ulong numero;

        public ulong Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private DateTime data;

        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        private String observacao;

        public String Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        private StatusOrdemServico status;

        public StatusOrdemServico Status
        {
            get { return status; }
            set { status = value; }
        }

        private Cobranca cobranca;

        public Cobranca Cobranca
        {
            get { return cobranca; }
            set { cobranca = value; }
        }

        private ISet<ItemOrdemServico> itens;

        public ISet<ItemOrdemServico> Itens
        {
            get { return itens; }
            set { itens = value; }
        }

        private ISet<EnderecoOrdemServico> enderecosRetirada;

        public ISet<EnderecoOrdemServico> EnderecoRetirada
        {
            get { return enderecosRetirada; }
            set { enderecosRetirada = value; }
        }

        private ISet<EnderecoOrdemServico> enderecosEntrega;

        public ISet<EnderecoOrdemServico> EnderecosEntrega
        {
            get { return enderecosEntrega; }
            set { enderecosEntrega = value; }
        }

        private Cliente cliente;

        public Cliente Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }
        

    }
}
