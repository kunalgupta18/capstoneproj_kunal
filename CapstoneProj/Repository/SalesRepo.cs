using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CapstoneProj.Repository
{
    public class SalesRepo : ISalesRepo
    {
        private readonly ISupplierRepo _supplierRepo;
        readonly string connectionString = "";

        public SalesRepo(ISupplierRepo supplierRepo)
        {
            _supplierRepo = supplierRepo;
            connectionString = "Data Source=APINP-ELPTIYMYO\\SQLEXPRESS;Initial Catalog=test;User ID=tap2023;Password=tap2023;Encrypt=False";
        }

        public IEnumerable<Sales> GetAllSales()
        {
            List<Sales> sales = new List<Sales>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM SalesOrders";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Sales sale = new Sales();
                    sale.SalesId = Convert.ToInt32(dr["SalesId"]);
                    sale.SaleDate = Convert.ToDateTime(dr["SaleDate"]);
                    sale.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                    sale.PaymentMode = dr["PaymentMode"].ToString();
                    sale.PaymentDetails = dr["PaymentDetails"].ToString();

                    sales.Add(sale);
                }
            }

            return sales;
        }

        public Sales GetSalesById(int salesId)
        {
            Sales sale = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM SalesOrders WHERE SalesId = {salesId}";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    sale = new Sales();
                    sale.SalesId = Convert.ToInt32(dr["SalesId"]);
                    sale.SaleDate = Convert.ToDateTime(dr["SaleDate"]);
                    sale.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                    sale.PaymentMode = dr["PaymentMode"].ToString();
                    sale.PaymentDetails = dr["PaymentDetails"].ToString();
                }
            }

            return sale;
        }

        public Sales AddSales(Sales sales)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO SalesOrders (SaleDate, TotalAmount, PaymentMode, PaymentDetails) " +
                               "VALUES (@SaleDate, @TotalAmount, @PaymentMode, @PaymentDetails);" +
                               "SELECT CAST(scope_identity() AS int)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SaleDate", sales.SaleDate);
                cmd.Parameters.AddWithValue("@TotalAmount", sales.TotalAmount);
                cmd.Parameters.AddWithValue("@PaymentMode", sales.PaymentMode);
                cmd.Parameters.AddWithValue("@PaymentDetails", sales.PaymentDetails);

                con.Open();
                sales.SalesId = (int)cmd.ExecuteScalar();
            }

            return sales;
        }

        public Sales UpdateSales(Sales sales)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE SalesOrders " +
                               "SET SaleDate = @SaleDate, TotalAmount = @TotalAmount, PaymentMode = @PaymentMode, " +
                               "PaymentDetails = @PaymentDetails " +
                               "WHERE SalesId = @SalesId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SaleDate", sales.SaleDate);
                cmd.Parameters.AddWithValue("@TotalAmount", sales.TotalAmount);
                cmd.Parameters.AddWithValue("@PaymentMode", sales.PaymentMode);
                cmd.Parameters.AddWithValue("@PaymentDetails", sales.PaymentDetails);
                cmd.Parameters.AddWithValue("@SalesId", sales.SalesId);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No records updated");
                }
            }

            return sales;
        }

        public Sales DeleteSales(int salesId)
        {
            Sales sales = GetSalesById(salesId);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"DELETE FROM SalesOrders WHERE SalesId = {salesId}";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            return sales;
        }
    }
}