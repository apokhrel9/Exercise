using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnockoutMVC;
using System.Web.Script.Serialization;

namespace KnockoutMVC.Controllers
{
    public class CustomerController : Controller
    {
        private KnockoutMVCEntities db = new KnockoutMVCEntities();

        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        /// <summary>
        /// Fetches all Customers and return the jason format.
        /// </summary>
        /// <returns></returns>
        public JsonResult FetchCustomers()
        {
            return Json(db.Customers.ToList(), JsonRequestBehavior.AllowGet);
        }
     
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public string Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (!CustomerExists(customer))
                    db.Customers.Add(customer);
                else
                    return Edit(customer);
                db.SaveChanges();
                return "Customer Created";
            }
            return "Creation Failed";
        }

        public ActionResult Edit(string id = null)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return null;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.InitialData = serializer.Serialize(customer);
            return View();
        }

        /// <summary>
        /// Edits particular customer details
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public string Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return " Customer Edited";
            }
            return " Edit Failed";
        }

        public ActionResult Delete(string id = null)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return null;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.InitialData = serializer.Serialize(customer);
            return View();
        }

        /// <summary>
        /// Delete particular customer details
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public string Delete(Customer customer)
        {
            Customer customerDetail = db.Customers.Find(customer.CustomerId);
            db.Customers.Remove(customerDetail);
            db.SaveChanges();
            return " Customer Deleted";
        }


        /// <summary>
        /// Checks if customer exists
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        private bool CustomerExists(Customer customer)
        {
            Customer foundCustomer = db.Customers.Find(customer.CustomerId);
            if (foundCustomer != null && !String.IsNullOrEmpty(foundCustomer.CustomerId))
                return true;
            return false;
        }

        /// <summary>
        /// Dispose dbContext
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
