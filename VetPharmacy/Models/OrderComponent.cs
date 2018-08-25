using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VetPharmacy.Models
{
    public class OrderComponent
    {
        public Order order { set; get; }
        public Shipment shipment { set; get; }
        public Medicine medicine { set; get; }
        public Supplier supplier { set; get; }
        public UserMe userme { set; get; }
        public SaleMethod salemethod { set; get; }
        public Invoice invoices { set; get; }
        public InvoiceItem invoice_items { set; get; }
        public Shift shift { set; get; }
        public OrderComponent()
        {
            supplier = new Supplier();
            order = new Order();
            shipment = new Shipment();
            medicine = new Medicine();
            salemethod = new SaleMethod();
            invoices = new Invoice();
            invoice_items = new InvoiceItem();
            shift = new Shift();
        }
    }
}