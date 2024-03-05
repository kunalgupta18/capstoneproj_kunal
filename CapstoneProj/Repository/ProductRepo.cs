using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using System.Data.SqlClient;
namespace CapstoneProj.Repository
{
    public class ProductRepo : IProductRepo
    {
        readonly string connectionString = "";

        public ProductRepo()
        {
            connectionString = "Data Source=APINP-ELPTIYMYO\\SQLEXPRESS;Initial Catalog=test;User ID=tap2023;Password=tap2023;Encrypt=False";
        }

        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(dr["Id"]);
                    product.Name = dr["Name"].ToString();
                    product.Description = dr["Description"].ToString();
                    product.SKU = dr["SKU"].ToString();
                    product.Category = dr["Category"].ToString();
                    product.Manufacturer = dr["Manufacturer"].ToString();
                    product.Price = decimal.Parse(dr["Price"].ToString());
                    product.Quantity = int.Parse(dr["Quantity"].ToString());

                    products.Add(product);
                }
            }

            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Products WHERE Id = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    product = new Product();
                    product.Id = Convert.ToInt32(dr["Id"]);
                    product.Name = dr["Name"].ToString();
                    product.Description = dr["Description"].ToString();
                    product.SKU = dr["SKU"].ToString();
                    product.Category = dr["Category"].ToString();
                    product.Manufacturer = dr["Manufacturer"].ToString();
                    product.Price = decimal.Parse(dr["Price"].ToString());
                    product.Quantity = int.Parse(dr["Quantity"].ToString());
                }
            }

            return product;
        }

            public Product AddProduct(Product product)
            {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (Name, Description, SKU, Category, Manufacturer, Price, Quantity) " +
                                "VALUES (@Name, @Description, @SKU, @Category, @Manufacturer, @Price, @Quantity)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@SKU", product.SKU);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return product;
        }

        public Product UpdateProduct(Product product)
        {


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Products " +
                               "SET Name = @Name, Description = @Description, SKU = @SKU, Category = @Category, " +
                               "Manufacturer = @Manufacturer, Price = @Price, Quantity = @Quantity" +
                               "WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@SKU", product.SKU);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                cmd.Parameters.AddWithValue("@Id", product.Id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No records updated");
                }
            }

            return product;
        }


        public Product DeleteProduct(int id)
        {
            Product product = GetProductById(id);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"DELETE FROM Products WHERE Id = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            return product;
        }
    }
}

