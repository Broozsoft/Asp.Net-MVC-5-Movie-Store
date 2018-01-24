using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieStore.Models.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}