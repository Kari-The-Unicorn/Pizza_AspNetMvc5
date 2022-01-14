using System.Web;
using System.Web.Mvc;

namespace Pizza_AspNetMvc5.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
