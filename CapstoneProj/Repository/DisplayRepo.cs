using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using System.Data.SqlClient;

namespace CapstoneProj.Repository
{
    public class DisplayRepo : IDisplayRepo
    {
        readonly string _connectionString = "";

        public DisplayRepo()
        {
            _connectionString = "Data Source=APINP-ELPTIYMYO\\SQLEXPRESS;Initial Catalog=test;User ID=tap2023;Password=tap2023;Encrypt=False";
        }

        public SalesAndPurchaseSummary GetSummary()
        {
            SalesAndPurchaseSummary summary = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TotalSalesQuantity, TotalPurchasesQuantity, TotalSalesAmount, TotalPurchaseAmount FROM dashboard";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    summary = new SalesAndPurchaseSummary
                    {
                        TotalSalesQuantity = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        TotalPurchasesQuantity = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        TotalSalesAmount = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                        TotalPurchaseAmount = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3)
                    };
                }
            }

            return summary;
        }
    }
}
