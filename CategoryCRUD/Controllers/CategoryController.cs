using Microsoft.AspNetCore.Mvc;

namespace CategoryCRUD.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
