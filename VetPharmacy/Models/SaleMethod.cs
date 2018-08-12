using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VetPharmacy.Models
{
    public class SaleMethod
    {
        [Key]
        public int SaleMethodId { set; get; }
        public int Medicine_id { set; get; }
        public virtual Unit unit { set; get; }
        public int Unit_id { set; get; }
        public double Capacity { set; get; }
        public virtual Medicine medicine { set; get; }

    }
}