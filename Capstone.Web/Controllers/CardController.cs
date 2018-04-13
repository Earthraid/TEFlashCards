﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Configuration;

namespace Capstone.Web.Controllers
{


    public class CardController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["HotelFlashCardsDB"].ConnectionString;

        // GET: Card
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CardConstruct()
        {

            return View("CardCreate");
        }


        public ActionResult CardSubmit(Card newCard)
        {
            //temporary userID
            string user_id = "2";

            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            cDal.CreateCard(newCard, user_id);

            List<Card> allCards = cDal.ViewCards(user_id);

            return View("CardView", allCards);
        }


        public ActionResult CardSearch(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return View("Index");
            }
            else
            {
                CardSqlDAL cDal = new CardSqlDAL(connectionString);
                List<Card> matchingCards = cDal.SearchCard(searchString);

                return View("CardSearch", matchingCards);
            }
        }


        public ActionResult CardView()
        {
            //temporary user ID
            string user_id = "2";

            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            List<Card> allCards = cDal.ViewCards(user_id);

            return View("CardView", allCards);
        }


        public ActionResult CardModify(string id)
        {
            Card cardToChange = new Card();
            cardToChange.CardID = id;

            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            Card existingCard = cDal.GetCardByID(id);

            //DAL doesn't return front and back with closing quotes
            ViewBag.cardFront = existingCard.Front;
            ViewBag.cardBack = existingCard.Back;
            ViewBag.Msg = "Message";

            return View("CardModify", cardToChange);
        }


        public ActionResult CardSubmitChange(string id, string front, string back)
        {
            Card currentCard = new Card();
            currentCard.CardID = id;
            currentCard.Front = front;
            currentCard.Back = back;
            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            cDal.EditCard(currentCard);

            //temporary user id
            string user_id = "2";
            List<Card> allCards = cDal.ViewCards(user_id);


            return View("CardView", allCards);
        }


        public ActionResult CardToDeck()
        {
            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            //cDal.AddCardToDeck

            return View("CardView");
        }
    }
}