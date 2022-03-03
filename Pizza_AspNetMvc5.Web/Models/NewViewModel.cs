using Pizza_AspNetMvc5.Data;
using System.Collections.Generic;

namespace Pizza_AspNetMvc5.Web.Models
{
    public class NewViewModel
    {
        public IEnumerable<Pizzeria> Pizzerias { get; set; }
        public string Name { get; set; }
    }
}