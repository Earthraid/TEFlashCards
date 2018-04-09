using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class DeckController : Controller
    {
        // GET: Deck
        public ActionResult Deck()
        {
            return View("DeckView");
        }
    }
}