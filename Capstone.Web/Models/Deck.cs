using System;
using System.Collections.Generic;

using System.Configuration;
using Capstone.Web.DAL;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Deck
    {
        public string DeckID { get; set; }

        public string UserID { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

    }
}