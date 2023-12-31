using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace bestbrightness.Pages.Products
{
    public class TestingModel : PageModel
    {
        public List<ProductInfo> listProducts { get; set; }
        // Cart counter property
        public int CartCounter { get; set; } = 0;

        public void OnGet()
        {
            listProducts = new List<ProductInfo>();
            try
            {
                // Connect to the database and fetch product data
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
        public void IncrementCartCounter()
        {
            CartCounter++;
        }

        public void OnPost(int addItemToCart)
        {
            // Increment the cart counter when a product is added to the cart
            IncrementCartCounter();

            // Redirect back to the same page or to a shopping cart page
            RedirectToPage("/Products/Testing"); // Adjust the redirection as needed
        }

        // Your ProductInfo class goes here
    }
}
