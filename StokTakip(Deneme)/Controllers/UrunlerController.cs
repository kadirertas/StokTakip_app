using StokTakip_Deneme_.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Data.Entity;

namespace StokTakip_Deneme_.Controllers
{

    [Authorize]
    public class UrunlerController : Controller
    {
  Stock_Tracking_AppEntities2 db = new Stock_Tracking_AppEntities2();

        public ActionResult Index()
        {
            var model = db.Urunler.ToList();  
            return View(model);

        }
        [HttpGet]
        public ActionResult Ekle ()
        {
            var model = new Urunler();

            Yenile(model);

            return View(model);
        }

        private void Yenile(Urunler model)
        {
            List<Kategoriler> KategoriList = db.Kategoriler.OrderBy(x => x.Kategori).ToList();

            model.KategoriListesi = (from x in KategoriList
                                     select new SelectListItem
                                     {

                                         Text = x.Kategori,
                                         Value = x.ID.ToString()


                                     }).ToList();


            List<Birimler> BirimList = db.Birimler.OrderBy(x => x.Birim).ToList();


            model.BirimListesi = (from x in BirimList
                                  select new SelectListItem
                                  {

                                      Text = x.Birim,
                                      Value = x.ID.ToString()


                                  }).ToList();
        }



        [HttpPost]


        public ActionResult Ekle ( Urunler p )
        
        {
            if (!ModelState.IsValid)
            {
                var model = new Urunler();

                Yenile(model);

                return View(model);
            }
            db.Entry(p).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges  (); 
            return RedirectToAction("Index");
        
        
        }


        public JsonResult GetMarka  (int id2 )
        {
            var model = new Urunler();


            List<Markalar> MarkaList = db.Markalar.Where(x=>x.KategoriID==id2).OrderBy(x => x.Marka).ToList();


            model.MarkaListesi = (from x in MarkaList
                                  select new SelectListItem
                                  {

                                      Text = x.Marka,
                                      Value = x.ID.ToString()


                                  }).ToList();
 
            return Json(model.MarkaListesi, JsonRequestBehavior.AllowGet);
       
        }


        [HttpGet]
        public ActionResult GuncelleBilgiGetir(int id)
        {


            var model = db.Urunler.Find(id); 
            Yenile(model);
            List<Markalar> MarkaList = db.Markalar.Where(x => x.KategoriID ==model.KategoriID).OrderBy(x => x.Marka).ToList();


            model.MarkaListesi = (from x in MarkaList
                                  select new SelectListItem
                                  {

                                      Text = x.Marka,
                                      Value = x.ID.ToString()


                                  }).ToList();

            return View(model);


        }
        [HttpPost]

        public ActionResult Guncelle(Urunler p)
        {
            if (!ModelState.IsValid)
            {
                var model = db.Urunler.Find(p.ID);
                Yenile(model);
                List<Markalar> markaList = db.Markalar
                    .Where(x => x.KategoriID == model.KategoriID)
                    .OrderBy(x => x.Marka)
                    .ToList();

                model.MarkaListesi = markaList
                    .Select(x => new SelectListItem
                    {
                        Text = x.Marka,
                        Value = x.ID.ToString()
                    })
                    .ToList();

                return View("GuncelleBilgiGetir", model); // Hata durumunda aynı view'ı tekrar göster
            }

            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult SilBilgiGetir(Urunler p)
        {

            var model = db.Urunler.Find(p.ID);
            return View(model);
        }
        public ActionResult Sil (Urunler p )

        {

          
            db.Entry(p).State=System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");  
        }
    }
}