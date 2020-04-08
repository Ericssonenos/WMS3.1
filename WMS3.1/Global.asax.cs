using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WMS3._1.Models.Sistema;

namespace WMS3._1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ApplicationDbContext db = new ApplicationDbContext();
            // Cricar os Roles (grupos de que define onde os usuários pedem entrar)
            CriarRoles(db);
            // Criar ADM
            ADM(db);
            // add roles ao usuario ADM
            AddPerADM(db);
            db.Dispose();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPerADM(ApplicationDbContext db)
        {
            var GerenciarUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var Usuario = GerenciarUsuario.FindByName("ericsson.eno@gmail.com");

            var GerenciarAcoes = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

           

            if (!GerenciarUsuario.IsInRole(Usuario.Id, "Usuarios"))
            {
                GerenciarUsuario.AddToRole(Usuario.Id, "Usuarios");
            }
        }

        private void ADM(ApplicationDbContext db)
        {
            var GerenciarUsuario = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var usuario = GerenciarUsuario.FindByName("ericsson.eno@gmail.com");

            if(usuario == null)
            {
                usuario = new ApplicationUser
                {
                    UserName = "ericsson.eno@gmail.com",
                    Email = "ericsson.eno@gmail.com",
                };
                GerenciarUsuario.Create(usuario, "ADM@123adm");
            }
        } 

        private void CriarRoles(ApplicationDbContext db)
        {
            var GerenciarAcoes = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!GerenciarAcoes.RoleExists("UpLoad"))
            {
                GerenciarAcoes.Create(new IdentityRole("UpLoad"));
            }
            if (!GerenciarAcoes.RoleExists("Pendente_de_Entrga"))
            {
                GerenciarAcoes.Create(new IdentityRole("Pendente_de_Entrga"));
            }
            if (!GerenciarAcoes.RoleExists("Entrada"))
            {
                GerenciarAcoes.Create(new IdentityRole("Entrada"));
            }

            if (!GerenciarAcoes.RoleExists("Doca"))
            {
                GerenciarAcoes.Create(new IdentityRole("Doca"));
            }

            if (!GerenciarAcoes.RoleExists("Armazem"))
            {
                GerenciarAcoes.Create(new IdentityRole("Armazem"));
            }

            if (!GerenciarAcoes.RoleExists("Cadastros"))
            {
                GerenciarAcoes.Create(new IdentityRole("Cadastros"));
            }
            if (!GerenciarAcoes.RoleExists("Alterar"))
            {
                GerenciarAcoes.Create(new IdentityRole("Alterar"));
            }
            if (!GerenciarAcoes.RoleExists("Cadastrar"))
            {
                GerenciarAcoes.Create(new IdentityRole("Cadastrar"));
            }

            if (!GerenciarAcoes.RoleExists("BI"))
            {
                GerenciarAcoes.Create(new IdentityRole("BI"));
            }

            if (!GerenciarAcoes.RoleExists("Usuarios"))
            {
                GerenciarAcoes.Create(new IdentityRole("Usuarios"));
            }
            if (!GerenciarAcoes.RoleExists("PreVenda"))
            {
                GerenciarAcoes.Create(new IdentityRole("PreVenda"));
            }

        }
    }
}
