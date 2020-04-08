using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta.Rastro_pasta;

namespace WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta
{
    public class cl_Produto
    {

        private float estoqueMinimo;
        private float altura;
        private float largura;
        private float comprimento;
        private float peso;
        private float qtdMaxima;
        private float pesoMaximo;
        private float valor;


        //----------------------------------------------------------------------

        [Range(01,999999, ErrorMessage = "Defina o ID Interno do Produto")]
        public int ID { get; set; }

        [XmlElement("cProd")]
        public string CodigoF { get; set; }

        [XmlElement("xProd")]
        public string NomeF { get; set; }

        [XmlElement("uCom")]
        public string Unidade_Medida { get; set; }

        [Compare("Conferir",ErrorMessage ="Os Total dos lotes não corresponse ao total da NF")]
        [XmlElement("qCom")]
        public double Quantidade { get; set; }

        [XmlElement("vUnCom")]
        public double Valor { get; set; }

        [XmlElement("rastro")]
        public List<cl_Rastro> rastro { get; set; }

        //------------------------------------------------------------------------------


        public string NomeI { get; set; }
   
       
        public string CodigoI { get; set; }

        public string Parceiro { get; set; }
        
        public double Conferir { get; set; }

        public float EstoqueMinimo
        {
            get => estoqueMinimo;
            set
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    estoqueMinimo = 0;
                }
                else
                {
                    estoqueMinimo = value;
                }
            }
        }

        public float Altura
        {
            get => altura;
            set
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    altura = 0;
                }
                else
                {
                    altura = value;
                }
            }
        }

        public float Largura
        {
            get => largura; set
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    largura = 0;
                }
                else
                {
                    largura = value;
                }
            }
        }

        public float Comprimento
        {
            get => comprimento; set
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    comprimento = 0;
                }
                else
                {
                    comprimento = value;
                }
            }
        }

        public float Peso
        {
            get => peso; set
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    peso = 0;
                }
                else
                {
                    peso = value;
                }
            }
        }

        public float QtdMaxima
        {
            get => qtdMaxima; set
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    qtdMaxima = 0;
                }
                else
                {
                    qtdMaxima = value;
                }
            }
        }

        public float PesoMaximo
        {
            get => pesoMaximo; set
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    pesoMaximo = 0;
                }
                else
                {
                    pesoMaximo = value;
                }
            }
        }

        public float ValorSaida
        {
            get => valor; set
            {
                if (String.IsNullOrEmpty(value.ToString()))
                {
                    valor = 0;
                }
                else
                {
                    valor = value;
                }
            }
        }

        //public string cEAN { get; set; }
        //public double vUnCom { get; set; }
        //public string NCM { get; set; }
        //public string CFOP { get; set; }
    }
}
