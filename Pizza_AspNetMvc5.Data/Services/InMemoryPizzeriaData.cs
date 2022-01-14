using Pizza_AspNetMvc5.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pizza_AspNetMvc5.Data
{
    public class InMemoryPizzeriaData : IPizzeriaData
    {
        List<Pizzeria> pizzerias;

        public InMemoryPizzeriaData()
        {
            pizzerias = new List<Pizzeria>()
            {
                new Pizzeria {Id = 1, Name = "Pap Johns", Location = "London"},
                new Pizzeria {Id = 2, Name = "Domin Pizza", Location = "Bahamas"},
                new Pizzeria {Id = 3, Name = "Margerit Pizzeria", Location = "Tenerife"}
            };
        }

        public void Add(Pizzeria pizzeria)
        {
            pizzerias.Add(pizzeria);
            pizzeria.Id = pizzerias.Count + 1;
        }

        public IEnumerable<Pizzeria> GetAll()
        {
            return pizzerias.OrderBy(prop => prop.Name);
        }

        public Pizzeria GetDetails(int id)
        {
            return pizzerias.FirstOrDefault(p => p.Id == id);
        }
    }
}
