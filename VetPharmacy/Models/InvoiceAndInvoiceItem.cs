using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VetPharmacy.Models
{
    public class InvoiceAndInvoiceItem
    {
        public Invoice invoice { set; get; }
        public InvoiceItem invoice_item { set; get; }
        public Medicine medicine { set; get; }
        public InvoiceAndInvoiceItem()
        {
            invoice = new Invoice();
            invoice_item = new InvoiceItem();
            medicine = new Medicine();
        }
    }
}