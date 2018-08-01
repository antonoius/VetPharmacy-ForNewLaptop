using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VetPharmacy;

namespace VetPharmacy.Controllers
{
    public class ShipmentsController : Controller
    {
        private VetPharmaDB db = new VetPharmaDB();

        // GET: Shipments
        public ActionResult Index()
        {
            var shipments = db.Shipments.Include(s => s.Medicine).Include(s => s.Order);
            return View(shipments.ToList());
        }

        // GET: Shipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // GET: Shipments/Create
        public ActionResult Create()
        {
            ViewBag.ShipmentMedicine_id = new SelectList(db.Medicines, "MedicineId", "MedicineName");
            ViewBag.Order_id = new SelectList(db.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipmentId,ShipmentAmount,OriginalPrice,TradPrice,Order_id,ShipmentRemainderAmount,ShipmentMedicine_id")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                db.Shipments.Add(shipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShipmentMedicine_id = new SelectList(db.Medicines, "MedicineId", "MedicineName", shipment.ShipmentMedicine_id);
            ViewBag.Order_id = new SelectList(db.Orders, "OrderId", "OrderId", shipment.Order_id);
            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShipmentMedicine_id = new SelectList(db.Medicines, "MedicineId", "MedicineName", shipment.ShipmentMedicine_id);
            ViewBag.Order_id = new SelectList(db.Orders, "OrderId", "OrderId", shipment.Order_id);
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShipmentId,ShipmentAmount,OriginalPrice,TradPrice,Order_id,ShipmentRemainderAmount,ShipmentMedicine_id")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShipmentMedicine_id = new SelectList(db.Medicines, "MedicineId", "MedicineName", shipment.ShipmentMedicine_id);
            ViewBag.Order_id = new SelectList(db.Orders, "OrderId", "OrderId", shipment.Order_id);
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shipment shipment = db.Shipments.Find(id);
            if (shipment == null)
            {
                return HttpNotFound();
            }
            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shipment shipment = db.Shipments.Find(id);
            db.Shipments.Remove(shipment);
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
    }
}
