using System.ComponentModel.DataAnnotations;

namespace Pizza_AspNetMvc5.Data
{
    public class Pizzeria
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"([A-Z]*[a-z])")]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
