using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta;

namespace WMS3._1.Models
{
    public class cl_03_Lote
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public cl_Produto Produto { get; set; }
        public cl_02_Nota_Fiscal NF { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime Fabricacao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime Validade { get; set; }
        public Double Valor { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public Double Quantidade { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public Double Conferir { get; set; }
        public List<cl_04_Acao> Acoes { get; set; }
        public string Status { get; set; }

        public string PreVenda { get; set; }

        public cl_051_Posicao Posicao {get;set;}

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString =  "{0:N}")]
        public Double Vendido { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public Double Disponível { get; set; }

        public List<cl_051_Posicao> Posicoes { get; set; }

    }
}