using System;
using System.Web.Mvc;
using WMS3._1.Apps;
using WMS3._1.Models.Serializar;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta;
using static WMS3._1.Models.Serializar.NFeProca_pasta.cl_NFe;

namespace WMS3._1.Controllers
{
    public class cl_01_ComprasController : Controller
    {
        [Authorize (Roles = "UpLoad")]
        public ActionResult Upload()
        {
            
            ViewBag.Error = TempData["msg"] as string;
            //Apresenta apenas o botão para Dowload
            cl_XML _NFe = new cl_XML();
            _NFe = new app_NF().AddNFVazia();
            var lista = new app_Produto().Idsdisponiveis();
            ViewBag.IDs = new SelectList(lista, "ID", "CodigoI");
            return View(_NFe.NFe.InfNFe);
        }

        [HttpPost]
        public ActionResult Upload(cl_Arquivo Arq)
        {
            app_Serializar serializar = new app_Serializar();
            var _NFE = new cl_XML();



            try
            {
                _NFE = serializar.serializarobjeto<cl_XML>(Arq.Xml.InputStream);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Upload");
            }

            if (_NFE.NFe.InfNFe.Compra == null)
            {
                _NFE.NFe.InfNFe.Compra = new cl_InfNFe.cl_Compra();
                _NFE.NFe.InfNFe.Compra.Pedido = new app_NF().GerarPedido();
            }

            _NFE = new app_Produto().Organizar(_NFE);
            int ID_ROMANEIO = new app_NF().SeExisteNF_e_Romanio(_NFE.Protocolo.InfProtocolo.Chave);

            


            var lista = new app_Produto().Idsdisponiveis();
            ViewBag.IDs = new SelectList(lista, "ID", "CodigoI");

           
            
            if (ID_ROMANEIO == -1)
                return View(new app_Parceiro().Organizar(_NFE));

            TempData["msg"]  = "Nota Fiscal: "+ _NFE.NFe.InfNFe.Identificacao.Numero +" já cadastrada";
            return RedirectToAction("Upload");
          
        }

        [HttpPost]
        public ActionResult Cadastrar(cl_InfNFe nFe)
        {

            if (ModelState.IsValid)
            {
              
                    nFe.Usuario = Request.ServerVariables["LOGON_USER"];
                    if (!new app_Operacao().RegistrarXML(nFe))
                    {
                        ViewBag.Error = "Nota Fiscal já cadastrada";
                        return View("Upload", nFe);
                    }


                    //RedirectToAction b
                    TempData["msg"] = "Cadastrado com sucesso";

                    return RedirectToAction("Upload");
               
                   

                
            }
            else
            {
                var lista = new app_Produto().Idsdisponiveis();
                ViewBag.IDs = new SelectList(lista, "ID", "CodigoI");
                ViewBag.Validar = "validar";
                return View("Upload", nFe);
            }


        }


        [Authorize(Roles = "Pendente_de_Entrga")]
        public ActionResult Pendetes()
        {
            
            var NFs = new app_NF().PendetesDeEntrega();
            return View(NFs);
        }
        [Authorize(Roles = "Pendente_de_Entrga")]
        public ActionResult NF_P_Recebimento(int _ID)
        {
            var NotaFiscal = new app_NF().PesquisarDetalhesDaNF(_ID);
            NotaFiscal.Lotes = new app_Lote().PesquisarPor(_ID);
            return View(NotaFiscal);
        }
    }
}