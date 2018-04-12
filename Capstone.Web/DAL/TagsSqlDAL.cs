using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Linq;
using System.Web;


namespace Capstone.Web.DAL
{
    public class TagsSqlDAL
    {
        private string connectionString;
        private string GetAllTagsSQL = "SELECT * FROM tags";

        public TagsSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Dictionary<string, string> GetAllTags()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(GetAllTagsSQL, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string resultKey = Convert.ToString(reader["TagID"]);
                        string resultValue = Convert.ToString(reader["TagName"]);

                        result.Add(resultKey, resultValue);
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SelectListItem> TagList
        {
            get
            {
                //TagsSqlDAL tagDAL = new TagsSqlDAL(ConfigurationManager.ConnectionStrings["HotelFlashcardsDB"].ConnectionString);
                Dictionary<string, string> tags = GetAllTags();

                List<SelectListItem> tagList = new List<SelectListItem>();

                foreach (KeyValuePair<string, string> item in tags)
                {
                    tagList.Add(new SelectListItem { Text = item.Value, Value = item.Key });
                }

                return tagList;
            }

        }
    }

}