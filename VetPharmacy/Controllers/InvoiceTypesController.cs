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
    public class InvoiceTypesController : Controller
    {
        private VetPharmaDB db = new VetPharmaDB();

        // GET: InvoiceTypes
        public ActionResult Index()
        {
            return View(db.InvoiceTypes.ToList());
        }

        // GET: InvoiceTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceType invoiceType = db.InvoiceTypes.Find(id);
            if (invoiceType == null)
            {
                return HttpNotFound();
            }
            return View(invoiceType);
        }

        // GET: InvoiceTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InvoiceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceTypeId,InvoiceTypeName")] InvoiceType invoiceType)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceTypes.Add(invoiceType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoiceType);
        }

        // GET: InvoiceTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceType invoiceType = db.InvoiceTypes.Find(id);
            if (invoiceType == null)
            {
                return HttpNotFound();
            }
            return View(invoiceType);
        }

        // POST: InvoiceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceTypeId,InvoiceTypeName")] InvoiceType invoiceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoiceType);
        }

        // GET: InvoiceTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceType invoiceType = db.InvoiceTypes.Find(id);
            if (invoiceType == null)
            {
                return HttpNotFound();
            }
            return View(invoiceType);
        }

        // POST: InvoiceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceType invoiceType = db.InvoiceTypes.Find(id);
            db.InvoiceTypes.Remove(invoiceType);
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
