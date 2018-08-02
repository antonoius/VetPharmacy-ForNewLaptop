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
        public int Shipment_id { set; get; }
        public int Unit_id { set; get; }
        public double TradPrice { set; get; }
        public double OriginalPrice { get; set; }
        public double Capacity { set; get; }

    }
}