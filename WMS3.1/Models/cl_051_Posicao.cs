using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMS3._1.Models
{
    public class cl_051_Posicao
    {
        public int ID { get; set; }
        public string Modulo { get; set; }
        public int Rua { get; set; }
        public string Nivel { get; set; }
        public int Numero { get; set; }
       
        public string Nome { get; set; }
        public string Setor { get; set; }
        public string Status { get; set; }

        //Movimentação soma
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public double Quantidade { get; set; }

        //quantidade expecífic do produto 
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public double QuantidadeP { get; set; }
        public string Class_Posicao { get; set; }
        //para conferir os valores
        public int material { get; set; }
    }
}