namespace VetPharmacy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvoiceItem")]
    public partial class InvoiceItem
    {
        public int InvoiceItemId { get; set; }

        public int Item_shipment_id { get; set; }

        public double Quantity { get; set; }

        public int Invoice_id { get; set; }
        
        public double ITemCost { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Shipment Shipment { get; set; }
    }
}
