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
    public class cl_04_ArmazemController : Controller
    {


        public class PostArmazenar
        {
            public int IDLote { get; set; }
            public string Modulo { get; set; }

        }
        // GET: cl_04_Armazem
        [Authorize(Roles = "Armazem")]
        public ActionResult Armazem(string _Modulo ="A",int _ID_Lote =0,int _ID_Origem =2)
        {
            var Armazem = new cl_0511_Armazem();
            int ID_Produto =0;
            Armazem.Lote = new app_Lote().PesquisarPor(ID_Posicao: _ID_Origem, ID_Lote: _ID_Lote).FirstOrDefault();
            Armazem.ID_NF = new app_NF().PendetesDeArmazenar(_ID_Lote);
            if (Armazem.Lote == null && Armazem.ID_NF != -1)
            {
                return RedirectToAction("NF_P_Conferir", "cl_03_Doca", new { _ID = Armazem.ID_NF });
            }
            if (Armazem.Lote != null)
                ID_Produto = Armazem.Lote.Produto.ID;
             Armazem.Modulo = new app_Armazem().GerarArmazem(_Modulo, ID_Produto);
            // pesquisar detalhes do lote.
            
            Armazem.ID_Saida = _ID_Origem;
           

            return View(Armazem);
        }
        
        public ActionResult Armazenar(cl_0511_Armazem Ps)
        {
            var usuario = Request.ServerVariables["LOGON_USER"];
            new app_Operacao().ArmazenarLote(Ps, usuario);


            return RedirectToAction("Armazem", new { _Modulo = Ps.Modulo.Modulo, _ID_Lote = Ps.Lote.ID, _ID_Origem = Ps.ID_Saida });
        }
       
        public ActionResult DetalhesPosicao(int _ID_Posicao)
        {
            var Armazem = new cl_0511_Armazem();
           
            Armazem.Posicao = new app_Armazem().PesquisarPosicao(_ID_Posicao);

            Armazem.Lotes = new app_Lote().PesquisarPor(ID_Posicao: Armazem.Posicao.ID) ;
            return View(Armazem);
        }

        public ActionResult BloquearPosicao( int _posicao)
        {
            new app_Armazem().Bloqueio(_posicao);

            return RedirectToAction("DetalhesPosicao", "cl_04_Armazem", new { _ID_Posicao = _posicao });

        }

   

    }
}