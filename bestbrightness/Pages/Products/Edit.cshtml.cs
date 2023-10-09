using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bestbrightness.Pages.Products
{
    public class EditModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Products Where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                productInfo.id = "" + reader.GetInt32(0);
                                productInfo.item = reader.GetString(1);
                                productInfo.category = reader.GetString(2);
                                productInfo.price = reader.GetString(3);
                                productInfo.review = reader.GetString(4);

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }

        public void OnPost()
        {
            productInfo.id = Request.Form["id"];
            productInfo.item = Request.Form["item"];
            productInfo.category = Request.Form["category"];
            productInfo.price = Request.Form["price"];
            productInfo.review = Request.Form["review"];

            if (productInfo.item.Length == 0 || productInfo.category.Length == 0 ||
                productInfo.price.Length == 0 || productInfo.review.Length == 0)
            {
                errorMessage = "All Fields Required";
                return;
            }

            // insert data to database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // create SQL Query
                    String sql = "Update products " + "SET item=@item, category=@category, price=@price review=@review" + "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@item", productInfo.item);
                        command.Parameters.AddWithValue("@category", productInfo.category);
                        command.Parameters.AddWithValue("@price", productInfo.price);
                        command.Parameters.AddWithValue("@review", productInfo.review);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            //redirect user if succesfully added product
            Response.Redirect("Products/Products");

        }
    }
}
