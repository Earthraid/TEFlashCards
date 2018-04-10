using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using System.Configuration;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class DeckController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["HotelFlashCardsDB"].ConnectionString;

        // GET: Deck
        public ActionResult Index(string user_id)
        {   
            if(user_id == null)
            {
                user_id = "2";
            }
            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            List<Deck> decks = dDAL.GetDecks(user_id);
            return View("DeckView", decks);
        }
        public ActionResult DeckSearch(string user_id, string search)
        {
            if (user_id == null || user_id == "")
            {
                user_id = "2";
            }
            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            List<Deck> decks = dDAL.SearchDecksByName(user_id, search);

            return View("DeckView", decks);
        }
        //Edit deck
        public ActionResult EditDeck(string deck_id)
        {
            //DeckSqlDAL dDAL = new DeckSqlDAL();
            //Deck deck = dDAL.GetDeck(deck_id);
            return View("EditDeck"/*, deck*/);
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
            //DeckSqlDAL dDAL = new DeckSqlDAL();
            //List<Deck> decks = dDAL.GetDecks(user_id);
            return View("DeckView"/*, decks*/);
        }
        [HttpPost]
        public ActionResult AddDeck(/*Deck deck*/)
        {
            //DeckSqlDAL dDAL = new DeckSqlDAL();
            //dDAL.AddDeck(deck);
            //List<Deck> decks = dDAL.GetDecks();
            return RedirectToAction("DeckView");
        }
    
    }
}