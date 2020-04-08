using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMS3._1.Models
{
    public class cl_0311_BI_Produto
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public string C_Interno { get; set; }

        public int Lotes { get; set; }
        public int Posicoes { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public double Quantidade { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public double Vendido { get; set; }

        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:N}")]
        public double Disponivel { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:dd/MM/yy}")]
        public DateTime ValidadeMinima { get; set; }

        public string Status { get; set; }



    }
}