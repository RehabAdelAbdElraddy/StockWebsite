using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Text;
using StockWebsite.Models;
using System.Diagnostics;

namespace StockWebsite.Controllers
{
    public class ItemsController : Controller
    {
        private StockModel db = new StockModel();

        // GET: Items
        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }

        // GET: purchase
        public ActionResult purchase()
        {
            List<SelectListItem> ListSelectListItem = new List<SelectListItem>();
            foreach (Item item in db.Items)
            {
                SelectListItem selectListItem = new SelectListItem()
                {  Text=item.ItemName + " " +"Quantity In the Stock is "+ item.ItemQuantity,
                    Value=item.ItemCode.ToString()
                };
                ListSelectListItem.Add(selectListItem);
            }

            ItemsViewModel itemsViewModel = new ItemsViewModel();
            itemsViewModel.Items = ListSelectListItem;
            itemsViewModel.invoices = db.Invoices.ToList();
            return View(itemsViewModel);
        }

        [HttpPost]
        public ActionResult purchase(IEnumerable<string> SelectedItems, int quantity)
        {
            if(SelectedItems==null)
            {
                return Content("You did not select any value");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You Select: " + "Quantity is: "+ quantity +" - " + string.Join(",", SelectedItems));
                //Create Invoice
                //Add in Invoice_Items Table
                // Increase Count Of selected Items In Items Table
                // again Load List
                List<Item> itemsInInvoice = new List<Item>();
                foreach (var item in SelectedItems)
                {   int intSelectedItems = Int32.Parse(item);
                    
                    Item i = db.Items.Find(intSelectedItems);
                    if (i != null)
                    {
                        Debug.WriteLine("Not Equal Null",i);
                        itemsInInvoice.Add(i);
                        i.ItemQuantity = i.ItemQuantity+quantity;
                        db.SaveChanges();
                    }
                }
                Debug.WriteLine("Debug");
                Debug.WriteLine(itemsInInvoice[0]);
                Invoice invoice = new Invoice() {InvoiceDate=DateTime.Now};
                invoice.Items = itemsInInvoice;
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("purchase");
            }
        }
        public ActionResult EditInvoice(int? id)
        {
            return RedirectToAction("Edit", "Invoices", new { id = id });
           
        }
            public ActionResult DeleteInvoice(int? id)
        {

            return RedirectToAction("Delete", "Invoices", new { id = id });

        }
            // GET: Items/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

    
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemCode,ItemName,ItemPrice,ItemQuantity")] Item item)
        {    
            if (ModelState.IsValid)
            {    if(item.ItemQuantity==null)
                {
                    item.ItemQuantity = 0;
                }
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemCode,ItemName,ItemPrice,ItemQuantity")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
