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

        private string getUser = "SELECT UserID, Email, Password, IsAdmin, UserName FROM [users] WHERE Email = @email;";

        private string registerUser = "INSERT INTO [users] (Email, Password, IsAdmin, UserName)" +
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
                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        
                            result.Email = Convert.ToString(reader["Email"]).Trim();
                            result.Password = Convert.ToString(reader["Password"]).Trim();
                            result.Id = Convert.ToInt32(reader["UserId"]);
                            result.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                            result.UserName = Convert.ToString(reader["UserName"]).Trim();
                       
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //add message to tell user that email already exists
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

        private User ConvertFields(SqlDataReader reader)
        {
            User user = new User();
            user.Id = Convert.ToInt32(reader["UserID"]);
            user.Email = Convert.ToString(reader["Email"]);
            user.Password = Convert.ToString(reader["Password"]);
            user.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
            user.UserName = Convert.ToString(reader["UserName"]);
            
            return user;
        }
    }
}