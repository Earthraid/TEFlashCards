using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class CardSqlDAL
    {
        private string connectionString;

        private string view_cards = "SELECT * FROM [cards] WHERE UserID = @user_id";

        private string get_card_by_id = "SELECT * FROM [cards] WHERE CardID = @card_id";

        private string view_cards_in_deck = "SELECT * FROM [cards] JOIN card_tag ON cards.CardID = card_tag.CardID JOIN tags on card_tag.TagID = tags.TagID " +
           "JOIN deck_tag ON tags.TagID = deck_tag.TagID JOIN decks ON deck_id WHERE card_deck.DeckID = decks.DeckID WHERE DeckID = @deck_id";

        private string create_Card = "INSERT INTO [cards] (Front, Back, UserID)" +
           "VALUES (@front, @back, @user_id);";

        private string edit_Card = "UPDATE [cards] SET Front = @front, Back = @back WHERE CardID = @id";

        private string search_Card = "SELECT * FROM[cards] JOIN card_tag ON cards.CardID = card_tag.CardID JOIN tags on card_tag.TagID = tags.TagID WHERE[TagName] = @TagName";

        public CardSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //List all cards that a user has
        public List<Card> ViewCards(string userID)
        {
            List<Card> result = new List<Card>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(view_cards, conn);
                    cmd.Parameters.AddWithValue("@user_id", userID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(ConvertFields(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        //List all cards in a deck
        public List<Card> ViewCardsInDeck(string deckID)
        {
            List<Card> result = new List<Card>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(view_cards_in_deck, conn);
                    cmd.Parameters.AddWithValue("deck_id", deckID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(ConvertFields(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public bool CreateCard(Card card, string user_id)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(create_Card, conn);
                    cmd.Parameters.AddWithValue("@front", card.Front);
                    cmd.Parameters.AddWithValue("@back", card.Back);
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return (result > 0);
        }

        public bool EditCard(Card currentCard)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(get_card_by_id, conn);
                    cmd.Parameters.AddWithValue("@card_id", currentCard.CardID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        currentCard.UserID = Convert.ToString(reader["UserID"]);
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(edit_Card, conn);
                    cmd.Parameters.AddWithValue("@front", currentCard.Front);
                    cmd.Parameters.AddWithValue("@back", currentCard.Back);
                    cmd.Parameters.AddWithValue("@id", currentCard.CardID);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return (result > 0);
        }


        public List<Card> SearchCard(string tagName)
        {
            List<Card> matchingCards = new List<Card>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(search_Card, conn);

                    cmd.Parameters.AddWithValue("@TagName", tagName);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        matchingCards.Add(ConvertFields(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return matchingCards;
        }



        private Card ConvertFields(SqlDataReader reader)
        {
            Card card = new Card();
            card.CardID = Convert.ToString(reader["CardID"]);
            card.Front = Convert.ToString(reader["Front"]);
            card.Back = Convert.ToString(reader["Back"]);

            return card;
        }
    }
}