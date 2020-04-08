using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS3._1.Apps;
using WMS3._1.Models;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta;

namespace WMS3._1.Controllers
{
    public class cl_06_ProdutoController : Controller
    {
        // GET: cl_06_Produto
        [Authorize(Roles = "BI")]
        public ActionResult Lista_BI()
        {

            return View(new app_Produto().Lista_BI());
        }
        [Authorize(Roles = "BI")]
        public ActionResult Detalhes(int _ID_Produto)
        {


            var saco = new cl_BI_DetalheProduto();

            var Produto = new cl_Produto();
            Produto.ID = _ID_Produto;
            Produto = new app_Produto().Pesquisa(Produto).FirstOrDefault();

            var ListaBI = new app_Lote().Lista_BI(_ID_Produto);

            saco.Basico = Produto;
            saco.LBI = ListaBI;
            saco.Quantidades = new app_Produto().Lista_BI(_ID_Produto: _ID_Produto).FirstOrDefault();
            saco.Posicoes = new app_Lote().Lista_BI_Posicao(_ID_Produto);
            saco.PreVendas = new app_Prevenda().ListaPendentes();

            return View(saco);

        }
        [Authorize(Roles = "Alterar")]
        [Authorize(Roles = "BI")]
        public ActionResult Alterar(int _ID_Produto) 
        {
            var Produto = new cl_Produto();
            Produto.ID = _ID_Produto;
            return View(new app_Produto().Pesquisa(Produto).FirstOrDefault());
        }

       

    }
}