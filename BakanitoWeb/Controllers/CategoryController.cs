using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Bakanito.DataAccess.Data;
using Bakanito.Models.Models;
using Bakanito.DataAccess.Repository.IRepository;

namespace BakanitoWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepository = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepository.GetAll().ToList();
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
                _categoryRepository.Add(category);
                _categoryRepository.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(category);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categorySelected = _categoryRepository.Get(x => x.Id == id);
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
                _categoryRepository.Update(category);
                _categoryRepository.Save();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
            }
            return View(category);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categorySelected = _categoryRepository.Get(x => x.Id == id);

            if (categorySelected == null)
            {
                return NotFound();
            }

            return View(categorySelected);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categorySelected = _categoryRepository.Get(x => x.Id == id);

            if (categorySelected == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(categorySelected);
            _categoryRepository.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");


        }
    }
}
