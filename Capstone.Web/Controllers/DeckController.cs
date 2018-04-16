﻿using System;
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
            
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            user_id = Session["userid"].ToString();
            List<Deck> decks = deckDAL.GetDecks(user_id);

            return View("Deck", decks);
        }

        //Search for decks by name
        public ActionResult DeckSearchByName(string user_id, string searchString)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Deck> decks = deckDAL.SearchDecksByName(user_id, searchString);
            if (decks.Count == 0)
            {
                Deck emptyDeck = new Deck
                {
                    Name = "No decks found with that name.",
                };
                decks.Add(emptyDeck);
            }

            return View("Deck", decks);
        }

        //Search for decks by tag
        public ActionResult DeckSearchByTag(string user_id, string searchString)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Deck> decks = new List<Deck>();

            decks = deckDAL.SearchDecksByTag(user_id, searchString);
            if (decks.Count == 0)
            {
                Deck emptyDeck = new Deck
                {
                    Name = "No decks found with that tag.",
                };
                decks.Add(emptyDeck);
            }

            return View("Deck", decks);
        }

        //EDIT DECK
        [HttpGet]
        public ActionResult EditDeck(int id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Deck deck = deckDAL.GetDeckByDeckID(id.ToString());
            Session["deck_ID"] = deck.DeckID;
            return View(deck);
        }

        //Deck Name
        [HttpPost]
        public ActionResult EditDeckName(Deck model)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            deckDAL.ModifyDeckName(model.DeckID, model.Name);
            Deck deck = deckDAL.GetDeckByDeckID(model.DeckID);

            return RedirectToAction(deck.DeckID, "Deck/EditDeck");
        }

        //Deck Tags
        [HttpPost]
        public ActionResult AddDeckTag(Deck model)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Deck curDeck = deckDAL.GetDeckByDeckID(model.DeckID);
            //if empty input is submitted
            if (model.TagName == null)
            {
                return View("EditDeck", curDeck);
            }
            //makes all tags lowercase to avoid conflicts
            model.TagName = model.TagName.ToLower();

            curDeck.AddTagToDeck(model.TagName);
            return RedirectToAction(curDeck.DeckID, "Deck/EditDeck");
        }

        public ActionResult RemoveDeckTag(string tagName, string deckID)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Deck curDeck = deckDAL.GetDeckByDeckID(deckID);
            curDeck.RemoveTagFromDeck(tagName);
            return RedirectToAction(curDeck.DeckID, "Deck/EditDeck");
        }

        //Remove Card
        [HttpGet]
        public ActionResult RemoveThisCard(int id)
        {

            string deckID = Session["deck_ID"].ToString();

            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            DeckSqlDAL dDAL = new DeckSqlDAL(connectionString);
            dDAL.RemoveCardFromDeck(id.ToString(), deckID);

            return RedirectToAction(deckID, "Deck/EditDeck");
        }

        //Add new deck
        [HttpGet]
        public ActionResult AddDeck()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View("NewDeck");
        }

        [HttpPost]
        public ActionResult AddDeck(string user_id, Deck model)
        {
            user_id = Session["userid"].ToString();
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            //user_id = CheckSession(user_id);
            if (Session["userid"] != null)
            {
                deckDAL.AddDeck(user_id, model.Name);
            }
            List<Deck> decks = deckDAL.GetDecks(user_id);
            return RedirectToAction("Index");
        }

        //begin study session
        public ActionResult BeginStudy(string deckID)
        {
            DeckSqlDAL dDal = new DeckSqlDAL(connectionString);
            Deck thisDeck = dDal.GetDeckByDeckID(deckID);
            return View("StudySession", thisDeck);
        }
      

        //private string CheckSession(string user_id)
        //{

        //    var currentUser = Session["user_id"];
        //    if (currentUser == null)
        //    {
        //        currentUser = "2";  //default for development
        //    }

        //    if (user_id == null)
        //    {
        //        //TODO redirect to login page if no user_id
        //        user_id = currentUser.ToString(); //for development purposes
        //    }

        //    return user_id;
        //}

    }
}