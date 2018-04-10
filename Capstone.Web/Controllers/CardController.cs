using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class CardController : Controller
    {
        // GET: Card
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CardConstruct()
        {


            return View("CardCreate");
        }

        public ActionResult CardSubmit()
        {
            CardsSqlDAL cDal = new CardsSqlDAL();

            //cDal.CreateCard()

            return View("CardView");
        }

        public ActionResult CardSearch(string searchString)
        {
            CardsSqlDAL cDal = new CardsSqlDAL();

            //cDal.SearchCards()

            //List<Card> as model

            return View("CardSearch");
        }

        public ActionResult CardView()
        {
            CardsSqlDAL cDal = new CardsSqlDAL();

            //cDal.ViewAllCards

            //List<Card> as model

            return View();
        }

        public ActionResult CardModify()
        {
            CardsSqlDAL cDal = new CardsSqlDAL();


            //cDal.ModifyCard

            return View();
        }

        public ActionResult CardToDeck()
        {
            CardsSqlDAL cDal = new CardsSqlDAL();

            //cDal.AddCardToDeck

            return View("CardView");
        }
    }
}