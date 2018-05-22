using System.Web.Mvc;

namespace ChattingApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
    }
}