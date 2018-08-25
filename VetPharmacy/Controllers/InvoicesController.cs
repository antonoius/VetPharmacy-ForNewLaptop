using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VetPharmacy;
using VetPharmacy.Models;
namespace VetPharmacy.Controllers
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class InvoicesController : Controller
    {

        private VetPharmaDB db = new VetPharmaDB();

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.InvoiceType);
            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceType_id = new SelectList(db.InvoiceTypes, "InvoiceTypeId", "InvoiceTypeName");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,InvoiceDate,InvoiceTotalMoney,InvoiceType_id")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceType_id = new SelectList(db.InvoiceTypes, "InvoiceTypeId", "InvoiceTypeName", invoice.InvoiceType_id);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceType_id = new SelectList(db.InvoiceTypes, "InvoiceTypeId", "InvoiceTypeName", invoice.InvoiceType_id);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,InvoiceDate,InvoiceTotalMoney,InvoiceType_id")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceType_id = new SelectList(db.InvoiceTypes, "InvoiceTypeId", "InvoiceTypeName", invoice.InvoiceType_id);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult AddItem()
        {
            InvoiceAndInvoiceItem an = new InvoiceAndInvoiceItem();
            return PartialView("AddInvoiceItemDynamic",an);
        }
        [HttpPost]
        public ActionResult Save()
        {
            List<InvoiceItem> items = new List<InvoiceItem>();
            InvoiceItem it = new InvoiceItem();

            UpdateModel(it);
            string x = it.Quantity.ToString();
            db.InvoiceItems.Add(items[0]);
            db.InvoiceItems.Add(items[1]);
            db.SaveChanges();
            return View("Index","InvoiceItems",null);
        }
        [HttpPost]
        public ActionResult SaveJson(List<InvoiceItem> ItemList)
        {

            //if(ItemList.Count==0)
            //{
            //    return View("Create", "Invoice");
            //}
            foreach(InvoiceItem it in ItemList)
            {
                InvoiceItem t = new InvoiceItem();
                t = it;
            db.InvoiceItems.Add(t);

            }

            db.SaveChanges();

            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddInvoice()
        {
            ViewBag.te = db.Units.ToList();
            return View();
        }
        [HttpGet]
        public double  GetShipmentDate(string value)
        {

            
            int s = Convert.ToInt32(value);
            double r=   db.Shipments.Where(x => x.ShipmentId == s).FirstOrDefault().TradPrice;
            //int x;
            //x = Convert.ToInt32(Shipment_id);
            //x = Int16.Parse(Shipment_id);
            return r;
        }
        [HttpGet]
        public int GetUint(string value)
        {
          //  int   s = Convert.ToInt32(value);
            return db.Medicines.Where(x => x.MedicineName == value).FirstOrDefault().MedicineCapacity;
        }
        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            List<Medicine> MyList = new List<Medicine>();
            MyList = db.Medicines.Where(m => m.MedicineId > 0).ToList();
            var ReturnList = (from N in MyList
                              where N.MedicineName.StartsWith(Prefix)
                              select new { N.MedicineName });
     
                return Json(ReturnList, JsonRequestBehavior.AllowGet);
 

        }


    
        public ActionResult test()
        {
            return View();
        }
        public ActionResult GetMedicineName(string term)
        {
            return Json(db.Medicines.Where(c => c.MedicineName.StartsWith(term)).Select(a => new { label = a.MedicineName }), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public int GetMedicineId(string value)
        {
           return   db.Medicines.Where(x => x.MedicineName == value).FirstOrDefault().MedicineId;
            
        }
        public JsonResult GetMedicineShipments (int data)
        {
            var ab = db.Shipments.Where((x => x.ShipmentMedicine_id == data)).Where(q=>q.ShipmentRemainderAmount>0).ToList();
            //        var list = JsonConvert.SerializeObject(ab,
            //               Formatting.None,
            //          new JsonSerializerSettings()
            //{
            //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //});
            var a = (from N in db.Shipments
                     where N.ShipmentMedicine_id == data
                     where N.ShipmentRemainderAmount>0
                     orderby N.ShipmentRemainderAmount descending
                     select new { N.ShipmentId ,N.ShipmentRemainderAmount,N.TradPrice});
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public int SubmetInvoice(Invoice InvoiceToSubmet)
        {
            int shift_id = Int16.Parse(Session["ShiftId"].ToString());
            DateTime temp_date = InvoiceToSubmet.InvoiceDate;
            InvoiceToSubmet.Shift_id = Int16.Parse(Session["ShiftId"].ToString());
            db.Invoices.Add(InvoiceToSubmet);
            db.Shifts.Where(x => x.ShiftId == shift_id).FirstOrDefault().TotalMoney += InvoiceToSubmet.InvoiceTotalMoney;
            db.Shifts.Where(x => x.ShiftId == shift_id).FirstOrDefault().InvoiceNumber += 1;
            DateTime date = DateTime.Now;
            date = date.AddTicks(-(date.Ticks % TimeSpan.TicksPerSecond));
            db.Shifts.Where(x => x.ShiftId == shift_id).FirstOrDefault().EndDate = date;


            db.SaveChanges();
            int q = 0;
            q = db.Invoices.OrderByDescending(u => u.InvoiceId).FirstOrDefault().InvoiceId;
      //      db.Invoices.Where(x => x.InvoiceId == q).FirstOrDefault().InvoiceTotalMoney = 999;
           
    //        db.SaveChanges();
            return q;
        }
        [HttpPost]
        public bool SubmetInvoiceItems(List<InvoiceItem> InvoiceItemsToSubmet)
        {
            foreach(InvoiceItem m in InvoiceItemsToSubmet)
            {
                db.InvoiceItems.Add(m);
                db.Shipments.Where(x => x.ShipmentId == m.Item_shipment_id).FirstOrDefault().ShipmentRemainderAmount -= m.Quantity;
            }
            db.SaveChanges();
            return true;
        }
    }
}
