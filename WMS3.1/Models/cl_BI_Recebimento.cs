using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS3._1.Models
{
    public class cl_BI_Recebimento
    {
        public List<cl_02_Nota_Fiscal> NFs { get; set; }
        public cl_01_Romaneio Romaneio { get; set; }
    }
}