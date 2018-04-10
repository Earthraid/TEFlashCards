using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using System.Configuration;

namespace Capstone.Web.Controllers
{
    public class DeckController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["HotelFlashCardsDB"].ConnectionString;

        // GET: Deck
        public ActionResult Decks(string user_id)
        {
            DeckSqlDAL dDAL = new DeckSqlDAL();
            List<Deck> decks = dDAL.GetDecks(user_id);
            return View("DeckView", decks);
        }
        
        //Edit deck
        public ActionResult EditDeck(string deck_id)
        {
            DeckSqlDAL dDAL = new DeckSqlDAL();
            Deck deck = dDAL.GetDeck(deck_id);
            return View("EditDeck", deck);
        }

        //New deck form
        public ActionResult NewDeck()
        {
            return View("NewDeckView");
        }

        //Add new deck
        [HttpGet]
        public ActionResult AddDeck(string user_id)
        {
            DeckSqlDAL dDAL = new DeckSqlDAL();
            List<Deck> decks = dDAL.GetDecks(user_id);
            return View("DeckView", decks);
        }
        [HttpPost]
        public ActionResult AddDeck(Deck deck)
        {
            DeckSqlDAL dDAL = new DeckSqlDAL();
            dDAL.AddDeck(deck);
            List<Deck> decks = dDAL.GetDecks();
            return RedirectToAction("DeckView");
        }
    
    }
}