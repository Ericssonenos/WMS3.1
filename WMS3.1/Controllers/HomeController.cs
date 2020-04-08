using System.Web.Mvc;

namespace WMS3._1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Expedição")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}