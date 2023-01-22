using Gestionale_Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestionale_Pizzeria.Controllers
{
    
    public class CarrelloController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        // GET: Carrello
        public ActionResult Index()
        {
            List<DettagliOrdine> DbDettagliOrdine = db.DettagliOrdine.ToList();
            DettagliOrdine.ListDettagliOrdine = DbDettagliOrdine;
            return View(DettagliOrdine.ListDettagliOrdine);
        }

        [HttpPost]
        public ActionResult Index(int id)
        {
            var utente = db.Utenti.Where(u => u.Username == User.Identity.Name).First();
            DettagliOrdine dettagliOrdine = db.DettagliOrdine.Find(id);
            Ordini ordine = new Ordini();
            ordine.DettagliOrdine = dettagliOrdine;
            ordine.IdDettagliOrdine = dettagliOrdine.IdDettagliOrdine;
            ordine.Confermato = "Ordine Confermato";
            ordine.Evaso = false;
            ordine.Prezzo = dettagliOrdine.Importo;
            ordine.Utenti = utente;
            ordine.IdUtente = utente.IdUtente;

            db.Ordini.Add(ordine);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


    }
}