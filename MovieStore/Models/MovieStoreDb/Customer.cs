using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string BillingAddress { get; set; }
        public string BillingZip { get; set; }
        public string BillingCity { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}