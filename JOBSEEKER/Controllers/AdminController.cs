using JOBSEEKER.Jobmodel;
using JOBSEEKER.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace JOBSEEKER.Controllers
{
    public class AdminController : Controller
    {
        jobseekerEntities db = new jobseekerEntities();
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(admin cp)
        {
            if (ModelState.IsValid)
            {
                var comp = db.admins.Where(x => x.email.Equals(cp.email) && x.password.Equals(cp.password)).FirstOrDefault();
                if (comp != null)
                {
                    if (cp.password == comp.password && cp.email == comp.email)
                    {

                        Session["ALogin"] = comp.id.ToString();
                        Session["AName"] = comp.name.ToString();
                        Session["AEmail"] = comp.email.ToString();
                        Session["Apic"] = comp.img;
                   
                        return RedirectToAction("Index");
                    }
                }

            }

            Session["Wrongpass"] = 1;
            return View();
        }

        public ActionResult logout()
        {
            if (Session["ALogin"] != null)
            {
                Session["ALogin"] = null;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            if (Session["ALogin"] != null)
            {
                var table = new tables
                {
                    applications = db.applications.ToList().Take(5),
                    Companies = db.companies.ToList().Take(5),
                    Jobs = db.jobs.ToList().Take(5),
                    Categories = db.categories.ToList().Take(5),
                    jobp = db.jobprofiles.ToList().Take(5)
                };
                Session["app"] = db.applications.Count();
                Session["jobp"] = db.jobprofiles.Count();
                Session["com"] = db.companies.Count();
                Session["jobs"] = db.jobs.Count();
                return View(table);
            }
            return RedirectToAction("login");
        }
        public ActionResult listapplication()
        {
            if (Session["ALogin"] != null)
            {
                return View(db.applications.ToList());
            }
            return RedirectToAction("login");
        }
        public ActionResult listcandiodate()
        {
            if (Session["ALogin"] != null)
            {
                return View(db.jobprofiles.ToList());
            }
            return RedirectToAction("login");
    }
        public ActionResult listjobs()
        {
            if (Session["ALogin"] != null)
            {
                return View(db.jobs.ToList());
            }
            return RedirectToAction("login");
        }
        public ActionResult listcompany()
        {
            return View(db.companies.ToList());
        }
        public ActionResult listcategory()
        {
            Session["cat"] = db.categories.Count();
            return View(db.categories.ToList());
        }

        public ActionResult addcanidadate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addcanidadate(jobprofile com, HttpPostedFileBase logo, HttpPostedFileBase file)
        {
            if (logo == null && file == null) 
            {
                db.jobprofiles.Add(com);
                db.SaveChanges();
            }
            if (file != null)
            {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.cover = file.FileName;

                db.jobprofiles.Add(com);
                    db.SaveChanges();
                
            }
            if(logo != null)
            {
                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);

                com.img = logo.FileName;
                db.jobprofiles.Add(com);
                db.SaveChanges();
            }
            if(logo != null && file!= null)
            {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.cover = file.FileName;

                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);

                com.img = logo.FileName;
                db.jobprofiles.Add(com);
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        public ActionResult addcompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addcompany(company com,HttpPostedFileBase logo, HttpPostedFileBase file, HttpPostedFileBase img1, HttpPostedFileBase img2,HttpPostedFileBase img3, HttpPostedFileBase img4)
        {
            if (logo == null && file == null &&img1 == null&&img2 == null&&img3 == null&&img4 ==null )
            {
                db.companies.Add(com);
                db.SaveChanges();
            }
           if (logo != null && file != null && img1 != null && img2 != null && img3 != null && img4 != null)
            {
                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);
                com.logo = logo.FileName;

                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.Cover = file.FileName;

                var pa = Server.MapPath(Path.Combine("~/Image/" + img1.FileName));
                img1.SaveAs(pa);
                com.img1 = img1.FileName;

                var pat = Server.MapPath(Path.Combine("~/Image/" + img2.FileName));
                img2.SaveAs(pat);
                com.img2 = img2.FileName;

                var p = Server.MapPath(Path.Combine("~/Image/" + img3.FileName));
                img3.SaveAs(p);
                com.img3 = img3.FileName;

                var a = Server.MapPath(Path.Combine("~/Image/" + img4.FileName));
                img4.SaveAs(a);
                com.img4 = img4.FileName;

                db.companies.Add(com);
                db.SaveChanges();
            }
            if (logo == null && file != null && img1 == null && img2 == null && img3 == null && img4 == null)
            {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.Cover = file.FileName;
                db.companies.Add(com);
                db.SaveChanges();
            }
            if (logo != null && file == null && img1 == null && img2 == null && img3 == null && img4 == null)
            {
                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);
                com.logo = logo.FileName;
                db.companies.Add(com);
                db.SaveChanges();
            }
            if (logo == null && file == null && img1 != null && img2 == null && img3 == null && img4 == null)
            {
                var pa = Server.MapPath(Path.Combine("~/Image/" + img1.FileName));
                img1.SaveAs(pa);
                com.img1 = img1.FileName;
                db.companies.Add(com);
                db.SaveChanges();
            }
            if (logo == null && file == null && img1 == null && img2 != null && img3 == null && img4 == null)
            {
                var pat = Server.MapPath(Path.Combine("~/Image/" + img2.FileName));
                img2.SaveAs(pat);
                com.img2 = img2.FileName;
                db.companies.Add(com);
                db.SaveChanges();
            }
            if (logo == null && file == null && img1 == null && img2 == null && img3 != null && img4 == null)
            {
                var p = Server.MapPath(Path.Combine("~/Image/" + img3.FileName));
                img3.SaveAs(p);
                com.img3 = img3.FileName;
                db.companies.Add(com);
                db.SaveChanges();
            }
            if (logo == null && file == null && img1 == null && img2 != null && img3 == null && img4 != null)
            {
                var a = Server.MapPath(Path.Combine("~/Image/" + img4.FileName));
                img4.SaveAs(a);
                com.img4 = img4.FileName;
                db.companies.Add(com);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    
    public ActionResult addcategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addcategory(category cat)
        {
            db.categories.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult editcategory(int id)
        {
            var returnID = db.categories.Where(x => x.ID == id).FirstOrDefault();
            return View(returnID);
        }
        [HttpPost]
        public ActionResult editcategory(category cat)
        {
            db.Entry(cat).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult editcanidadate(int id)
        {
            var returnID = db.jobprofiles.Where(x => x.ID == id).FirstOrDefault();
            return View(returnID);
        }
        [HttpPost]
        public ActionResult editcanidadate(jobprofile com,HttpPostedFileBase logo,HttpPostedFileBase file)
        {
            if (logo == null && file == null)
            {
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (file != null)
            {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.cover = file.FileName;

                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();

            }
            if (logo != null)
            {
                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);
                com.img = logo.FileName;

                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (logo != null && file != null)
            {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.cover = file.FileName;

                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);
                com.img = logo.FileName;

                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult editcompany(int id)
        {
            var returnID = db.companies.Where(x => x.ID == id).FirstOrDefault();
            return View(returnID);
            
        }
        [HttpPost]
        public ActionResult editcompany(company com, HttpPostedFileBase logo, HttpPostedFileBase file, HttpPostedFileBase img1, HttpPostedFileBase img2, HttpPostedFileBase img3, HttpPostedFileBase img4)
        {
            if (logo == null && file == null && img1 == null && img2 == null && img3 == null && img4 == null)
            {
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (logo != null && file != null && img1 != null && img2 != null && img3 != null && img4 != null)
            {
                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);
                com.logo = logo.FileName;

                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.Cover = file.FileName;

                var pa = Server.MapPath(Path.Combine("~/Image/" + img1.FileName));
                img1.SaveAs(pa);
                com.img1 = img1.FileName;

                var pat = Server.MapPath(Path.Combine("~/Image/" + img2.FileName));
                img2.SaveAs(pat);
                com.img2 = img2.FileName;

                var p = Server.MapPath(Path.Combine("~/Image/" + img3.FileName));
                img3.SaveAs(p);
                com.img3 = img3.FileName;

                var a = Server.MapPath(Path.Combine("~/Image/" + img4.FileName));
                img4.SaveAs(a);
                com.img4 = img4.FileName;

                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (logo == null && file != null && img1 == null && img2 == null && img3 == null && img4 == null)
            {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.Cover = file.FileName;
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (logo != null && file == null && img1 == null && img2 == null && img3 == null && img4 == null)
            {
                var path = Server.MapPath(Path.Combine("~/Image/" + logo.FileName));
                logo.SaveAs(path);
                com.logo = logo.FileName;
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (logo == null && file == null && img1 != null && img2 == null && img3 == null && img4 == null)
            {
                var pa = Server.MapPath(Path.Combine("~/Image/" + img1.FileName));
                img1.SaveAs(pa);
                com.img1 = img1.FileName;
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (logo == null && file == null && img1 == null && img2 != null && img3 == null && img4 == null)
            {
                var pat = Server.MapPath(Path.Combine("~/Image/" + img2.FileName));
                img2.SaveAs(pat);
                com.img2 = img2.FileName;
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (logo == null && file == null && img1 == null && img2 == null && img3 != null && img4 == null)
            {
                var p = Server.MapPath(Path.Combine("~/Image/" + img3.FileName));
                img3.SaveAs(p);
                com.img3 = img3.FileName;
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (logo == null && file == null && img1 == null && img2 != null && img3 == null && img4 != null)
            {
                var a = Server.MapPath(Path.Combine("~/Image/" + img4.FileName));
                img4.SaveAs(a);
                com.img4 = img4.FileName;
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Editjob(int id)
        {
            var returnID = db.jobs.Where(x => x.ID == id).FirstOrDefault();
            return View(returnID);
        }

        [HttpPost]
        public ActionResult Editjob(job com, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var h = Server.MapPath(Path.Combine("~/Image/" + file.FileName));
                file.SaveAs(h);
                com.cover = file.FileName;
                db.Entry(com).State = EntityState.Modified;
                db.SaveChanges();

            }

            db.Entry(com).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}