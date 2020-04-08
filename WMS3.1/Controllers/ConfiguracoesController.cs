using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS3._1.Apps;
using WMS3._1.Models;

namespace WMS3._1.Controllers
{
    public class ConfiguracoesController : Controller
    {
        // GET: Configuracoes
        public ActionResult Index()
        {

            return View();
        }

     

        public ActionResult TEsteArmazem()
        {

            new app_Armazem().CriarPosicoes();
            

            return View();
        }
    }
}