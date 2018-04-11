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

        private string GetDeckByDeckIDSQL = "SELECT * FROM decks WHERE DeckID = @deckIDValue ORDER BY DeckID ASC";

        private string GetDecksByNameSQL = "SELECT * FROM decks WHERE UserID = @userIDValue and Name = @nameValue ORDER BY DeckID ASC";

        private string GetDecksByTagSQL = "SELECT * FROM decks " +
            "JOIN deck_tag ON decks.DeckID = deck_tag.DeckID " +
            "WHERE decks.UserID = @userIDValue and deck_tag.TagID = @tagValue ORDER BY decks.DeckID ASC";

        private string AddDeckSQL = "INSERT INTO decks (UserID, Name) VALUES (@userIDValue, @nameValue); SELECT CAST(SCOPE_IDENTITY() as int);";

        private string ModifyDeckNameSQL = "UPDATE decks SET Name = @nameValue WHERE DeckID = @deckIDValue;";

        private string ModifyDeckPublicSQL = "UPDATE decks SET IsPublic = @publicValue WHERE DeckID = @deckIDValue;";

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

        public List<Deck> GetDeckByDeckID(string deckID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    var result = conn.Query<Deck>(GetDeckByDeckIDSQL, new { userIDValue = deckID });
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

        public string AddDeck(string userID, string deckName)
        {
            string newDeckID = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    var result = conn.ExecuteScalar<int>(AddDeckSQL, new { userIDValue = userID, nameValue = deckName });
                    if (result.ToString() != null)
                    {
                        newDeckID = result.ToString();
                    }

                    return newDeckID;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public bool ModifyDeckName(string deckID, string deckName)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            var result = conn.Query<int>(ModifyDeckNameSQL, new { deckIDValue = deckID, nameValue = deckName });
        //            if (result.Count() == 1)
        //            {
        //                return true;
        //            }
        //            else return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}