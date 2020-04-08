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
    public class cl_Destinatario
    {
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{#:###\\.###\\.###-##}", ApplyFormatInEditMode = true)]
        public string CNPJ { get; set; }
        public string Codigo { get; set; }
        [DisplayFormat(DataFormatString = "{#:###\\.###\\.###-##}", ApplyFormatInEditMode = true)]
        public string CNPJ_CPF { get; set; }
        [DisplayFormat(DataFormatString = "{#:###\\.###\\.###-##}", ApplyFormatInEditMode = true)]
        public string CPF { get; set; }
        public string xNome { get; set; }
        [XmlElement("enderDest")]
        public cl_Endereco Endereco { get; set; }
        public string email { get; set; }

    }
}
