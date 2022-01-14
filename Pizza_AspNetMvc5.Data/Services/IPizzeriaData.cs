using Pizza_AspNetMvc5.Data.Models;
using System.Collections.Generic;

namespace Pizza_AspNetMvc5.Data
{
    public interface IPizzeriaData
    {
        IEnumerable<Pizzeria> GetAll();
        Pizzeria GetDetails(int id);
        void Add(Pizzeria pizzeria);
    }
}
