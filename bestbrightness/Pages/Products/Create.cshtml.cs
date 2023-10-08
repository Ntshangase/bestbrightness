using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            // clear textFields
            productInfo.item = "";
            productInfo.category = "";
            productInfo.price = "";
            productInfo.review = "";
            successMessage = "New Product Added Successfuly";
        }
    }
}
