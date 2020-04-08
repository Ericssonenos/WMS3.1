using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS3._1.Apps;
using WMS3._1.Models; 

namespace WMS3._1.Controllers
{
    public class cl_02_RecebimentoController : Controller
    {
        // GET: cl_02_Recebimento 13:32 27/12/2019
        [Authorize(Roles = "Entrada")]
        public ActionResult Entrada()
        {
            
            ViewBag.Romaneio = "Pesquisar";
            var saco = new cl_BI_Recebimento();
            saco.Romaneio = new cl_01_Romaneio();
            saco.NFs = new List<cl_02_Nota_Fiscal>();
            return View(saco);
            
        }

        [HttpPost]
        public ActionResult Entrada(cl_BI_Recebimento _romaneio)
        {
            var saco = new cl_BI_Recebimento();
            saco.Romaneio = _romaneio.Romaneio;
            _romaneio.Romaneio.Usuario = Request.ServerVariables["LOGON_USER"];
            if (String.IsNullOrEmpty(_romaneio.Romaneio.Nome))
            {
         
                return RedirectToAction("Romaneio", new { _Romaneio = _romaneio.Romaneio.Nome });
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Preenha todos os Campos";
                @ViewBag.Romaneio = "Romaneio";
               
                saco.NFs = new List<cl_02_Nota_Fiscal>();
                return View(saco);
            }
            var cadastrar = new app_Operacao().EntradaRomaneio(_romaneio.Romaneio);
            

            if (cadastrar == -1)
            {
                ViewBag.Error = "   XML Não cadastrado";
                saco.NFs = new List<cl_02_Nota_Fiscal>();
                return View(saco);

            }
            else if (cadastrar>0)
            {
                ViewBag.Error = "   NF já registrada";
                saco.NFs = new List<cl_02_Nota_Fiscal>();
                return View(saco);
            }
            else
            {
                ViewBag.Sucesso = "Cadastrado com Sucesso";
               
                saco.NFs = new app_NF().PendetesDeConferir(_romaneio.Romaneio.ID);
                return View(saco);

                
            }

           
        }

       
        public ActionResult Romaneio( string _Romaneio)
        {
            var saco = new cl_BI_Recebimento();
            
                if (String.IsNullOrEmpty(_Romaneio))
                {
                    saco.Romaneio = new cl_01_Romaneio();
                    saco.Romaneio.Nome = new app_Romaneio().Gerar();
                    saco.NFs = new List<cl_02_Nota_Fiscal>();
                    ViewBag.Romaneio = "Romaneio";
                    return View("Entrada", saco);
                }

            cl_01_Romaneio romaneio = new app_Romaneio().PesquisarRomaneio(_Romaneio);
            if (String.IsNullOrEmpty(romaneio.Motorista))
            {
                ViewBag.Romaneio = "Romaneio";
            }

            saco.Romaneio = romaneio;
            saco.NFs = new app_NF().PendetesDeConferir(romaneio.ID);
            return View("Entrada", saco);
           
        }
    }



}