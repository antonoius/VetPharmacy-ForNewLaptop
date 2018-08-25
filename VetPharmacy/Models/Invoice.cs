namespace VetPharmacy
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int InvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public double InvoiceTotalMoney { get; set; }
       
        public int InvoiceDiscount { set; get; }

        public int InvoiceType_id { get; set; }
        public int Shift_id { set; get; }

        public virtual InvoiceType InvoiceType { get; set; }
        public virtual Shift shift { set; get; }
        public int InvoiceItemNumber { set; get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
