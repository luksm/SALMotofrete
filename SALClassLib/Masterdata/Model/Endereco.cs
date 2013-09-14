using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALClassLib.Masterdata.Model
{
    public class Endereco : ICloneable
    {
        public Endereco()
        {
            this.municipio = new Municipio();
        }

        public Endereco(JToken token)
        {
            if(this.municipio == null)
                this.municipio = new Municipio();
            if (this.municipio.Estado == null)
                this.municipio.Estado = new Estado();

            JToken address_components = token.SelectToken("results").First().SelectToken("address_components");

            this.latitude = float.Parse(token.SelectToken("results").First().SelectToken("geometry").SelectToken("location").SelectToken("lat").ToString());
            this.longitude = float.Parse(token.SelectToken("results").First().SelectToken("geometry").SelectToken("location").SelectToken("lng").ToString());

            //iteração para ler os endereços
            foreach (var item in address_components)
            {
                String tipo = item.SelectToken("types").First().ToString();
                if (tipo.Equals("street_number"))
                {
                    this.numero = item.SelectToken("long_name").ToString();
                }
                else if (tipo.Equals("route"))
                {
                    this.logradouro = item.SelectToken("long_name").ToString();
                }
                else if (tipo.Equals("sublocality"))
                {
                    this.bairro = item.SelectToken("long_name").ToString();
                }
                else if (tipo.Equals("locality"))
                {
                    this.municipio.Nome = item.SelectToken("long_name").ToString();
                }
                else if (tipo.Equals("administrative_area_level_1"))
                {
                    this.municipio.Estado.Nome = item.SelectToken("long_name").ToString();
                }
                else if (tipo.Equals("postal_code"))
                {
                    this.cep = item.SelectToken("long_name").ToString();
                }
            }
        }

        private uint id;

        public virtual uint Id
        {
            get { return id; }
            set { id = value; }
        }

        private String logradouro;

        [Required(ErrorMessage="O logradouro deve ser preenchido")]
        [MaxLength(80, ErrorMessage="O logradouro deve conter no máximo 80 caracteres")]
        public virtual String Logradouro
        {
            get { return logradouro; }
            set { logradouro = value; }
        }

        private String bairro;

        [Required(ErrorMessage = "O bairro deve ser preenchido")]
        [MaxLength(20, ErrorMessage = "O bairro deve conter no máximo 20 caracteres")]
        public virtual String Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        private String cep;

        [Required(ErrorMessage = "O CEP deve ser preenchido")]
        [MaxLength(9, ErrorMessage = "O CEP deve conter no máximo 8 caracteres")]
        public virtual String Cep
        {
            get { return cep; }
            set { cep = value; }
        }

        private String numero;

        [Required(ErrorMessage = "O número deve ser preenchido")]
        [MaxLength(20, ErrorMessage = "O número deve conter no máximo 20 caracteres")]
        public virtual String Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private String complemento;

        [MaxLength(255, ErrorMessage = "O complemento deve conter no máximo 255 caracteres")]
        public virtual String Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }

        private String observacao;

        [MaxLength(2000, ErrorMessage = "A observação deve conter no máximo 2000 caracteres")]
        public virtual String Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        private Municipio municipio;

        public virtual Municipio Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }

        private Pessoa pessoa;

        public virtual Pessoa Pessoa
        {
            get { return pessoa; }
            set { pessoa = value; }
        }

        public virtual String getEndereco() {
            return Logradouro + "," +
                   Numero + " - " +
                   Bairro + ", " +
                   Municipio.Nome + " - " + Municipio.Estado.Sigla;
        }

        public virtual float latitude { get; set; }

        public virtual float longitude { get; set; }
                

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Endereco e = (Endereco)obj;
            return e.Id == this.Id;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                return base.GetHashCode();
            }
        }

        public virtual object Clone()
        {
            Endereco end = new Endereco();
            end.Id = this.id;
            end.Bairro = this.bairro;
            end.Complemento = this.complemento;
            end.Logradouro = this.logradouro;
            end.Municipio = this.municipio;
            end.Numero = this.numero;
            end.Observacao = this.observacao;
            return end;
        }
    }
}
