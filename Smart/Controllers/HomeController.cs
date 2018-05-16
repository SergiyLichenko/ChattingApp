using System.Web.Mvc;

namespace ChattingApp.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //List<ChatViewModel> allChats = chats.GetAllChats(); 
            return View();
        }
    }
}