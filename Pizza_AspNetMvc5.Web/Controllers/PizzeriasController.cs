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
            // Used DataAnnotations insted in Pizzeria.cs e.g. [Required] and [RegularExpression(@"([A-Z]*[a-z])")]
            // if (string.IsNullOrEmpty(pizzeria.Name))
            // {
            //     ModelState.AddModelError(nameof(pizzeria.Name), "Name is required");
            // }

            // if (string.IsNullOrEmpty(pizzeria.Location))
            // {
            //     ModelState.AddModelError(nameof(pizzeria.Location), "Location is required");
            // }

            if (ModelState.IsValid)
            {
                db.Add(pizzeria);
                // return RedirectToAction("Index");
                return RedirectToAction("Details", new { id = pizzeria.Id});
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.GetDetails(id);

            if (model == null)
            {
                // return HttpNotFound();
                return View("Empty");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pizzeria pizzeria)
        {
            if (ModelState.IsValid)
            {
                db.Update(pizzeria);
                return RedirectToAction("Details", new { id = pizzeria.Id });
            }
            return View(pizzeria);
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Pizzeria pizzeria)
        {
            db.RemovePizzeria(pizzeria);
            return View(pizzeria);
        }
    }
}