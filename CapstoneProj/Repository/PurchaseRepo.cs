using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CapstoneProj.Repository
{
    public class PurchaseRepo : IPurchaseRepo
    {
        readonly string connectionString = "";

        public PurchaseRepo()
        {
            connectionString = "Data Source=APINP-ELPTIYMYO\\SQLEXPRESS;Initial Catalog=test;User ID=tap2023;Password=tap2023;Encrypt=False";
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            List<Purchase> purchases = new List<Purchase>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PurchaseOrders";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Purchase purchase = new Purchase();
                    purchase.PurchaseId = Convert.ToInt32(dr["purchase_id"]);
                    purchase.PurchaseDate = Convert.ToDateTime(dr["purchase_date"]);
                    purchase.SupplierId = Convert.ToInt32(dr["supplier_id"]);
                    purchase.Quantity = Convert.ToInt32(dr["quantity"]);
                    purchase.TotalAmount = Convert.ToDecimal(dr["total_amount"]);

                    purchases.Add(purchase);
                }
            }

            return purchases;
        }

        public Purchase GetPurchaseById(int purchaseId)
        {
            Purchase purchase = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM PurchaseOrders WHERE purchase_id = {purchaseId}";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    purchase = new Purchase();
                    purchase.PurchaseId = Convert.ToInt32(dr["purchase_id"]);
                    purchase.PurchaseDate = Convert.ToDateTime(dr["purchase_date"]);
                    purchase.SupplierId = Convert.ToInt32(dr["supplier_id"]);
                    purchase.Quantity = Convert.ToInt32(dr["quantity"]);
                    purchase.TotalAmount = Convert.ToDecimal(dr["total_amount"]);
                }
            }

            return purchase;
        }

        public Purchase AddPurchase(Purchase purchase)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO PurchaseOrders (purchase_date, supplier_id, quantity, total_amount) " +
                               "VALUES (@PurchaseDate, @SupplierId, @Quantity, @TotalAmount);" +
                               "SELECT CAST(scope_identity() AS int)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);
                cmd.Parameters.AddWithValue("@SupplierId", purchase.SupplierId);
                cmd.Parameters.AddWithValue("@Quantity", purchase.Quantity);
                cmd.Parameters.AddWithValue("@TotalAmount", purchase.TotalAmount);

                con.Open();
                purchase.PurchaseId = (int)cmd.ExecuteScalar();
            }

            return purchase;
        }

        public Purchase UpdatePurchase(Purchase purchase)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE PurchaseOrders " +
                               "SET purchase_date = @PurchaseDate, supplier_id = @SupplierId, quantity = @Quantity, total_amount = @TotalAmount " +
                               "WHERE purchase_id = @PurchaseId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);
                cmd.Parameters.AddWithValue("@SupplierId", purchase.SupplierId);
                cmd.Parameters.AddWithValue("@Quantity", purchase.Quantity);
                cmd.Parameters.AddWithValue("@TotalAmount", purchase.TotalAmount);
                cmd.Parameters.AddWithValue("@PurchaseId", purchase.PurchaseId);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No records updated");
                }
            }

            return purchase;
        }

        public Purchase DeletePurchase(int purchaseId)
        {
            Purchase purchase = GetPurchaseById(purchaseId);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"DELETE FROM PurchaseOrders WHERE purchase_id = {purchaseId}";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            return purchase;
        }
    }
}