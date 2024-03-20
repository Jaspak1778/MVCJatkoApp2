using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCJatkoApp2.Models;

namespace MVCJatkoApp2.Controllers
{
    public class Order_DetailsController : Controller
    {
        private NorthwindEntity db = new NorthwindEntity();

        // GET: Order_Details
        public ActionResult Index()
        {
            var order_Details = db.Order_Details.Include(o => o.Orders).Include(o => o.Products);
            return View(order_Details.ToList());
        }

        // GET: Order_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details order_Details = db.Order_Details.Find(id);
            if (order_Details == null)
            {
                return HttpNotFound();
            }
            return View(order_Details);
        }

        // GET: Order_Details/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            return View();
        }

        // POST: Order_Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,ProductID,UnitPrice,Quantity,Discount")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                db.Order_Details.Add(order_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        public ActionResult Edit(int? OrderID, int? ProductID)
        {
            if (OrderID == null || ProductID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order_Details order_Details = db.Order_Details.Find(OrderID, ProductID);
            if (order_Details == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }


        // POST: Order_Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,ProductID,UnitPrice,Quantity,Discount")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        // GET: Order_Details/Delete/5
        public ActionResult Delete(int? OrderID, int? ProductID)
        {
            if (OrderID == null || ProductID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order_Details order_Details = db.Order_Details.Find(OrderID, ProductID);
            if (order_Details == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        // POST: Order_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Details order_Details = db.Order_Details.Find(id);
            db.Order_Details.Remove(order_Details);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region omat ACT
        // GET: Order_Details

        public ActionResult OneView()
        {
            var order_Details = db.Order_Details.Include(o => o.Orders).Include(o => o.Products);
            return View(order_Details.ToList());
        }


        // POST: Order_Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OneEdit([Bind(Include = "OrderID,ProductID,UnitPrice,Quantity,Discount")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View("OneView");
        }


        #endregion



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
