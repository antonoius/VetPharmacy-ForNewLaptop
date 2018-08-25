using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VetPharmacy.Models;
using System.Data.Entity;

namespace VetPharmacy.Controllers
{
    public class InvoiceDemo
    {
        public int InvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public double InvoiceTotalMoney { get; set; }

        public int InvoiceDiscount { set; get; }

        public int InvoiceType_id { get; set; }
        
        public int InvoiceItemNumber { set; get; }

    }
    public class InvoiceItemDemo
    {
   
        public string MedicineName { set; get; }


        public double Quantity { get; set; }

        public int Invoice_id { get; set; }

        public double ITemCost { get; set; }
    }
    public class Invoice_data
    {
        public InvoiceDemo invoice_demo { set; get; }
        public List<InvoiceItemDemo> items_demo { set; get; }
        public Invoice_data()
        {
            invoice_demo = new InvoiceDemo();
            items_demo = new List<InvoiceItemDemo>();
        }
    }
    public class ShiftsController : Controller
    {
        // GET: Shifts
        VetPharmaDB db = new VetPharmaDB();
        [verify]
        public ActionResult ShiftPage()
        {
            return View();
        }
        public ActionResult test()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OpenShift()
        {
           
            Shift shift = new Shift();
            shift.User_id = Int16.Parse(Session["UserId"].ToString());
            DateTime date = DateTime.Now;
            date = date.AddTicks(-(date.Ticks % TimeSpan.TicksPerSecond));
            shift.StartDate = date;
            shift.EndDate = null;
            shift.InvoiceNumber = 0;
            db.Shifts.Add(shift);
            db.SaveChanges();
            int a = db.Shifts.OrderByDescending(x => x.ShiftId).FirstOrDefault().ShiftId;
            Session["ShiftId"] = a;            
            Session["ShiftStartDate"] = date;
            return Json(null, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        public ActionResult CloseShift()
        {
            int x = int.Parse(Session["ShiftId"].ToString());
            Shift shift = db.Shifts.Where(q => q.ShiftId == x).FirstOrDefault();
            DateTime date = DateTime.Now;
            date = date.AddTicks(-(date.Ticks % TimeSpan.TicksPerSecond));
            shift.EndDate = date;
            db.Shifts.Where(q => q.ShiftId == x).FirstOrDefault().EndDate = date;
           // db.Entry(shift).State = EntityState.Modified;
            db.SaveChanges();
            Session["ShiftId"] = null;
            Session["ShiftStartDate"] = null;
            return Json(db.Shifts.Where(q => q.ShiftId == x).FirstOrDefault().EndDate, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetInvoicesData()
        {
          
            List<Invoice_data> data_to_return = new List<Invoice_data>();

            var a = db.Invoices.Where(x => x.InvoiceId > 0);
            foreach(Invoice item in a)
            {
                Invoice_data data = new Invoice_data();
                InvoiceDemo invo_demo = new InvoiceDemo();
                List< InvoiceItemDemo> item_demo = new List< InvoiceItemDemo>();
                invo_demo.InvoiceDate = item.InvoiceDate;
                invo_demo.InvoiceDiscount = item.InvoiceDiscount;
                invo_demo.InvoiceId = item.InvoiceId;
                invo_demo.InvoiceItemNumber = item.InvoiceItemNumber;
                invo_demo.InvoiceTotalMoney = item.InvoiceTotalMoney;
                invo_demo.InvoiceType_id = item.InvoiceType_id;
                data.invoice_demo = invo_demo;

                var ab = db.InvoiceItems.Where(x => x.Invoice_id == item.InvoiceId).ToList();
                
                for (int z=0;z<ab.Count; z++)
                {
                    InvoiceItemDemo d = new InvoiceItemDemo();
                    d.Invoice_id = ab[z].Invoice_id;
                    d.ITemCost = ab[z].ITemCost;
                    d.Quantity = ab[z].Quantity;
                    int qw = ab[z].Item_shipment_id; 
                    

                    d.MedicineName = db.Shipments.Where(x => x.ShipmentId == qw).FirstOrDefault().Medicine.MedicineName;
                    item_demo.Add(d);
                }
                data.items_demo = item_demo;
                data_to_return.Add(data);

           
                
                
            }
            //var list = JsonConvert.SerializeObject(data_to_return,
            // Formatting.None,
            //new JsonSerializerSettings()
            // {
            //       ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            // });
           
   
           
            return Json(data_to_return,JsonRequestBehavior.AllowGet);


          //  return SerializeJSON(list, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetInvoicesDataByDate()
        {

            List<Invoice_data> data_to_return = new List<Invoice_data>();

            var a = db.Invoices.Where(x => x.InvoiceId > 0);
            foreach (Invoice item in a)
            {
                Invoice_data data = new Invoice_data();
                InvoiceDemo invo_demo = new InvoiceDemo();
                List<InvoiceItemDemo> item_demo = new List<InvoiceItemDemo>();
                invo_demo.InvoiceDate = item.InvoiceDate;
                invo_demo.InvoiceDiscount = item.InvoiceDiscount;
                invo_demo.InvoiceId = item.InvoiceId;
                invo_demo.InvoiceItemNumber = item.InvoiceItemNumber;
                invo_demo.InvoiceTotalMoney = item.InvoiceTotalMoney;
                invo_demo.InvoiceType_id = item.InvoiceType_id;
                data.invoice_demo = invo_demo;

                var ab = db.InvoiceItems.Where(x => x.Invoice_id == item.InvoiceId).ToList();

                for (int z = 0; z < ab.Count; z++)
                {
                    InvoiceItemDemo d = new InvoiceItemDemo();
                    d.Invoice_id = ab[z].Invoice_id;
                    d.ITemCost = ab[z].ITemCost;
                    d.Quantity = ab[z].Quantity;
                    int qw = ab[z].Item_shipment_id;


                    d.MedicineName = db.Shipments.Where(x => x.ShipmentId == qw).FirstOrDefault().Medicine.MedicineName;
                    item_demo.Add(d);
                }
                data.items_demo = item_demo;
                data_to_return.Add(data);




            }
            //var list = JsonConvert.SerializeObject(data_to_return,
            // Formatting.None,
            //new JsonSerializerSettings()
            // {
            //       ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            // });



            return Json(data_to_return, JsonRequestBehavior.AllowGet);


            //  return SerializeJSON(list, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetInvoicesDataByShiftId(string shift_id)
        {
            int qww = int.Parse(shift_id);
            List<Invoice_data> data_to_return = new List<Invoice_data>();

            var a = db.Invoices.Where(x => x.Shift_id == qww);
            foreach (Invoice item in a)
            {
                Invoice_data data = new Invoice_data();
                InvoiceDemo invo_demo = new InvoiceDemo();
                List<InvoiceItemDemo> item_demo = new List<InvoiceItemDemo>();
                invo_demo.InvoiceDate = item.InvoiceDate;
                invo_demo.InvoiceDiscount = item.InvoiceDiscount;
                invo_demo.InvoiceId = item.InvoiceId;
                invo_demo.InvoiceItemNumber = item.InvoiceItemNumber;
                invo_demo.InvoiceTotalMoney = item.InvoiceTotalMoney;
                invo_demo.InvoiceType_id = item.InvoiceType_id;
                data.invoice_demo = invo_demo;

                var ab = db.InvoiceItems.Where(x => x.Invoice_id == item.InvoiceId).ToList();

                for (int z = 0; z < ab.Count; z++)
                {
                    InvoiceItemDemo d = new InvoiceItemDemo();
                    d.Invoice_id = ab[z].Invoice_id;
                    d.ITemCost = ab[z].ITemCost;
                    d.Quantity = ab[z].Quantity;
                    int qw = ab[z].Item_shipment_id;


                    d.MedicineName = db.Shipments.Where(x => x.ShipmentId == qw).FirstOrDefault().Medicine.MedicineName;
                    item_demo.Add(d);
                }
                data.items_demo = item_demo;
                data_to_return.Add(data);




            }
            //var list = JsonConvert.SerializeObject(data_to_return,
            // Formatting.None,
            //new JsonSerializerSettings()
            // {
            //       ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            // });



            return Json(data_to_return, JsonRequestBehavior.AllowGet);


            //  return SerializeJSON(list, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetShiftDate()
        {
            int shift_id= int.Parse(Session["ShiftId"].ToString());
            var shift = db.Shifts.Single(x => x.ShiftId == shift_id).TotalMoney;
            return Json(shift, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetShiftsOfDay(int  day,int year,int month)
        {
            var list_of_shifts= db.Shifts.Where(x => x.StartDate.Year==year).Where(a=>a.StartDate.Month==month).Where(q=>q.StartDate.Day==day).Select(z => z.ShiftId).ToList();
            return Json(list_of_shifts, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetShiftsOfMonth( int year, int month)
        {
            var list_of_shifts = db.Shifts.OrderByDescending(d=>d.InvoiceNumber).Where(x => x.StartDate.Year == year).Where(a => a.StartDate.Month == month).Select(z => z.ShiftId).ToList();
            return Json(list_of_shifts, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetShiftAllData(string shift_id)
        {
            int qww = int.Parse(shift_id);
            var ab = (from a in db.Shifts
                      where a.ShiftId == qww
                      join b in db.UserMes on a.User_id equals b.UserId
                      select new { a.TotalMoney, a.StartDate,a.EndDate, b.UserName }).FirstOrDefault();
            return Json(ab, JsonRequestBehavior.AllowGet);
        }
    }
}