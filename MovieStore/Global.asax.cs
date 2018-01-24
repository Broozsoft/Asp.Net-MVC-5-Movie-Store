using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MovieStore.Controllers;

namespace MovieStore
{
    public class MvcApplication : System.Web.HttpApplication
    {


        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

    }
        void Session_Start(object sender, EventArgs e)
        {
            // your code here, it will be executed upon session start

            ApplicationDbContext db = new ApplicationDbContext();
            if (System.Web.HttpContext.Current.Session["OrderId"] == null)
            {
                Order order = new Order();
                order.OrderDate = DateTime.Now;
                order.CustomerId = null;
                db.Orders.Add(order);
                db.SaveChanges();
                int orderId = order.Id;
                System.Web.HttpContext.Current.Session["OrderId"] = orderId;
            }
        }
    }
}
