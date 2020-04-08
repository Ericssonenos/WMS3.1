using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta;

namespace WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta

{
    public class cl_Detalhes
    {
        [XmlElement("nItem")]
        public int Item { get; set; }

        [XmlElement("prod")]
        public cl_Produto Produto { get; set; }
    }
}
