using Pizza_AspNetMvc5.Data.Services;
using System.Web.Mvc;

namespace Pizza_AspNetMvc5.Web.Controllers
{
    public class HomeController : Controller
    {
        IPizzeriaData db;

        public HomeController(IPizzeriaData db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

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