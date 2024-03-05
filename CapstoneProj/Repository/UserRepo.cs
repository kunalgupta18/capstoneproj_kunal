using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace CapstoneProj.Repository
{
    public class UserRepo : IUserRepo
    {
        readonly string connectionString = "";

        public UserRepo()
        {
            connectionString = "Data Source=APINP-ELPTIYMYO\\SQLEXPRESS;Initial Catalog=test;User ID=tap2023;Password=tap2023;Encrypt=False";
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the password string to byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a string representation
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public Users GetUser(string username, string password)
        {
            Users user = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string hashedPassword = dr["Password"].ToString();

                    if (HashPassword(password) == hashedPassword)
                    {
                        user = new Users();
                        user.Username = dr["Username"].ToString();
                        user.Password = hashedPassword;

                        

                        // Assign FirstName and LastName only if they are available
                        if (dr["FirstName"] != DBNull.Value)
                        {
                            user.FirstName = dr["FirstName"].ToString();
                        }
                        if (dr["LastName"] != DBNull.Value)
                        {
                            user.LastName = dr["LastName"].ToString();
                        }
                        if (dr["Role"] != DBNull.Value)
                        {
                            user.Role = dr["Role"].ToString();
                        }
                    }
                }
            }

            return user;
        }

        public Users AddUser(Users user)
        {
            string hashedPassword = HashPassword(user.Password);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, Password, FirstName, LastName, Role) VALUES (@Username, @Password, @FirstName, @LastName, @Role)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Role",user.Role);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return user;
        }


    }
}