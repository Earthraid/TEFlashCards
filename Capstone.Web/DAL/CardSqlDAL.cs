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

        private string view_cards = "SELECT Front, Back FROM [cards]";

        private string create_Card = "INSERT INTO [cards] (CardID, Front, Back)" +
           "VALUES (@cardid, @front, @back);";

        private string edit_Card = "INSERT INTO [cards] (Front, Back)" +
           "VALUES (@front, @back);";

        public CardSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Card> viewCards(int deckID)
        {
            List<Card> result = new List<Card>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(view_cards, conn);

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

        public bool createCard(Card card)
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

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return (result > 0);
        }

        public bool editCard(Card card)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(edit_Card, conn);
                    cmd.Parameters.AddWithValue("@front", card.Front);
                    cmd.Parameters.AddWithValue("@back", card.Back);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return (result > 0);
        }

        private Card ConvertFields(SqlDataReader reader)
        {
            Card card = new Card();
            card.CardID = Convert.ToInt32(reader["id"]);
            card.Front = Convert.ToString(reader["front"]);
            card.Back = Convert.ToString(reader["back"]);

            return card;
        }
    }
}