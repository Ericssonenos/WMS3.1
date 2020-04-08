using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Dest_Emite_pasta;

namespace WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta
{
    public class cl_Emitente
    {

        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:000\\.000\\.000-00}", ApplyFormatInEditMode = true)]
        public string CNPJ { get; set; }
        public string Codigo { get; set; }
        public string xNome { get; set; }
        public string xFant { get; set; }
        [XmlElement("enderEmit")]
        public cl_Endereco Endereco { get; set; }
          
        public string IE { get; set; }
        public string IEST { get; set; }
        public int CRT { get; set; }

        
    }
}
