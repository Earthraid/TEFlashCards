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
            user_id = CheckSession(user_id);

            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            List<Deck> decks = dDAL.GetDecks(user_id);


            return View("DeckView", decks);
        }

        public ActionResult DeckSearchByName(string user_id, string searchString)
        {
            user_id = CheckSession(user_id);

            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            List<Deck> decks = new List<Deck>();

            decks = dDAL.SearchDecksByName(user_id, searchString);

            return View("DeckView", decks);
        }

        public ActionResult DeckSearchByTag(string user_id, string searchString)
        {
            user_id = CheckSession(user_id);

            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            List<Deck> decks = new List<Deck>();

            decks = dDAL.SearchDecksByTag(user_id, searchString);

            return View("DeckView", decks);
        }

        //Edit deck
        public ActionResult EditDeck(string deck_id)
        {
            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
           // Deck deck = dDAL.EditDeck(deck_id);
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
            //List<Deck> decks = dDAL.AddDeck(user_id);
            return View("NewDeckView"/*, decks*/);
        }
        [HttpPost]
        public ActionResult AddDeck(/*Deck deck*/)
        {
            //DeckSqlDAL dDAL = new DeckSqlDAL();
            //dDAL.AddDeck(deck);
            //List<Deck> decks = dDAL.GetDecks();
            return RedirectToAction("DeckView");
        }

        private string CheckSession(string user_id)
        {
            var currentUser = Session["user_id"];
            if (currentUser == null)
            {
                currentUser = "2";  //default for development
            }

            if (user_id == null)
            {
                //TODO redirect to login page if no user_id
                user_id = currentUser.ToString(); //for development purposes
            }

            return user_id;
        }

    }
}