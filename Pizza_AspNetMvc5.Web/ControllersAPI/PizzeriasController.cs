using System.Collections.Generic;
using System.Web.Http;
using Pizza_AspNetMvc5.Data;
using Pizza_AspNetMvc5.Data.Services;

namespace Pizza_AspNetMvc5.Web
{
    public class PizzeriasController : ApiController
    {
        private readonly IPizzeriaData db;

        public PizzeriasController(IPizzeriaData db)
        {
            this.db = db;
        }

        // https://localhost:.../api/pizzerias to see XML in browser
        public IEnumerable<Pizzeria> Get()
        {
            var model = db.GetAll();
            return model;
        }
    }
}
