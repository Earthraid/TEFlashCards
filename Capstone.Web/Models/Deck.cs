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
        
        /// <summary>
        /// Returns a list of all available Tags.
        /// </summary>
        public List<string> AllTags
        {
            get
            {
                TagsSqlDAL tagSql = new TagsSqlDAL(connectionString);
                return tagSql.TagList;
            }
        }

        /// <summary>
        /// Returns the Tags associated with an individual deck in the current instance.
        /// </summary>
        public List<string> ThisDeckTags
        {
            get
            {
                TagsSqlDAL tagSql = new TagsSqlDAL(connectionString);
                return tagSql.GetTagsByDeckID(DeckID);
            }
        }
        
        /// <summary>
        /// Adds a Tag to an individual deck in a current instance.
        /// </summary>
        /// <param name="tagName"></param>
        public void AddTagToDeck(string tagName)
        {
            TagsSqlDAL tagsSql = new TagsSqlDAL(connectionString);
            tagsSql.AddTagToDeck(DeckID, tagName);
        }
    }
}