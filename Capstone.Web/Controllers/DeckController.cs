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


            return View("Deck", decks);
        }

        public ActionResult DeckSearchByName(string user_id, string searchString)
        {
            user_id = CheckSession(user_id);

            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            List<Deck> decks = dDAL.SearchDecksByName(user_id, searchString);

            return View("Deck", decks);
        }

        public ActionResult DeckSearchByTag(string user_id, string searchString)
        {
            user_id = CheckSession(user_id);

            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            List<Deck> decks = new List<Deck>();

            decks = dDAL.SearchDecksByTag(user_id, searchString);

            return View("Deck", decks);
        }

        //EDIT DECK
        [HttpGet]
        public ActionResult EditDeck(int id)
        {
            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            Deck deck = dDAL.GetDeckByDeckID(id.ToString());
            return View(deck);
        }

        //Deck Name
        [HttpPost]
        public ActionResult EditDeckName(string deck_id, string deck_name)
        {
            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            // Deck deck = dDAL.EditDeckName(deck_id);
            // or Deck deck = dDAL.GetDeckByDeckID(deck_id);
            return View("EditDeck", deck_id);
        }

        //Deck Tags
        [HttpPost]
        public ActionResult EditDeckTags(string deck_id, string deck_tag)
        {
            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            // Deck deck = dDAL.AddTag(deck_id);
            return View("EditDeck", deck_id);
        }


        //Add new deck
        [HttpGet]
        public ActionResult AddDeck()
        {
            return View("NewDeck");
        }

        [HttpPost]
        public ActionResult AddDeck(string user_id, Deck model)
        {
            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            user_id = CheckSession(user_id);
            if (CheckSession(user_id) != null)
            {
                dDAL.AddDeck(user_id, model.Name);
            }
            List<Deck> decks = dDAL.GetDecks(user_id);
            return RedirectToAction("Deck", decks);
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