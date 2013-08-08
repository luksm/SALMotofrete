using System;
using Iesi.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SALClassLib.OS.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SALClassLib.Masterdata.Model
{
    public class Entregador : PessoaFisica 
    {
        private String placaMoto;

        [DisplayName("Placa da moto")]
        [Required(ErrorMessage="Informe a placa da moto")]
        [MaxLength(10, ErrorMessage = "A placa da moto deve conter no máximo 10 caracteres")]
        [MinLength(8, ErrorMessage = "A placa da moto deve conter no mínimo 8 caracteres")]
        public virtual String PlacaMoto
        {
            get { return placaMoto; }
            set { placaMoto = value; }
        }

        private String modeloMoto;

        [DisplayName("Modelo da moto")]
        [Required(ErrorMessage="Informe o modelo da moto")]
        [MaxLength(50, ErrorMessage = "O modelo da moto deve conter no máximo 50 caracteres")]
        public virtual String ModeloMoto
        {
            get { return modeloMoto; }
            set { modeloMoto = value; }
        }

        private short statusAtividade;

        public virtual short StatusAtividade
        {
            get { return statusAtividade; }
            set { statusAtividade = value; }
        }

        private ISet<AtividadeEntregador> historicoAtividade;

        public virtual ISet<AtividadeEntregador> HistoricoAtividade
        {
            get { return historicoAtividade; }
            set { historicoAtividade = value; }
        }

        private AparelhoMovel aparelhoMovel;
        
        public virtual AparelhoMovel AparelhoMovel
        {
            get { return aparelhoMovel; }
            set { aparelhoMovel = value; }
        }

        private String posicaoAtual;

        [DisplayName("Posição Atual")]
        public virtual String PosicaoAtual
        {
            get { return posicaoAtual; }
            set { posicaoAtual = value; }
        }
        
        private ISet<FilaOrdemServico> fila;

        public virtual ISet<FilaOrdemServico> Fila
        {
            get { return fila; }
            set { fila = value; }
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Entregador e = (Entregador)obj;

            return e.Id == this.Id;
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
