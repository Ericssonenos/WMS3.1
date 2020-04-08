using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS3._1.Models;
using WMS3._1.Apps;

namespace WMS3._1.Controllers
{
    public class cl_07_PreVendaController : Controller
    {
        // GET: cl_07_PreVenda
        [Authorize(Roles = "PreVenda")]
        public ActionResult Lista()
        {

            return View(new app_Prevenda().ListaPendentes());
        }
       

        [Authorize(Roles = "PreVenda")]
        public ActionResult Separar_Produto( int _ID_Produto, int _ID_Prevenda, float _Conferir)
        {
            string Usuario = Request.ServerVariables["LOGON_USER"];
            new app_Operacao().SepararPreVenda(_ID_Produto, _ID_Prevenda, _Conferir, Usuario);

            return RedirectToAction("Detalhes", "cl_06_Produto",new { _ID_Produto = _ID_Produto });
        }

        [Authorize(Roles = "PreVenda")]
        public ActionResult Lista_Lote(string _PreVenda, string _Status)
        {
            ViewBag.PreVenda = _PreVenda;
            ViewBag.Status = _Status;
            return View(new app_Lote().Lista_PreVendas(_PreVenda));
        }
        [Authorize(Roles = "PreVenda")]
        public ActionResult Lista_Lote_Impressao(string _PreVenda)
        {
            ViewBag.Usuario = Request.ServerVariables["LOGON_USER"];
            ViewBag.PreVenda = _PreVenda;
            
            ViewBag.PreVenda = _PreVenda;
            return View(new app_Lote().Lista_PreVendas(_PreVenda));
        }

    }
}