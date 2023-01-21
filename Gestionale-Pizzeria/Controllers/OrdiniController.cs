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
    public class OrdiniController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Ordini
        public ActionResult Index()
        {
            var ordini = db.Ordini.Include(o => o.DettagliOrdine).Include(o => o.Utenti);
            return View(ordini.ToList());
        }

        // GET: Ordini/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Find(id);
            if (ordini == null)
            {
                return HttpNotFound();
            }
            return View(ordini);
        }

        // GET: Ordini/Create
        public ActionResult Create(int id)
        {
            var utente = db.Utenti.Where(u => u.Username == User.Identity.Name).First();
            DettagliOrdine dettagliOrdine = db.DettagliOrdine.Find(id);
            Ordini ordine = new Ordini();
            ordine.DettagliOrdine = dettagliOrdine;
            ordine.IdDettagliOrdine = dettagliOrdine.IdDettagliOrdine;
            ordine.Confermato = "Ordine Confermato";
            ordine.Evaso = false;
            ordine.Prezzo = dettagliOrdine.Importo;
            ordine.IdUtente = utente.IdUtente;
            //ViewBag.IdDettagliOrdine = new SelectList(db.DettagliOrdine, "IdDettagliOrdine", "Nota");
            //ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Username");
            return View(ordine);
        }

        // POST: Ordini/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrdine,Prezzo,Evaso,Confermato,IdUtente,IdDettagliOrdine")] Ordini ordine)
        {
           
                db.Ordini.Add(ordine);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            

   
        }

        // GET: Ordini/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Find(id);
            if (ordini == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDettagliOrdine = new SelectList(db.DettagliOrdine, "IdDettagliOrdine", "Nota", ordini.IdDettagliOrdine);
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Username", ordini.IdUtente);
            return View(ordini);
        }

        // POST: Ordini/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrdine,Prezzo,Evaso,Confermato,IdUtente,IdDettagliOrdine")] Ordini ordini)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordini).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDettagliOrdine = new SelectList(db.DettagliOrdine, "IdDettagliOrdine", "Nota", ordini.IdDettagliOrdine);
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Username", ordini.IdUtente);
            return View(ordini);
        }

        // GET: Ordini/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Find(id);
            if (ordini == null)
            {
                return HttpNotFound();
            }
            return View(ordini);
        }

        // POST: Ordini/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ordini ordini = db.Ordini.Find(id);
            db.Ordini.Remove(ordini);
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
