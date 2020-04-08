using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS3._1.Apps;
using WMS3._1.Models;

namespace WMS3._1.Controllers
{
    public class cl_05_FornecedorController : Controller
    {
        // GET: cl_05_Fornecedor
        [Authorize(Roles = "BI")]
        public ActionResult Lista_BI()
        {
            return View(new app_Parceiro().ListasBIFornecedor());
        }
        [Authorize(Roles = "Cadastrar")]
        public ActionResult Cadastrar()
        {

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Cadastrar")]
        public ActionResult Cadastrar(cl_021_Parceiro _Fornecedor)
        {
            if (ModelState.IsValid)
            {
                new app_Parceiro().Salvar(_Fornecedor);
                return View(_Fornecedor);
            }
            return View(_Fornecedor);
        }



        public ActionResult Detalhes(int _ID_Fornecedor)
        {
            var Parceiro = new cl_021_Parceiro();
            Parceiro.ID = _ID_Fornecedor;
            Parceiro = new app_Parceiro().Pesquisar(Parceiro).FirstOrDefault();
            return View(Parceiro);
        }



        [Authorize(Roles = "Alterar")]
        public ActionResult Alterar(int _ID_Fornecedor)
        {
            var Parceiro = new cl_021_Parceiro();
            Parceiro.ID = _ID_Fornecedor;
            Parceiro = new app_Parceiro().Pesquisar(Parceiro).FirstOrDefault();
            return View(Parceiro);
        }
        [HttpPost]
        [Authorize(Roles = "Alterar")]
        public ActionResult Alterar(cl_021_Parceiro _Fornecedor)
        {

            //alterar dados
            new app_Parceiro().Atualizar(_Fornecedor);
            ViewBag.SGN = "Alterado com Sucesso";
            return View(_Fornecedor);
        }
        [Authorize(Roles = "Cadastros")]
        public ActionResult Lista_Simples_Parceiro()
        {
            var Parceiros = new List<cl_021_Parceiro>();
            Parceiros = new app_Parceiro().Pesquisar(new cl_021_Parceiro());

            return View(Parceiros);
        }
    }
}