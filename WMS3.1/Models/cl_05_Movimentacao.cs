using System;
using System.ComponentModel.DataAnnotations;

namespace WMS3._1.Models
{
    public class cl_05_Movimentacao
    {
        public int ID { get; set; }

        public cl_04_Acao Acao { get; set; }


        public int ID_Posicao { get; set; }
        public cl_051_Posicao Posicao { get; set; }


        public int ID_PreVenda { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public Double Quantidade { get; set; }
    }
}