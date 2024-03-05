using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CapstoneProj.Repository
{
    public class SupplierRepo : ISupplierRepo
    {
        readonly string connectionString = "";

        public SupplierRepo()
        {
            connectionString = "Data Source=APINP-ELPTIYMYO\\SQLEXPRESS;Initial Catalog=test;User ID=tap2023;Password=tap2023;Encrypt=False";
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Suppliers";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Supplier supplier = new Supplier();
                    supplier.SupplierId = Convert.ToInt32(dr["SupplierId"]);
                    supplier.Name = dr["Name"].ToString();
                    supplier.ContactName = dr["ContactName"].ToString();
                    supplier.ContactEmail = dr["ContactEmail"].ToString();
                    supplier.ContactPhone = dr["ContactPhone"].ToString();
                    supplier.Address = dr["Address"].ToString();
                    supplier.PricingAgreement = decimal.Parse(dr["PricingAgreement"].ToString());

                    suppliers.Add(supplier);
                }
            }

            return suppliers;
        }

        public Supplier GetSupplierById(int id)
        {
            Supplier supplier = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Suppliers WHERE SupplierId = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    supplier = new Supplier();
                    supplier.SupplierId = Convert.ToInt32(dr["SupplierId"]);
                    supplier.Name = dr["Name"].ToString();
                    supplier.ContactName = dr["ContactName"].ToString();
                    supplier.ContactEmail = dr["ContactEmail"].ToString();
                    supplier.ContactPhone = dr["ContactPhone"].ToString();
                    supplier.Address = dr["Address"].ToString();
                    supplier.PricingAgreement = decimal.Parse(dr["PricingAgreement"].ToString());
                }
            }

            return supplier;
        }

        public Supplier AddSupplier(Supplier supplier)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Suppliers (Name, ContactName, ContactEmail, ContactPhone, Address, PricingAgreement) " +
                               "VALUES (@Name, @ContactName, @ContactEmail, @ContactPhone, @Address, @PricingAgreement);" +
                               "SELECT CAST(scope_identity() AS int)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", supplier.Name);
                cmd.Parameters.AddWithValue("@ContactName", supplier.ContactName);
                cmd.Parameters.AddWithValue("@ContactEmail", supplier.ContactEmail);
                cmd.Parameters.AddWithValue("@ContactPhone", supplier.ContactPhone);
                cmd.Parameters.AddWithValue("@Address", supplier.Address);
                cmd.Parameters.AddWithValue("@PricingAgreement", supplier.PricingAgreement);

                con.Open();
                supplier.SupplierId = (int)cmd.ExecuteScalar();
            }

            return supplier;
        }

        public Supplier UpdateSupplier(Supplier supplier)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Suppliers " +
                               "SET Name = @Name, ContactName = @ContactName, ContactEmail = @ContactEmail, " +
                               "ContactPhone = @ContactPhone, Address = @Address, PricingAgreement = @PricingAgreement " +
                               "WHERE SupplierId = @SupplierId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", supplier.Name);
                cmd.Parameters.AddWithValue("@ContactName", supplier.ContactName);
                cmd.Parameters.AddWithValue("@ContactEmail", supplier.ContactEmail);
                cmd.Parameters.AddWithValue("@ContactPhone", supplier.ContactPhone);
                cmd.Parameters.AddWithValue("@Address", supplier.Address);
                cmd.Parameters.AddWithValue("@PricingAgreement", supplier.PricingAgreement);
                cmd.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No records updated");
                }
            }

            return supplier;
        }

        public Supplier DeleteSupplier(int id)
        {
            Supplier supplier = GetSupplierById(id);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"DELETE FROM Suppliers WHERE SupplierId = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            return supplier;
        }
    }
}