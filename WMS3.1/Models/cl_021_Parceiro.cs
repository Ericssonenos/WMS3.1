
using System.ComponentModel.DataAnnotations;

namespace WMS3._1.Models

{
    public class cl_021_Parceiro
    {

        public cl_021_Parceiro()
        {
            if (string.IsNullOrEmpty(Codigo)) Codigo = "";
            if (string.IsNullOrEmpty(Nome)) Nome = "";
            if (string.IsNullOrEmpty(Logaritimo)) Logaritimo = "";
            if (string.IsNullOrEmpty(Codigo_Municipio)) Codigo_Municipio = "";
            if (string.IsNullOrEmpty(Municipio)) Municipio = "";
            if (string.IsNullOrEmpty(CEP)) CEP = "";
            if (string.IsNullOrEmpty(Codigo_Pais)) Codigo_Pais = "";
            if (string.IsNullOrEmpty(UF)) UF = "";
            if (string.IsNullOrEmpty(Telefone)) Telefone = "";
            if (string.IsNullOrEmpty(Status)) Status = "";
        }

        public int ID { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Codigo { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]

        [DisplayFormat(DataFormatString = "{0:000\\.000\\.000-00}", ApplyFormatInEditMode = true)]
        public string CNPJ_CPF { get; set; }
        public string Logaritimo { get; set; }
        public string Codigo_Municipio { get; set; }
        public string Municipio { get; set; }
        public string CEP { get; set; }
        public string Codigo_Pais { get; set; }
        public string Pais { get; set; }

        public string UF { get; set; }
        public string Telefone { get; set; }
        public string Status { get; set; }



    }
}