using CategoryCRUD.Data;
using CategoryCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace CategoryCRUD.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            //Custom validations
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            } 

            //if(category.Name != null && category.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value.");
            //}

            if (ModelState.IsValid) { 
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
            
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            Category? categorySelected = _db.Categories.Find(id);
            //Category? categorySelected2 = _db.Categories.FirstOrDefault(x=>x.Id==id);
            //Category? categorySelected3 = _db.Categories.Where(x=>x.Id==id).FirstOrDefault();

            if (categorySelected == null)
            {
                return NotFound();
            }

            return View(categorySelected);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            //Custom validations
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            //if(category.Name != null && category.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value.");
            //}

            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);

        }
    }
}
