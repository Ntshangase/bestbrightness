using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace bestbrightness.Pages.Products
{
    public class CreateModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
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
                    String sql = "INSERT INTO products" + "(item, category, price, review) VALUES" + "(@item, @category, @price, @review);";

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
            catch(Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            // clear textFields
            productInfo.item = "";
            productInfo.category = "";
            productInfo.price = "";
            productInfo.review = "";
            successMessage = "New Product Added Successfuly";

            //redirect user if succesfully added product
            Response.Redirect("Products/Products");
        }
    }
}
