using System;
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
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public ActionResult CardConstruct()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("CardCreate");
        }

        public ActionResult CardSubmit(Card newCard, string user_id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CardSqlDAL cDal = new CardSqlDAL(connectionString);

            if (!string.IsNullOrEmpty(newCard.TempDeckNum))
            {
                cDal.CreateCard(newCard, user_id);
                List<Card> cardsInDeck = cDal.ViewCardsInDeck(newCard.TempDeckNum);
                return RedirectToAction("Index","Deck");
            }
            else
            {
                cDal.CreateCard(newCard, user_id);
                List<Card> allCards = cDal.ViewCards(Session["user_id"].ToString());
                return View("CardView", allCards);
            }
        }


        public ActionResult CardSearch(string searchString)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

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
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            List<Card> allCards = cDal.ViewCards(Session["userid"].ToString());

            return View("CardView", allCards);
        }


        public ActionResult CardModify(string id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            Card existingCard = cDal.GetCardByID(id);


            return View("CardModify", existingCard);
        }


        public ActionResult CardSubmitChange(string id, string front, string back)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Card currentCard = new Card();
            currentCard.CardID = id;
            currentCard.Front = front;
            currentCard.Back = back;
            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            cDal.EditCard(currentCard);

            List<Card> allCards = cDal.ViewCards(Session["userid"].ToString());


            return View("CardView", allCards);
        }


        public ActionResult CardToDeck()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            //cDal.AddCardToDeck

            return View("CardView");
        }
    }
}