using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta;

namespace WMS3._1.Models
{
    public class cl_0512_Modulo
    {
        public string Modulo { get; set; }
        public int QTD_Ruas { get; set; }
        public List<string> Niveis { get; set; }
        public int QTD_Numeros { get; set; }

        public List<cl_051_Posicao> Posicoes { get; set; }


    }
}