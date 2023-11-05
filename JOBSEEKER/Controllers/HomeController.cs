using JOBSEEKER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JOBSEEKER.Jobmodel;
using System.IO;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace JOBSEEKER.Controllers
{

    public class HomeController : Controller
    {
        jobseekerEntities db = new jobseekerEntities();
        // GET: Home
        public ActionResult Index()
        {
            var table = new tables
            {

                Companies = db.companies.ToList().Take(6),
                Jobs = db.jobs.ToList().Take(9),
                Categories = db.categories.ToList().Take(8),
               
            };

            Session["khi"] = db.jobs.Where(x => x.locat == "karachi").Count();
            Session["ln"] = db.jobs.Where(x => x.locat == "London").Count();
            Session["lhr"] = db.jobs.Where(x => x.locat == "Lahore").Count();
            Session["bj"] = db.jobs.Where(x => x.locat == "Beiging").Count();
            Session["la"] = db.jobs.Where(x => x.locat == "Los Angles").Count();
            Session["cs"] = db.jobs.Where(x => x.category == "Customer Services").Count();
            Session["hc"] = db.jobs.Where(x => x.category == "Helath Care").Count();
            Session["bd"] = db.jobs.Where(x => x.category == "Business Development").Count();
            Session["mc"] = db.jobs.Where(x => x.category == "Marketing & Communications").Count();
            Session["hr"] = db.jobs.Where(x => x.category == "Human Resources").Count();
            Session["pm"] = db.jobs.Where(x => x.category == "Project Management").Count();
            Session["se"] = db.jobs.Where(x => x.category == "Software Engineering").Count();
            Session["total"] = db.jobs.ToList().Count();

            return View(table);
        }

        public ActionResult findjob()
        {

            Session["total"] = db.jobs.ToList().Count();
            return View(db.jobs.ToList());
        }
        public ActionResult catjob(string cat)
        {
            Session["tota"] = db.jobs.Where(x => x.category == cat).Count();
            var job = db.jobs.Where(x => x.category == cat).ToList();
            return View(job);
        }
        public ActionResult singlejob(int id)
        {
            var detail = db.jobs.Where(x => x.ID == id).FirstOrDefault();
            ViewBag.options = db.jobs.ToList().Take(4);
            return View(detail);
        }
        public ActionResult singlecom(string name)
        {
            var detail = db.companies.Where(x => x.Name == name).FirstOrDefault();
            var comname = detail.Name; 
            ViewBag.options = db.jobs.Where(x =>x.company == comname).ToList().Take(4);
            return View(detail);
        }
        public ActionResult locatjob(string ocat)
        {
            Session["tota"] = db.jobs.Where(x => x.locat == ocat).Count();
            var job = db.jobs.Where(x => x.locat == ocat).ToList();
            return View(job);
        }
        public ActionResult singlecompany(int id)
        {
            var detail = db.companies.Where(x => x.ID == id).FirstOrDefault();
            var comname = detail.Name;
            ViewBag.options = db.jobs.Where(x => x.company == comname).ToList().Take(4);
            return View(detail);
        }

        public ActionResult singlecandiate(int id)
        {
            var detail = db.jobprofiles.Where(x => x.ID == id).FirstOrDefault();
            return View(detail);
        }

        public ActionResult confirm(int id)
        {
            var table = new tables
            {

            jo = db.jobs.Where(x => x.ID == id).FirstOrDefault(),
           
        };
            Session["comname"] = table.jo.company;
            Session["title"] = table.jo.title;
            Session["locat"] = table.jo.locat;

            return View(table);
            
        }
        [HttpPost]
        public ActionResult confirm(string usermail,string username,string userimg,string commail,string title,string locat,string userid)
        {           
            application ap = new application();
                ap.userid = userid;
                ap.useremail = usermail;
                ap.username = username;
                ap.userimg = userimg;
                ap.comemail = commail;
                ap.application_tittle = title;
                ap.application_locat = locat;
                db.applications.Add(ap);
                db.SaveChanges();



        
            return RedirectToAction("Index");

        }

        public ActionResult company()
        {
            Session["company"] = db.companies.Count();
            return View(db.companies.ToList());
        }
        public ActionResult canidate()
        {
            Session["jobprofiles"] = db.jobprofiles.Count();
            return View(db.jobprofiles.ToList());
        }
        public ActionResult about()
        {
            Session["mc"] = db.jobs.ToList().Where(x => x.category == "Marketing & Communications").Count();
            Session["hr"] = db.jobs.ToList().Where(x => x.category == "Human Resources").Count();
            Session["se"] = db.jobs.ToList().Where(x => x.category == "Software Engineering").Count();
            return View();
        }
        public ActionResult contact()
        {
            return View();
        }

        public ActionResult addcompany()
        {
            var ne = Session["CompanyEmail"];

            var returnID = db.companies.Where(x => x.email == ne).FirstOrDefault();
            return View(returnID);
        }

        [HttpPost]
        public ActionResult addcompany(company com, HttpPostedFileBase file, HttpPostedFileBase logo, HttpPostedFileBase img2, HttpPostedFileBase img1, HttpPostedFileBase img3, HttpPostedFileBase img4)
        {
            if (logo != null)
            {
                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);
                com.logo = logo.FileName;
            }
            else if (file != null)
            {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.Cover = file.FileName;
            }
            else if (img1 != null)
            {
                var pa = Server.MapPath(Path.Combine("~/Image/" + img1.FileName));
                img1.SaveAs(pa);
                com.img1 = img1.FileName;
            }
            else if (img2 != null)
            {
                var pat = Server.MapPath(Path.Combine("~/Image/" + img2.FileName));
                img2.SaveAs(pat);
                com.img2 = img2.FileName;
            }
            else if (img3 != null)
            {
                var p = Server.MapPath(Path.Combine("~/Image/" + img3.FileName));
                img3.SaveAs(p);
                com.img3 = img3.FileName;
            }
            else if (img4 != null)
            {
                var a = Server.MapPath(Path.Combine("~/Image/" + img4.FileName));
                img4.SaveAs(a);
                com.img4 = img4.FileName;
            }

            db.Entry(com).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Success"] = "Success message text.";
            return RedirectToAction("loginR");
        }
        public ActionResult addcanditate()
        {
            var ne = Session["email"];
           
            var returnID = db.jobprofiles.Where(x => x.email == ne).FirstOrDefault();
            return View(returnID);
        }

        [HttpPost]
        public ActionResult addcanditate(jobprofile com, HttpPostedFileBase file, HttpPostedFileBase logo)
        {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.cover = file.FileName;

                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);
                com.img = logo.FileName;

                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
           
            TempData["company"] = "Success message text.";
            if (com.img != null)
            {
                Session["img"] = com.img.ToString();
            }
            return RedirectToAction("loginU");

        }


        //               COMPANY DASHBOARD
        public ActionResult companydashboard()
        {
            var ne = Session["CompanyName"];
            var applicID = db.applications.Where(x => x.comemail == ne).Count();
            var applic = db.applications.Where(x => x.comemail == ne).ToList();
            var returnID = db.jobs.Where(x => x.company == ne).Count();
            Session["totaljobs"] = returnID;
            Session["applicactions"] = applicID;
            return View(applic);
        }
        public ActionResult editcompany()
        {
            var ne = Session["CompanyEmail"];

            var returnID = db.companies.Where(x => x.email == ne).FirstOrDefault();
            return View(returnID);
        }

        [HttpPost]
        public ActionResult editcompany(company com, HttpPostedFileBase file, HttpPostedFileBase logo, HttpPostedFileBase img2, HttpPostedFileBase img1, HttpPostedFileBase img3, HttpPostedFileBase img4)
        {
            if (logo != null)
            {
                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);
                com.logo = logo.FileName;
            }
            if (file != null)
            {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.Cover = file.FileName;
            }
            if (img1 != null)
            {
                var pa = Server.MapPath(Path.Combine("~/Image/" + img1.FileName));
                img1.SaveAs(pa);
                com.img1 = img1.FileName;
            }
            if (img2 != null)
            {
                var pat = Server.MapPath(Path.Combine("~/Image/" + img2.FileName));
                img2.SaveAs(pat);
                com.img2 = img2.FileName;
            }
            if (img3 != null)
            {
                var p = Server.MapPath(Path.Combine("~/Image/" + img3.FileName));
                img3.SaveAs(p);
                com.img3 = img3.FileName;
            }
            if (img4 != null)
            {
                var a = Server.MapPath(Path.Combine("~/Image/" + img4.FileName));
                img4.SaveAs(a);
                com.img4 = img4.FileName;
            }

            db.Entry(com).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("companydashboard");
        }
        public ActionResult addjob()
        {
            ViewBag.cat = db.categories.ToList();
            Session["total_cat"] = db.categories.Count();
            return View();
        }

        [HttpPost]
        public ActionResult addjob(job com, HttpPostedFileBase file)
        {
            var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
            file.SaveAs(h);
            com.cover = file.FileName;
            db.jobs.Add(com);
            db.SaveChanges();
            return RedirectToAction("Managejobs");
        }
         
         public ActionResult ComCanidates()
        {
            var ne = Session["CompanyName"];
            Session["no"] = db.applications.Where(x => x.comemail == ne).Count();
            var applic = db.applications.Where(x => x.comemail == ne).ToList();
            return View(applic);

        } 


        public ActionResult Managejobs()
        {
            var ne = Session["CompanyName"];
            Session["Count"] = db.jobs.Where(x => x.company == ne).Count();
            var applic = db.jobs.Where(x => x.company == ne).ToList();
            return View(applic);
        }

        public ActionResult editjobs(int id)
        {

            var applic = db.jobs.Where(x => x.ID == id).FirstOrDefault();
            return View(applic);
        }
        [HttpPost]
        public ActionResult editjobs(job jo, HttpPostedFileBase file)
        {

            if (file != null)
            {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                jo.cover = file.FileName;
            }
            db.Entry(jo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("companydashboard");
        }

       


        //user login & Registration
        public ActionResult loginU()
        {
            if (Session["UserLogin"] != null)
            {
                var finalID = Session["Email"];
                var con = db.jobprofiles.Where(x => x.email == finalID).FirstOrDefault();
                if (con.phone == null && con.about == null)
                {
                    return RedirectToAction("addcanditate");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult loginU(jobprofile jp)
        {
            if (ModelState.IsValid) {

                var user = db.jobprofiles.Where(x => x.email.Equals(jp.email) && x.password.Equals(jp.password)).FirstOrDefault();
                if (user != null )
                {
                    if (jp.password == user.password && jp.email == user.email)
                    {
                        Session["UserLogin"] = user.ID;
                        Session["Name"] = user.name.ToString();

                        Session["Email"] = user.email.ToString();
                       
                        var finalID = Session["Email"];
                        var con = db.jobprofiles.Where(x => x.email == finalID).FirstOrDefault();
                        if(con.img != null)
                        {
                             Session["img"] = user.img.ToString();
                        }

                        if (con.location == null && con.about == null)
                        {
                           return RedirectToAction("addcanditate");
                        }
                        else
                        {
                            TempData["Success"] = "Success message text.";
                            return RedirectToAction("Index");
                        }


                       
                        if (Session["CompanyLogin"] != null)
                        {
                            Session["CompanyLogin"] = null;
                        }

                    }

                }
                
            }
            ViewData["Error"] = "Error message text.";
            Session["Wrongpass"] = 1;
            return View();
        }


        public ActionResult RegisterU()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterU(jobprofile jp)
        {
            db.jobprofiles.Add(jp);
            db.SaveChanges();
            return RedirectToAction("addcanditate");
        }


        public ActionResult logoutU()
        {
            if (Session["UserLogin"] != null)
            {
                Session["Name"] = null;
                Session["UserLogin"] = null;
                Session["Email"] = null;
            }
            return RedirectToAction("Index");
        }



        // company login & Registration
        public ActionResult loginR()
        {
            if (Session["CompanyLogin"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult loginR(company cp)
        {
            if (ModelState.IsValid)
            {
                var comp = db.companies.Where(x => x.email.Equals(cp.email) && x.password.Equals(cp.password)).FirstOrDefault();
                if (comp != null)
                {
                    if (cp.password == comp.password && cp.email == comp.email)
                    {
                     
                        Session["CompanyLogin"] = comp.ID.ToString();
                        Session["CompanyName"] = comp.Name.ToString();
                        Session["CompanyEmail"] = comp.email.ToString();
                        var finalID = Session["CompanyEmail"];
                        var con = db.companies.Where(x => x.email == finalID).FirstOrDefault();
                        if (con.logo != null)
                        {
                            Session["comimg"] = comp.logo.ToString();
                        }
                        if (Session["UserLogin"] != null)
                        {
                            Session["UserLogin"] = null;
                        }
                        if (con.size == null && con.industry == null)
                        {
                            return RedirectToAction("addcompany");
                        }
                        else
                        {
                            TempData["company"] = "Success message text.";
                            return RedirectToAction("Index");
                        }

                    }
                }

            }

            Session["Wrongpass"] = 1;
            return View();
        }

        public ActionResult RegisterR()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterR(company jp)
        {
            db.companies.Add(jp);
            db.SaveChanges();
            return RedirectToAction("addcompany");
        }



        public ActionResult logoutR()
        {
            if (Session["CompanyLogin"] != null)
            {
                Session["CompanyName"] = null;
                Session["CompanyLogin"] = null;
                Session["CompanyEmail"] = null;
            }
            return RedirectToAction("Index");
        }
    }
}