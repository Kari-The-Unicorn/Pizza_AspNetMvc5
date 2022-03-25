using System.Collections.Generic;
using System.Linq;

namespace Pizza_AspNetMvc5.Data.Services
{
    public class InMemoryPizzeriaData : IPizzeriaData
    {
        List<Pizzeria> pizzerias;

        public InMemoryPizzeriaData()
        {
            pizzerias = new List<Pizzeria>()
            {
                new Pizzeria {Id = 1, Name = "Pap Johns", Location = "London", Type = PizzaType.American},
                new Pizzeria {Id = 2, Name = "Domin Pizza", Location = "Bahamas", Type= PizzaType.American},
                new Pizzeria {Id = 3, Name = "Margerit Pizzeria", Location = "Tenerife", Type= PizzaType.Turkish}
            };
        }

        public void Add(Pizzeria pizzeria)
        {
            pizzerias.Add(pizzeria);
            pizzeria.Id = pizzerias.Count + 1;
        }

        public void Update(Pizzeria pizzeria)
        {
            var current = GetDetails(pizzeria.Id);
            if (current != null)
            {
                current.Name = pizzeria.Name;
                current.Location = pizzeria.Location;
                current.Type = pizzeria.Type;
            }
        }

        public void RemovePizzeria(Pizzeria pizzeria)
        {
            pizzerias.Remove(pizzeria);
            pizzeria.Id = pizzerias.Count - 1;
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
