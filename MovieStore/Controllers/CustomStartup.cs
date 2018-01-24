using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.Controllers
{
    public class CustomStartup
    {
       
        public static void CurrentOrderCartId(System.Web.HttpContext context)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (context.Session["OrderId"] == null)
            {

                    Order order = new Order();
                    order.OrderDate = DateTime.Now;
                    order.CustomerId = null;
                    db.Orders.Add(order);
                    db.SaveChanges();
                    int orderId = order.Id;
                    context.Session["OrderId"] = orderId;
            }
           
        }
    }
}