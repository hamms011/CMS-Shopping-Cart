using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TheCakeShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //For Index or Home
            routes.MapRoute("Default", "", new { controller = "Pages", action = "Index" }, new[] { "TheCakeShop.Controllers" });

            //For Pages
            routes.MapRoute("Pages", "{page}", new { controller = "Pages", action = "Index" }, new[] { "TheCakeShop.Controllers" });

            //For PagesMenu
            routes.MapRoute("PagesMenuPartial", "Pages/PagesMenuPartial", new { controller = "Pages", action = "PagesMenuPartial" }, new[] { "TheCakeShop.Controllers" });

            //For SideBar
            routes.MapRoute("SidebarPartial", "Pages/SidebarPartial", new { controller = "Pages", action = "SidebarPartial" }, new[] { "TheCakeShop.Controllers" });

            //For CategoryMenu 
            routes.MapRoute("Shop", "Shop/{action}/{name}", new { controller = "Shop", action = "Index", name = UrlParameter.Optional }, new[] { "TheCakeShop.Controllers" });

            //For Cart
            routes.MapRoute("Cart", "Cart/{action}/{id}", new { controller = "Cart", action = "Index", id = UrlParameter.Optional }, new[] { "TheCakeShop.Controllers" });

            //For Account
            routes.MapRoute("Account", "Account/{action}/{id}", new { controller = "Account", action = "Index", id = UrlParameter.Optional }, new[] { "TheCakeShop.Controllers" });



            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
