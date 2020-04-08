using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta;

namespace WMS3._1.Models
{
    public class cl_BI_DetalheProduto
    {
       
            public cl_Produto Basico { get; set; }

        //lista de lotes por posição
            public List<cl_03_Lote> Posicoes { get; set; }

        // Lista de lotes por histórico de entrada
            public List<cl_0321_BI_Lote> LBI { get; set; }
        
        // lista de prevendas 
           public List<cl_07_PreVenda> PreVendas { get; set; }
        // Quantidades
          public cl_0311_BI_Produto Quantidades { get; set; }
        
    }
}