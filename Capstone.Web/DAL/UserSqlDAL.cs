﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class UserSqlDAL
    {

        private string connectionString;

        private string getUser = "SELECT userid, email, password, isadmin, username FROM [users];";

        private string registerUser = "INSERT INTO [users] (email, password, isadmin, username)" +
            "VALUES (@email, @password, @isadmin, @username);";

        public UserSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User GetUser(string email)
        {
            User result = new User();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(getUser, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmdExecuteReader();
                    while (reader.Read())
                    {
                        result = ConvertFields(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public bool Register(User user)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(registerUser, conn);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
                    cmd.Parameters.AddWithValue("@username", user.UserName);

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return (result > 0);
        }
    }
}