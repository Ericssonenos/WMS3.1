using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WMS3._1.Models.Serializar.NFeProca_pasta;


namespace WMS3._1.Models.Serializar
{
    [XmlRoot(ElementName = "nfeProc", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class cl_XML
    {
        [XmlAttribute("versao")]
        public string versao { get; set; }

        

        [XmlElement("NFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public cl_NFe NFe { get; set; }

        [XmlElement(ElementName = "protNFe")]
        public cl_Protocolo Protocolo { get; set; }


        public class cl_Protocolo
        {
            [XmlElement(ElementName = "infProt")]
            public cl_InfProtocolo InfProtocolo { get; set; }

            public class cl_InfProtocolo
            {
                [XmlElement(ElementName = "chNFe")]
                public string Chave { get; set; }
            }
        }
    }
}
