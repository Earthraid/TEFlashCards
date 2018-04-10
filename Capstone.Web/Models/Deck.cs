using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Deck
    {
        public int DeckID { get; set; }

        public int UserID { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

    }
}