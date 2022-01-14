using System.Web.Mvc;

namespace Pizza_AspNetMvc5.Web
{
    public class NewController : Controller
    {
        // GET: New
        public ActionResult Index(string name)
        {
            var model = new NewViewModel();
            model.Name = name ?? "empty";
            return View(model);
        }
    }
}