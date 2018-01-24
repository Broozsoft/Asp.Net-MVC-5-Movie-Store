using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId  { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Order_Movies> orders_movies { get; set; }
        public Boolean status { get; set; }
    }
}