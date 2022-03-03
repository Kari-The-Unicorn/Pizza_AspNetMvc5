using Pizza_AspNetMvc5.Data;
using Pizza_AspNetMvc5.Data.Services;
using System.Web.Mvc;

namespace Pizza_AspNetMvc5.Web.Controllers
{
    public class PizzeriasController : Controller
    {
        private readonly IPizzeriaData db;

        public PizzeriasController(IPizzeriaData db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var model = db.GetDetails(id);
            if (model == null) return View("Empty");
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pizzeria pizzeria)
        {
            if (ModelState.IsValid)
            {
                db.Add(pizzeria);
                return View();
            }
            return View();
        }
    }
}