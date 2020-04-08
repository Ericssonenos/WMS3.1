using System;
using System.Collections.Generic;
using WMS3._1.Models;

namespace WMS3._1.Models
{
    public class cl_04_Acao
    {
        

        public int ID { get; set; }
        public cl_03_Lote Lote { get; set; }
        public string Nome { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string Usuario { get; set; }
        public string OBS { get; set; }

        public List<cl_05_Movimentacao> Movimentacoes { get; set; }
        



    }
}