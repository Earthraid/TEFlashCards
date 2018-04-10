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
            CardSqlDAL cDal = new CardSqlDAL(connectionString);

            //cDal.CreateCard(newCard);
            //CardSqlDAL throws error: missing cardid

            return View("CardView");
        }

        public ActionResult CardSearch(string searchString)
        {
            CardSqlDAL cDal = new CardSqlDAL(connectionString);

            //cDal.SearchCards()

            //List<Card> as model

            return View("CardSearch");
        }

        public ActionResult CardView()
        {
            CardSqlDAL cDal = new CardSqlDAL(connectionString);

            //List<Card> allCards = cDal.ViewCards(int userID);

            //List<Card> as model

            return View();
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