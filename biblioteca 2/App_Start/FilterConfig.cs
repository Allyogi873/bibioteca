using System.Web;
using System.Web.Mvc;
using biblioteca_2.Filters;

namespace biblioteca_2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new FiltroAutorizador());
        }
    }
}
