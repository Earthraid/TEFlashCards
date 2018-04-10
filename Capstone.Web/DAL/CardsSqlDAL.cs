using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class CardsSqlDAL 
    {
        private string connectionString;

        private string createCard = "INSERT INTO [cards] (cardid, front, back)" +
           "VALUES (@cardid, @front, @back);";

        private string editCard = "INSERT INTO [cards] (front, back)" +
           "VALUES (@front, @back);";

        private string viewCards = "SELECT front, back FROM [cards]";

        public UserSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public
    }
}