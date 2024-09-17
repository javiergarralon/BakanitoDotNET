﻿using CategoryCRUD.Data;
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
    }
}
