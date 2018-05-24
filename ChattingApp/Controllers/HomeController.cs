using System.Web.Mvc;

namespace ChattingApp.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index() => View();
    }
}