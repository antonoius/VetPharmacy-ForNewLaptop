using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VetPharmacy.Models
{
    public class UserMe
    {
        [Key]
        public int UserId { set; get; }
        public string UserName { set; get; }
        public string UserPassword { set; get; }
        public ICollection<Shift> shift { set; get; }
    }
}