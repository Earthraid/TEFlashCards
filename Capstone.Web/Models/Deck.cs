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
        private string connectionString = ConfigurationManager.ConnectionStrings["HotelFlashCardsDB"].ConnectionString;

        public string DeckID { get; set; }

        public string UserID { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }


        public List<Card> DeckCards(string deck_id)
        {
            CardSqlDAL c = new CardSqlDAL(connectionString);
            List<Card> cards = c.ViewCardsInDeck(deck_id);

            return cards;

        }
    }
}