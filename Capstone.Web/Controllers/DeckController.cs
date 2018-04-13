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
        private DeckSqlDAL deckDAL = new DeckSqlDAL(ConfigurationManager.ConnectionStrings["HotelFlashCardsDB"].ConnectionString);

        // GET: Deck
        public ActionResult Index(string user_id)
        {
            user_id = CheckSession(user_id);

            List<Deck> decks = deckDAL.GetDecks(user_id);

            return View("Deck", decks);
        }

        //Search for decks by name
        public ActionResult DeckSearchByName(string user_id, string searchString)
        {
            user_id = CheckSession(user_id);

            List<Deck> decks = deckDAL.SearchDecksByName(user_id, searchString);

            return View("Deck", decks);
        }

        //Search for decks by tag
        public ActionResult DeckSearchByTag(string user_id, string searchString)
        {
            user_id = CheckSession(user_id);

            List<Deck> decks = new List<Deck>();

            decks = deckDAL.SearchDecksByTag(user_id, searchString);

            return View("Deck", decks);
        }

        //EDIT DECK
        [HttpGet]
        public ActionResult EditDeck(int id)
        {
            Deck deck = deckDAL.GetDeckByDeckID(id.ToString());
            Session["deck_ID"] = deck.DeckID;
            return View(deck);
        }

        //Deck Name
        [HttpPost]
        public ActionResult EditDeckName(Deck model)
        {
            deckDAL.ModifyDeckName(model.DeckID, model.Name);
            Deck deck = deckDAL.GetDeckByDeckID(model.DeckID);

            return RedirectToAction(deck.DeckID, "Deck/EditDeck");
        }

        //Deck Tags
        [HttpPost]
        public ActionResult AddDeckTag(Deck model)
        {
            Deck curDeck = deckDAL.GetDeckByDeckID(model.DeckID);
            curDeck.AddTagToDeck(model.TagName);
            return RedirectToAction(curDeck.DeckID, "Deck/EditDeck");
        }
        [HttpGet]
        public ActionResult RemoveDeckTag(string tagName)
        {
            string deckID = Session["deck_ID"].ToString();
            Deck curDeck = deckDAL.GetDeckByDeckID(deckID);
            curDeck.RemoveTagFromDeck(tagName);
            return RedirectToAction(curDeck.DeckID, "Deck/EditDeck");
        }
        //Remove Card
        [HttpGet]
        public ActionResult RemoveCard()
        {
            return View("EditDeck");
        }

        [HttpPost]
        public ActionResult RemoveCard(string card_id, string deck_id)
        {
            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            //Deck deck = dDAL.RemoveCardFromDeck(card_id, deck_id);
            return RedirectToAction("EditDeck", deck_id);
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
            user_id = CheckSession(user_id);
            if (CheckSession(user_id) != null)
            {
                deckDAL.AddDeck(user_id, model.Name);
            }
            List<Deck> decks = deckDAL.GetDecks(user_id);
            return RedirectToAction("Index");
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