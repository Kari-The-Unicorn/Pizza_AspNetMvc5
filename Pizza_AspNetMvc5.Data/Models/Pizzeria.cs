using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_AspNetMvc5.Data
{
    public class Pizzeria
    {
        public int Id { get; set; }
        //[Required]
        //[RegularExpression(@"([A-Z]*[a-z])")]
        public string Name { get; set; }
        //[Required]
        public string Location { get; set; }

        public PizzaType Type { get; set; }
    }
}
