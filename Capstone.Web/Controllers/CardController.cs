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
        private CardSqlDAL cDal = new CardSqlDAL(ConfigurationManager.ConnectionStrings["HotelFlashCardsDB"].ConnectionString);

        // GET: Card
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //create new card
        public ActionResult CardConstruct()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            TagsSqlDAL tDal = new TagsSqlDAL(connectionString);
            ViewBag.allTags = tDal.TagDictionary;
            return View("CardCreate");
        }

        //add created card
        public ActionResult CardSubmit(Card newCard)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            cDal.CreateCard(newCard, Session["userid"].ToString());

            List<Card> allCards = cDal.ViewCards(Session["userid"].ToString());

            return View("CardView", allCards);
        }

        //search cards by tag
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
                List<Card> matchingCards = cDal.SearchCard(searchString);

                return View("CardSearch", matchingCards);
            }
        }

        //view all cards
        public ActionResult CardView()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Card> allCards = cDal.ViewCards(Session["userid"].ToString());

            return View("CardView", allCards);
        }

        //modify cards
        public ActionResult CardModify(string id)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Card existingCard = cDal.GetCardByID(id);

            return View("CardModify", existingCard);
        }

        //card tags
        [HttpPost]
        public ActionResult AddCardTag(string cardID, string tagName)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Card currentCard = cDal.GetCardByID(cardID);
            currentCard.AddTagToCard(tagName);
            return RedirectToAction(currentCard.CardID, "Card/CardModify");
        }

        [HttpPost]
        public ActionResult RemoveCardTag(string cardID, string tagName)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Card currentCard = cDal.GetCardByID(cardID);
            currentCard.TagName = tagName;
            currentCard.RemoveTagFromCard(tagName);
            return RedirectToAction(currentCard.CardID, "Card/CardModify");
        }

        [HttpPost]
        public ActionResult CreateCardTag(Card model)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (string tag in model.AllTags)
            {
                if (tag == model.TagName)
                {
                    return RedirectToAction("CardView", "Card");
                }
            }
            CardSqlDAL cDAL = new CardSqlDAL(connectionString);
            Card curCard = cDAL.GetCardByID(model.CardID);
            curCard.AddTagToCard(model.TagName);
            return RedirectToAction(curCard.CardID, "Card/CardModify");
        }


        public ActionResult CardSubmitChange(string id, string front, string back, List<string> tags)
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Card currentCard = new Card();
            currentCard.CardID = id;
            currentCard.Front = front;
            currentCard.Back = back;
            currentCard.ThisCardTags = tags;
 
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