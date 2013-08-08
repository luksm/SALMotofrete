using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.Masterdata.Model;
using Iesi.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


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

        [DisplayName("Observação")]
        public virtual String Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        public virtual String DataFormatada
        {
            get { return data.ToString("dd/MM/yyyy HH:mm:ss"); }
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

        private EnderecoRetirada enderecoRetirada;

        public virtual EnderecoRetirada EnderecoRetirada
        {
            get { return enderecoRetirada; }
            set { enderecoRetirada = value; }
        }

        private EnderecoEntrega enderecoEntrega;

        public virtual EnderecoEntrega EnderecoEntrega
        {
            get { return enderecoEntrega; }
            set { enderecoEntrega = value; }
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

            if (os.id == this.id) return true;

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
