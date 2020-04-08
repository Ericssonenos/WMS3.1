using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta
{
    public class cl_Transporte
    {
        [XmlElement("modFrete")]
        public int Frete { get; set; }

        [XmlElement("vol")]
        public cl_Volume Volume { get; set; }


    }

    public class cl_Volume
    {
        public int qVol { get; set; }
        public string esp { get; set; }
    }

}
