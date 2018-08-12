namespace VetPharmacy
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Medicine")]
    public partial class Medicine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medicine()
        {
            Shipments = new HashSet<Shipment>();
        }

        public int MedicineId { get; set; }
        [Required]
        [StringLength(50)]
        public string MedicineName { get; set; }
        public bool IsWholeSale { set; get; }
        public int Unit_Id { get; set; }
        public int MedicineCapacity { get; set; }
        [StringLength(100)]
        public string Comment { get; set; }
        public virtual Unit Unit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipment> Shipments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleMethod> SaleMethods { set; get; }
        
    }
}
