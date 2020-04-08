using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS3._1.Apps;
using WMS3._1.Models;

namespace WMS3._1.Controllers
{
    public class cl_03_DocaController : Controller
    {
        // GET:  cl_03_Doca
        [Authorize(Roles = "Doca")]
        public ActionResult Romaneio()
        {

            var Romaneio = new app_Romaneio().PendeteDeConferir();
            return View(Romaneio);
        }

        public ActionResult NFs_P_Conferir(int ID_Romaneio)
        {
           
            var NFs = new app_NF().PendetesDeConferir(ID_Romaneio);
            return View(NFs);
            
        }

        public ActionResult NF_P_Conferir(int _ID)
        {
            
            var NotaFiscal = new app_NF().PesquisarDetalhesDaNF(_ID);
            NotaFiscal.Lotes = new app_Lote().PesquisarPor(ID_Posicao:2,_ID_NF:_ID);
            NotaFiscal.ID = _ID;
            return View(NotaFiscal);
        }
        [HttpPost]
        public ActionResult Conferir(cl_02_Nota_Fiscal Conferir)
        {
          
                var usuario = Request.ServerVariables["LOGON_USER"];
                new app_Operacao().ConferirLote(Conferir.Lotes, usuario);
                return RedirectToAction("NF_P_Conferir", new { _ID = Conferir.ID });
            
        }

       

    }
}