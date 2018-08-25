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
    public class OrdersController : Controller
    {
        private VetPharmaDB db = new VetPharmaDB();

        // GET: Orders
      
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,OrderDate,OrderTotalMoney")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderDate,OrderTotalMoney")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
       
        public ActionResult AddOrder()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetShipmentData(string medicine_name)
        {
            var num = db.Medicines.Where(x => x.MedicineName == medicine_name).FirstOrDefault();
            if(db.Shipments.Where(x=>x.ShipmentMedicine_id==num.MedicineId).Count()==0)
            {
                Shipment shipment_temp = new Shipment();
                shipment_temp.ShipmentMedicine_id = num.MedicineId;
                shipment_temp.Medicine = num;
                shipment_temp.Medicine.MedicineCapacity = num.MedicineCapacity;
                shipment_temp.TradPrice = 0;
                shipment_temp.OriginalPrice = 0;
                
                return Json(shipment_temp, JsonRequestBehavior.AllowGet);
            }
            var a = (from n in db.Shipments
                     where n.Medicine.MedicineName == medicine_name
                     orderby n.ShipmentId descending
                     select new { n.ShipmentMedicine_id, n.Medicine.MedicineCapacity, n.TradPrice, n.WholesalePrice, n.OriginalPrice ,n.Medicine.IsWholeSale}).FirstOrDefault();
            //double w= db.Medicines.Where(x => x.MedicineName.StartsWith(medicine_name)).FirstOrDefault().MedicineCapacity;
          
                 
            return Json(a,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string SubmetOrder(Order OrderToSubment,List<Shipment> ShipmentsToSubmet)
        {
            
            try
            {
                db.Orders.Add(OrderToSubment);
                db.SaveChanges();
                var a = (from b in db.Orders orderby b.OrderId descending select new { b.OrderId }).FirstOrDefault();
                int order_id = a.OrderId;
                foreach (Shipment shipment in ShipmentsToSubmet)
                {
                    shipment.Order_id = order_id;
                    shipment.ShipmentRemainderAmount = shipment.ShipmentAmount;
                    db.Shipments.Add(shipment);
                }
                db.SaveChanges();
                return "true";
            }
            catch (Exception e)
            {
                return e.ToString();
            }


        }
        [HttpGet]
        public ActionResult GetShipmentNumbers(string name)
        {
            return Json(db.Shipments.Where(x => x.Medicine.MedicineName == name).Count(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetMedicineCapacity(string name)
        {
            return Json(db.Medicines.Where(x => x.MedicineName == name).FirstOrDefault().MedicineCapacity, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetMedicineId(string name)
        {
            return Json(db.Medicines.Where(x => x.MedicineName == name).FirstOrDefault().MedicineId, JsonRequestBehavior.AllowGet);
        }
    }

}
