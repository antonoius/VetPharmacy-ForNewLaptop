using AutoMapper;
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
    public class MedicineData
    {
        public Medicine medicine { set; get;}
        public List<Shipment>shipments { set; get; }
        public MedicineData()
        {
            medicine = new Medicine();
            shipments = new List<Shipment>();
        }
        
    }
    public class Shipment2
    {
        public int ShipmentId { get; set; }
        public int ShipmentAmount { get; set; }
        public double OriginalPrice { get; set; }
        public double ShipmentRemainderAmount { get; set; }
        public double? WholesalePrice { set; get; }
        public DateTime OrderDate { set; get; }
    }
    public class Medicine2
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public bool IsWholeSale { set; get; }
        public int Unit_Id { get; set; }
        public int MedicineCapacity { get; set; }
        public string Comment { get; set; }
    }

    public class MedicinesController : Controller
    {
        private VetPharmaDB db = new VetPharmaDB();

        // GET: Medicines
        public ActionResult Index()
        {
            var medicines = db.Medicines.Include(m => m.Unit);
            return View(medicines.ToList());
        }

        // GET: Medicines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // GET: Medicines/Create
        public ActionResult AddMedicine()
        {
            ViewBag.Unit_Id = new SelectList(db.Units, "UnitId", "UnitName");
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.Unit_Id = new SelectList(db.Units, "UnitId", "UnitName");

            return View();
        }

        // POST: Medicines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Medicines.Add(medicine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Unit_Id = new SelectList(db.Units, "UnitId", "UnitName", medicine.Unit_Id);
            return View(medicine);
        }

        // GET: Medicines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            ViewBag.Unit_Id = new SelectList(db.Units, "UnitId", "UnitName", medicine.Unit_Id);
            return View(medicine);
        }

        // POST: Medicines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Unit_Id = new SelectList(db.Units, "UnitId", "UnitName", medicine.Unit_Id);
            return View(medicine);
        }

        // GET: Medicines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            db.Medicines.Remove(medicine);
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
        public ActionResult GetSaleMethodNames(string Name)
        {
            return Json(db.SaleMethods.Where(x => x.SaleMethodId > 0).Select(x => x.unit.UnitName).ToList());
        }
        [HttpGet]
        public ActionResult GetUnitNames(string any)
        {
            var a = (from r in db.Units
                     where r.UnitId > 0
                     select new { r.UnitId, r.UnitName });
            return Json(a,JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditMedicine ()
        {
            return View();  
        }
        public ActionResult GetMedicineDate(string medicine_name)
        {
            Medicine m = db.Medicines.Where(x => x.MedicineName == medicine_name).FirstOrDefault();
            MedicineData data = new MedicineData();
            data.medicine = m;
            data.medicine.Shipments = null;
            //data.medicine.Unit = null;
            List<Medicine2> md2 = new List<Medicine2>();
            List<Shipment> shipments = new List<Shipment>();
            List<Shipment2> shipments2 = new List<Shipment2>();
            shipments = db.Shipments.Where(x => x.ShipmentMedicine_id == m.MedicineId).ToList();

            //int q =  db.Shipments.Where(x => x.ShipmentMedicine_id == m.MedicineId).ToList().Count;
            for (int x=0;x<shipments.Count;x++)
            {
                Shipment2 sh = new Shipment2();
                sh.ShipmentAmount = shipments[x].ShipmentAmount;
                sh.OriginalPrice = shipments[x].OriginalPrice;
                sh.ShipmentRemainderAmount = shipments[x].ShipmentRemainderAmount;
                sh.WholesalePrice = shipments[x].WholesalePrice;
                sh.ShipmentId = shipments[x].ShipmentId;
                sh.OrderDate = shipments[x].Order.OrderDate;
                shipments2.Add(sh);

            }


            return Json(shipments2, JsonRequestBehavior.AllowGet);
        }
    }
}
