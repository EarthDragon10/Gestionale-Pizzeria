using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gestionale_Pizzeria.Models;

namespace Gestionale_Pizzeria.Controllers
{
    [Authorize]
    public class DettagliOrdineController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: DettagliOrdine
        public ActionResult Index()
        {
            var dettagliOrdine = db.DettagliOrdine.Include(d => d.Prodotti);
            return View(dettagliOrdine.ToList());
        }

        // GET: DettagliOrdine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliOrdine dettagliOrdine = db.DettagliOrdine.Find(id);
            if (dettagliOrdine == null)
            {
                return HttpNotFound();
            }
            return View(dettagliOrdine);
        }

        // GET in PartialView di Prodotto
        public ActionResult PartialViewSingoloProdotto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            TempData["Prodotto"] = prodotti;
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // GET: DettagliOrdine/Create
        public ActionResult Create()
        {
            ViewBag.IdProdotto = new SelectList(db.Prodotti, "IdProdotto", "Nome");
            return View();
        }

        // POST: DettagliOrdine/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDettagliOrdine,Quantita,Nota,IdProdotto")] DettagliOrdine dettagliOrdine)
        {
            dettagliOrdine.Prodotti = TempData["Prodotto"] as Prodotti;

            if (ModelState.IsValid)
            {
                ViewBag.IdProdotto = new SelectList(db.Prodotti, "IdProdotto", "Nome", dettagliOrdine.IdProdotto);
                dettagliOrdine.Importo = dettagliOrdine.Quantita * Convert.ToInt32(dettagliOrdine.Prodotti.PrezzoVendita);
                dettagliOrdine.Confermato = "ORDINE DA CONFERMARE";
                dettagliOrdine.StatoPreparazione = "Preparazione in corso...";
                db.DettagliOrdine.Add(dettagliOrdine);
                db.SaveChanges();
                //DettagliOrdine DbDettagliOrdine = db.DettagliOrdine.ToList().LastOrDefault();
                //DettagliOrdine.ListDettagliOrdine.Add(DbDettagliOrdine);
                
                return RedirectToAction("Index", "Carrello");
            }

            return View(dettagliOrdine);
        }

        // GET: DettagliOrdine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliOrdine dettagliOrdine = db.DettagliOrdine.Find(id);
            if (dettagliOrdine == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProdotto = new SelectList(db.Prodotti, "IdProdotto", "Nome", dettagliOrdine.IdProdotto);
            return View(dettagliOrdine);
        }

        // POST: DettagliOrdine/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDettagliOrdine,Quantita,Nota,Importo,IdProdotto")] DettagliOrdine dettagliOrdine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dettagliOrdine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProdotto = new SelectList(db.Prodotti, "IdProdotto", "Nome", dettagliOrdine.IdProdotto);
            return View(dettagliOrdine);
        }

        // GET: DettagliOrdine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliOrdine dettagliOrdine = db.DettagliOrdine.Find(id);
            if (dettagliOrdine == null)
            {
                return HttpNotFound();
            }
            return View(dettagliOrdine);
        }

        // POST: DettagliOrdine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DettagliOrdine dettagliOrdine = db.DettagliOrdine.Find(id);
            db.DettagliOrdine.Remove(dettagliOrdine);
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
