using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;
using System.Data.Entity;

namespace MvcApplication3.Controllers
{

    
    public class AdminController : Controller
    {
        Database1Entities2 de1 = new Database1Entities2();
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(de1.Orders.ToList());
        }
        public ActionResult Add()
        {
            int price = Int32.Parse(Request["price"]) ;
            string category = Request["category"];
            string description = Request["description"];
            string src = Request["source"];
            string src1 = "~/Images/" + src + ".jpg";
            Product p = new Product();
            p.Price = price;
            p.Category = category;
            p.Desc = description;
            p.Src = src1;
            
            de1.Products.Add(p);
            de1.SaveChanges();

            Session["msg"] = " Item Added successfully";
            ViewBag.msg = " Item added successfully";


            return RedirectToAction("Index");
        }

        public ActionResult Update()
        {
            int pid = Int32.Parse(Request["update_productID"]);

          

            int price = Int32.Parse(Request["price"]);
            string category = Request["update_category"];
            string description = Request["update_description"];
            string src = Request["update_source"];
            Product p = de1.Products.Find(pid);
            
            p.Price = price;
            p.Category = category;
            p.Desc = description;
            p.Src = src;
            de1.Entry(p).State = System.Data.EntityState.Modified;
            de1.SaveChanges();
            Session["msg"] = " Item Updated successfully";

            ViewBag.msg = " Item Updated successfully";


            return RedirectToAction("Index");
        }
        public ActionResult Remove()
        {
            int pid = Int32.Parse(Request["remove_productID"]);

            Product p = de1.Products.Find(pid);

            de1.Products.Remove(p);
           
            de1.SaveChanges();

            Session["msg"] = " Item deleted successfully";

            ViewBag.msg = " Item deleted successfully";


            return RedirectToAction("Index");
        }

    }
}
