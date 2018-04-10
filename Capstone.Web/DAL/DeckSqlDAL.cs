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

        private string GetAllDecksSQL = "SELECT * FROM decks WHERE UserID = @userIDValue ORDER BY DeckID ASC";
        private string GetDecksByNameSQL = "SELECT * FROM decks WHERE UserID = @userIDValue and Name = @nameValue ORDER BY DeckID ASC";

        private string GetDecksByTagSQL = "SELECT * FROM decks " +
            "JOIN deck_tag ON decks.DeckID = deck_tag.DeckID " +
            "WHERE decks.UserID = @userIDValue and deck_tag.TagID = @tagValue ORDER BY decks.DeckID ASC";

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

                    var result = conn.Query<Deck>(GetAllDecksSQL, new { userIDValue = userID });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Deck> SearchDecksByName(string userID, string searchName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    var result = conn.Query<Deck>(GetDecksByNameSQL, new { userIDValue = userID, nameValue = searchName });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Deck> SearchDecksByTag(string userID, string searchName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    var result = conn.Query<Deck>(GetDecksByTagSQL, new { userIDValue = userID, tagValue = searchName });
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