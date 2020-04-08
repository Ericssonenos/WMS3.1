using System;
using System.ComponentModel.DataAnnotations;

namespace WMS3._1.Models
{
    public class cl_01_Romaneio
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Motorista { get; set; }


        public Double Total { get; set; }
        public cl_02_Nota_Fiscal NF { get; set; }
        public string Usuario { get; set; }
        public DateTime Data { get; set; }

        //Cáuculos SQL
        public int Qtd_NF { get; set; }
        public int Qtd_Produtos { get; set; }
        public int Qtd_Lotes { get; set; }

    }
}