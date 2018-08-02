namespace VetPharmacy
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Shipment")]
    public partial class Shipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shipment()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int ShipmentId { get; set; }

        public int ShipmentAmount { get; set; }

        public double OriginalPrice { get; set; }

        public double TradPrice { get; set; }

        public int Order_id { get; set; }

        public double ShipmentRemainderAmount { get; set; }

        public int? ShipmentMedicine_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }

        public virtual Medicine Medicine { get; set; }

        public virtual Order Order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleMethod> SaleMethods { set; get; }
    }
}
