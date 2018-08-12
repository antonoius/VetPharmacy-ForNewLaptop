using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VetPharmacy.Models
{
    public class Shift
    {
        [Key]
        public int ShiftId { set; get; }
        public int User_id { set; get; }
        public virtual UserMe user { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public ICollection<Invoice> invoices { set; get; }
        public double TotalMoney { set; get; }
    }
}