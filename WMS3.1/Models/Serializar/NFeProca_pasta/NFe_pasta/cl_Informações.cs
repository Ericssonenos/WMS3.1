using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta
{
    // desativado
     public class cl_Informações
    {
        [XmlElement("infCpl")]
        public string Informacao { get; set; }
       
    }
}
 