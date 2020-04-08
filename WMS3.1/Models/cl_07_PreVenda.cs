using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS3._1.Models
{
    public class cl_07_PreVenda
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public cl_021_Parceiro Parceiro { get; set; }

        public int Itens { get; set; }
        public double Volume { get; set; }

        public DateTime Data { get; set; }

        public string Status { get; set; }
    }
}