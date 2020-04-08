using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMS3._1.Models
{
    public class cl_0212_BI_Parceiros
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CNPJ_CPF { get; set; }
        public int Produtos { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.0,##}")]
        public double Quantidade { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd/MM/yy}")]
        public DateTime Primeira_Compra { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd/MM/yy}")]
        public DateTime Ultima_Compra { get; set; }
    }
}