using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CapstoneProj.Repository
{
    public class ClientRepo : IClientRepo
    {
        readonly string _connectionString = "";

        public ClientRepo()
        {
            _connectionString = "Data Source=APINP-ELPTIYMYO\\SQLEXPRESS;Initial Catalog=test;User ID=tap2023;Password=tap2023;Encrypt=False";
        }

        public IEnumerable<Client> GetDetails()
        {
            List<Client> details = new List<Client>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id, firstname, lastname, role FROM users;";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Client client = new Client
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                        LastName = reader.GetString(reader.GetOrdinal("lastname")),
                        Role = reader.GetString(reader.GetOrdinal("role"))
                    };

                    details.Add(client);
                }
            }

            return details;
        }

        public Client GetClientById(int id)
        {
            Client client = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = $"SELECT * FROM users WHERE id = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    client = new Client();
                    client.Id = dr.GetInt32(dr.GetOrdinal("id"));
                    client.FirstName = dr.GetString(dr.GetOrdinal("firstname"));
                    client.LastName = dr.GetString(dr.GetOrdinal("lastname"));
                    client.Role = dr.GetString(dr.GetOrdinal("role"));
                }
            }

            return client;
        }

        public Client DeleteClient(int id)
        {
            Client client = GetClientById(id);

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = $"DELETE FROM users WHERE Id = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            return client;
        }
    }
}