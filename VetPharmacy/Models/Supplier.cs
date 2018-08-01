using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VetPharmacy.Models
{
    public class Supplier
    {
        public int SupplierId { set; get; }
        public string SupplierName { set; get; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { set; get; }
        public Supplier()
        {
            Orders = new HashSet<Order>();
        }
    }
}