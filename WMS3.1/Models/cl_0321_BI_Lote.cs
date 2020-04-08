using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMS3._1.Models
{
    public class cl_0321_BI_Lote
    {
        public int ID { get; set; }
        public string Lote { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public Double Quantidade { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? Compras { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy HH:mm}")]
        public DateTime? Entrada { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public double QTD_Entrada { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy HH:mm}")]
        public DateTime? Conferencia { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy HH:mm}")]
        public DateTime? Armazenar { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public double QTD_Armazenar { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy HH:mm}")]
        public DateTime? Inventario { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? Validade { get; set; }


        public int Posicoes { get; set; }
    }
}