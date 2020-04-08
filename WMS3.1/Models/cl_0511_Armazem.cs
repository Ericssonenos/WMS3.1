using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS3._1.Models
{
    public class cl_0511_Armazem
    {
        public cl_0512_Modulo Modulo { get; set; }
        public cl_03_Lote Lote { get; set; }
        public List<cl_03_Lote> Lotes { get; set; }
        public cl_051_Posicao Posicao { get; set; }
        public int ID_NF { get; set; }
        public int ID_Saida { get; set; }


        


    }
}