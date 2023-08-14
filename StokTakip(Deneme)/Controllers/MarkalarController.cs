using StokTakip_Deneme_.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StokTakip_Deneme_.Controllers
{

    [Authorize(Roles = "ceo")]
    public class MarkalarController : Controller
    {
        Stock_Tracking_AppEntities2 db = new Stock_Tracking_AppEntities2();
        public ActionResult Index()
        {

            var model = db.Markalar.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            SelecteBilgiGetir();
            return View();

        }

        private void SelecteBilgiGetir()
        {
            var model = new Markalar();

            List<SelectListItem> Liste = new List<SelectListItem>(from x in db.Kategoriler

                                                                  select new SelectListItem
                                                                  {
                                                                      Value = x.ID.ToString(),
                                                                      Text = x.Kategori

                                                                  }

                                                                    ).ToList();

            ViewBag.l = Liste;
        }

        [HttpPost]
        public ActionResult Ekle(Markalar p)

        {
            if (!ModelState.IsValid)
            {
                ViewBag.KategoriID = new SelectList(db.Kategoriler, "ID", "Kategori", p.KategoriID);
                return View();
            }
            
            db.Markalar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult GuncelleBilgiGetir (int id ) 
        
        {
            SelecteBilgiGetir(); 
            var ara = db.Markalar.Find(id); 
            return View(ara);        
        }

        public ActionResult Guncelle ( Markalar p)
        {
            if (!ModelState.IsValid)
            {
                SelecteBilgiGetir();
                return View("GuncelleBilgiGetir");
            }

            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SilBilgiGetir(Markalar p )
            
        {

            var getir = db.Markalar.Find(p.ID); 

            return View(getir);
        
        }
        public ActionResult Sil (Markalar p  )
        { 

             db.Entry(p).State= System.Data.Entity.EntityState.Deleted; db.SaveChanges();
            return RedirectToAction("Index");   
        }
    }
}