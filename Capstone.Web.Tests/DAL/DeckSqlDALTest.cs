using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Collections.Generic;


namespace Capstone.Web.Tests.DAL
{
    [TestClass]
    public class DeckSqlDALTest
    {
        private TransactionScope tran;

        private string connectionString = ConfigurationManager.ConnectionStrings["HotelFlashCardsDB"].ConnectionString;
        private int numDecks = 0;
        private int deckID = 0;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize a new transaction scope. This automatically begins the transaction.
            tran = new TransactionScope();

            // Open a SqlConnection object using the active transaction
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand(@"SELECT COUNT(*) FROM decks", conn);
                numDecks = (int)cmd.ExecuteScalar();

                //Insert a Dummy Record for Deck
                // SELECT CAST(SCOPE_IDENTITY() as int) as a work-around
                // This will get the newest identity value generated for the record most recently inserted
                cmd = new SqlCommand(@"INSERT INTO decks ([UserID], [Name], [IsPublic]) VALUES ('2', 'SQL Test', '1');SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                deckID = (int)cmd.ExecuteScalar();

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
            //tran.Complete();
        }

        [TestMethod]
        public void GetDecksTest()
        {
            //Arrange
            DeckSqlDAL deckSql = new DeckSqlDAL(connectionString);

            //Act
            List<Deck> deckList = deckSql.GetDecks("2");

            //Assert
            Assert.AreEqual(numDecks + 1, deckList.Count);
        }
    }
}
