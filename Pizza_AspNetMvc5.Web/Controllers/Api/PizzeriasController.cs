using Pizza_AspNetMvc5.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace Pizza_AspNetMvc5.Web.Controllers.Api
{
    public class PizzeriasController : ApiController
    {
        private readonly IPizzeriaData db;
        public PizzeriasController(IPizzeriaData db)
        {
            this.db = db;
        }

        public IEnumerable<Pizzeria> Get()
        {
            var model = db.GetAll();
            return model;
        }
    }
}
