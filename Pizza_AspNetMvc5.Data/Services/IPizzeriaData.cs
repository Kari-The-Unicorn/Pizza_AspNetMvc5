using System.Collections.Generic;

namespace Pizza_AspNetMvc5.Data.Services
{
    public interface IPizzeriaData
    {
        IEnumerable<Pizzeria> GetAll();
        Pizzeria GetDetails(int id);
        void Add(Pizzeria pizzeria);
        void Update(Pizzeria pizzeria);
        void RemovePizzeria(Pizzeria pizzeria);
    }
}
