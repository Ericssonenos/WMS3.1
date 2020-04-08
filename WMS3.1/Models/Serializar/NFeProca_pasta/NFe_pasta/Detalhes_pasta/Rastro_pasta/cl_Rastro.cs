using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WMS3._1.Apps;

namespace WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta.Rastro_pasta
{
    public class cl_Rastro
    {
        [Required(ErrorMessage = "Preencha o campo de Loite")]
        [XmlElement("nLote")]
        public string Lote { get; set; }

        [Required(ErrorMessage = "Preencha o campo de quantidade")]
        [Range(1,100000,ErrorMessage ="A quantidade deve estar entre 1 e 100.000")]
        [XmlElement("qLote")]
        public Double Quantidade { get; set; }


        //[DataBrasil(ErrorMessage = "Preencha o campo de fabricação com uma daca correta", DataRequerida = false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy }")]
        [XmlElement("dVal")] 
        public DateTime Validade { get; set; }

       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [XmlElement("dFab")]
        //[DataBrasil(ErrorMessage = "Preencha o campo de fabricação com uma daca correta", DataRequerida = false)]
        public DateTime Fabricacao { get; set; }

    }

}