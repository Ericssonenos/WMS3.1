using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Dest_Emite_pasta
{
    public class cl_Endereco
    {  
        public  cl_Endereco()
        {
           if(xLgr == null) xLgr = string.Empty;
            if (nro == null) nro = string.Empty;
            if (xCpl == null) xCpl = string.Empty;
            if (xBairro == null) xBairro = string.Empty;
            if (cMun == null) cMun = string.Empty;
            if (xMun == null) xMun = string.Empty;
            if (UF == null) UF = string.Empty;
            if (CEP == null) CEP = string.Empty;
            if (xCpl == xPais) xPais = string.Empty;
            if (fone == null) fone = string.Empty;
        } 
        
        public string xLgr { get; set; }
        public string nro { get; set; }
        public string xCpl  { get; set; }
     
        public string xBairro { get; set; }
        public string cMun { get; set; }
        public string xMun { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public int cPais { get; set; }
        public string xPais { get; set; }
        public string fone { get; set; }
    }
}
