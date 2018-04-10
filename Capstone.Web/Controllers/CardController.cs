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

        public ActionResult CardCreate()
        {
            CardsSqlDAL cDal = new CardsSqlDAL();
            //cDal.CreateCard()

            return View("CardView");
        }

        public ActionResult CardSearch(string searchString)
        {
            //cDal.SearchCards

            //List<Card> as model

            return View("CardSearch");
        }

        public ActionResult CardView()
        {
            //dal.ViewAllCards

            //List<Card> as model

            return View();
        }

        public ActionResult CardModify()
        {
            //dal.ModifyCard

            return View();
        }

        public ActionResult CardToDeck()
        {
            //dal.AddCardToDeck

            return View("CardView");
        }
    }
}