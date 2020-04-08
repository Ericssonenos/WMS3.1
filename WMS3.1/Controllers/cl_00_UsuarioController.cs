using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMS3._1.Apps;
using WMS3._1.Models.Sistema;

namespace WMS3._1.Controllers
{
    public class cl_00_UsuarioController : Controller
    {
        // GET: cl_00_Usuario

        ApplicationDbContext db = new ApplicationDbContext();
        
        [Authorize(Roles ="Usuarios")]
        public ActionResult Index()
        {
            var GerenciarUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var Usuarios = GerenciarUsuario.Users.ToList();
            var UsuariosView = new List<UserView>();

            foreach (var Usuario in Usuarios)
            {
                var UsuarioView = new UserView
                {
                    Email = Usuario.Email,
                    Nome = Usuario.UserName,
                    UserId = Usuario.Id
                };
                UsuariosView.Add(UsuarioView);
            }

            return View(UsuariosView);
        }

        public ActionResult Role(string id)
        {
            var GerenciarUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var Usuarios = GerenciarUsuario.Users.ToList();
            var Usuario = Usuarios.Find(u => u.Id == id);

            var GerenciadorAcoes = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var Acoes = GerenciadorAcoes.Roles.ToList();
            var AcoesView = new List<RoleView>();

            foreach (var item in Usuario.Roles)
            {
                var Acao = Acoes.Find(a => a.Id == item.RoleId);
                var AcaoView = new RoleView
                {
                    RoleID = Acao.Id,
                    Nome = Acao.Name
                };
                AcoesView.Add(AcaoView);
            }

            var UsuarioView = new UserView
            {
                Email = Usuario.Email,
                Nome = Usuario.UserName,
                UserId = Usuario.Id,
                Roles = AcoesView

            };

            return View(UsuarioView);
        }

        public ActionResult addRole(string UserId)
        {

            ViewBag.Error = TempData["Error"] as string;
            var GerenciarUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var Usuarios = GerenciarUsuario.Users.ToList();
            var Usuario = Usuarios.Find(u => u.Id == UserId);
            var UsuarioView = new UserView
            {
                Email = Usuario.Email,
                Nome = Usuario.UserName,
                UserId = Usuario.Id
            };


            var Permicoes = new app_Usuario().Permições(UserId);
            ViewBag.RoleId = new SelectList(Permicoes, "Id", "Name");

            return View(UsuarioView);
        }

        [HttpPost]
        public ActionResult addRole(string UserId, string Roleid)
        {
            var RoleId = Request["RoleID"];
            if (string.IsNullOrEmpty(RoleId))
            {
                TempData["Error"] = "Você precisa selecionar uma permissão!";
                
                return RedirectToAction("addRole", new { UserId = UserId });
            }

            var GerenciarUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var GerenciarAcoes = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var Permicoes = GerenciarAcoes.Roles.ToList().Find(P => P.Id == RoleId);

            if(! GerenciarUsuario.IsInRole(UserId, Permicoes.Name))
            {
                GerenciarUsuario.AddToRole(UserId, Permicoes.Name);
            }


            return RedirectToAction("Role", new { id = UserId });

        }

        public ActionResult ExcluirRole(string UserId, string RoleId)
        {
            var GerenciarUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var GerenciarAcoes = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var Permicoes = GerenciarAcoes.Roles.ToList().Find(P => P.Id == RoleId);

            GerenciarUsuario.RemoveFromRole(UserId, Permicoes.Name);

            return RedirectToAction("Role", new { id = UserId });
                
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}