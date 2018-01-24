using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MovieStore.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseYear { get; set; }
        public int Price { get; set; }
        public string image { get; set; }

        public virtual ICollection<Order_Movies> orders_movies { get; set; }
    }
}