using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta
{
    public class cl_Identificacao
    {
        


        //public int cUF { get; set; }
        //public string cNF { get; set; }

        //[XmlElement("natOp")]
        [XmlElement("natOp")]
        public string Natureza_Operacao { get; set; }

        //public int indPag { get; set; }
        //public string mod { get; set; }
        public int serie { get; set; }


        [XmlElement("nNF")]
        public string Numero { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        [XmlElement("dhEmi")]
        public DateTime Emisao { get; set; }
      

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime Previsao { get; set; }

        //[XmlAttribute("dhSaiEnt")]
        //public DateTime dhSaiEnt { get; set; }

    }
}
