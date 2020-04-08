using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta;

namespace WMS3._1.Models.Serializar.NFeProca_pasta
{
   
    public class cl_NFe
    {
        [XmlElement(ElementName = "infNFe")]
        public cl_InfNFe InfNFe { get; set; }

       

        public class cl_InfNFe
        {
            //[XmlAttribute("Id")]
            //public string ID { get; set; }

            [XmlElement("ide")]
            public cl_Identificacao Identificacao { get; set; }

            [XmlElement("emit")]
            public cl_Emitente Emitente { get; set; }

            [XmlElement("dest")]
            public cl_Destinatario Destinatario { get; set; }

            [XmlElement("det")]
            public List<cl_Detalhes> Detalhes { get; set; }

            //[XmlElement("transp")]
            //public Transporte Transporte { get; set; }

            //[XmlElement("infAdic")]
            //public Informações Informações { get; set; }

            [XmlElement("compra")]
            public cl_Compra Compra { get; set; }
            public class cl_Compra
            {
                private string _pedido { get; set; }
                [Required(ErrorMessage ="Preencha o campo de pedido")]
                [StringLength(30,MinimumLength = 1,ErrorMessage ="Valor dever estar entre 1 e 30 caracteres")]
                [XmlElement("xPed")]
                public string Pedido { get; set; }

            }

            public string Usuario { get; set; }

            public string Chave { get; set; }

        }

        
        
    }
}
