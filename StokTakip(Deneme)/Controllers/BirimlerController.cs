using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip_Deneme_.Models.Entity;

namespace StokTakip_Deneme_.Controllers
{
    [Authorize (Roles ="ceo,personel ")]   
    // Bunu yanlızca Rolü "ceo" olan görebilir 
    // Bunu Globalden Toplu olarakdda Yapabiliyoruz ben akılda kalıcı olsun diye ayrı ayrı yaptım 
    public class BirimlerController : Controller
    {

        Stock_Tracking_AppEntities2 db = new Stock_Tracking_AppEntities2();
        public ActionResult Index()
        {
            return View(db.Birimler.ToList());
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Birimler p )
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            db.Birimler.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult GuncelleBilgiGetir(Birimler p)

        {
            var model = db.Birimler.Find(p.ID);

            return View(model);
        }

        public ActionResult Guncelle(Birimler p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SilBilgiGetir(Birimler p)
        {

            var model = db.Birimler.Find(p.ID);
            return View(model);
        }


        public ActionResult Sil(Birimler p)
        {

            db.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}