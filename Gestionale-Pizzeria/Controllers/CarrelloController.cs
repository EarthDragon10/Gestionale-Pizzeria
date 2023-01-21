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
    }
}