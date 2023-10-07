using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bestbrightness.Pages.Products
{
    public class ProductsModel : PageModel
    {
        // List to store iterations of ProductInfo
        public List<ProductInfo> listProducts = new List<ProductInfo>();

        public void OnGet()
        {
            try
            {
                // connect to database
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Products";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo productInfo = new ProductInfo();
                                productInfo.id = "" + reader.GetInt32(0);
                                productInfo.item = reader.GetString(1);
                                productInfo.category = reader.GetString(2);
                                productInfo.price = reader.GetString(3);
                                productInfo.review = reader.GetString(4);
                                productInfo.created_at = reader.GetDateTime(5).ToString();

                                listProducts.Add(productInfo);
                            }
                        }
                    }
                }
            }catch (Exception ex) { 
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    // A new class to store information about 1 product
    public class ProductInfo
    {
        // variables to store data from database:
        public String id;
        public String item;
        public String category;
        public String price;
        public String review;
        public String created_at;
    }
}
