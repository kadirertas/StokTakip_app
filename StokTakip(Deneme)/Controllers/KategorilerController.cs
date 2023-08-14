using StokTakip_Deneme_.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using static System.Web.Razor.Parser.SyntaxConstants;
using System.Web.UI.WebControls;

namespace StokTakip_Deneme_.Controllers
{

    [Authorize(Roles ="ceo")]
    public class KategorilerController : Controller
    {
        Stock_Tracking_AppEntities2 db = new Stock_Tracking_AppEntities2();

        public ActionResult Index()
        {
            return View(db.Kategoriler.ToList());

        }

        public ActionResult Ekle()
        {
            return View();
        }
        public ActionResult Ekle2(Kategoriler p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            db.Kategoriler.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult GuncelleBilgiGetir(Kategoriler p)

        {
            var model = db.Kategoriler.Find(p.ID);

            return View(model);
        }

        public ActionResult Guncelle(Kategoriler p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



         public ActionResult SilBilgiGetir(Kategoriler p )
        {
            var model = db.Kategoriler.Find(p.ID); 

            if(model== null  ) return HttpNotFound();   

             return View(model) ;
        }

        public ActionResult Sil (Kategoriler p )
        {


            db.Entry(p).State= System.Data.Entity.EntityState.Deleted; 
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }



        }


       
}






