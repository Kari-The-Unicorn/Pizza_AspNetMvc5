using System.ComponentModel.DataAnnotations;

namespace Pizza_AspNetMvc5.Data
{
	public class Pizzeria
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Location { get; set; }

		public PizzaType Type { get; set; }
	}
}
