using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WMS3._1.Models
{
    public class cl_02_Nota_Fiscal
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Chave { get; set; }
        public string Numero { get; set; }
        public string Serie { get; set; }

        public string Pedido { get; set; }

        public DateTime Emissao { get; set; }
        public DateTime Previsao { get; set; }
        public string Natureza { get; set; }

        public cl_021_Parceiro Destinatario { get; set; }

        public cl_021_Parceiro Fornecedor { get; set; }

        public  List<cl_03_Lote> Lotes { get; set; }

        public cl_03_Lote Lote { get; set; }

        public int Qtd_Produtos { get; set; }
        public int Qtd_Lotes { get; set; }
        public Double Total { get; set; }

        public string Status { get; set; }
    }
}