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
            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            List<Card> matchingCards = cDal.SearchCard(searchString);

            return View("CardSearch", matchingCards);
        }

        public ActionResult CardView()
        {
            //temporary user ID
            string user_id = "2";

            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            List<Card> allCards = cDal.ViewCards(user_id);

            return View("CardView", allCards);
        }

        public ActionResult CardModify()
        {
            CardSqlDAL cDal = new CardSqlDAL(connectionString);


            //cDal.EditCard

            return View();
        }

        public ActionResult CardToDeck()
        {
            CardSqlDAL cDal = new CardSqlDAL(connectionString);
            //cDal.AddCardToDeck

            return View("CardView");
        }
    }
}