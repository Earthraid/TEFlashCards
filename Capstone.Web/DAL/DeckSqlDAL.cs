using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using Dapper;

namespace Capstone.Web.DAL
{
    public class DeckSqlDAL
    {
        private string connectionString;

        private string GetAllDecksSQL = "SELECT * FROM decks WHERE UserID = @userID ORDER BY DeckID, ASC";

        public DeckSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Deck> GetDecks(string userID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    var result = conn.Query<Deck>("SELECT * FROM decks WHERE UserID = @userIDValue ORDER BY DeckID ASC", new { userIDValue = userID });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            
        }
    }
}