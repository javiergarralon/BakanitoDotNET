using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Bakanito.DataAccess.Data;
using Bakanito.Models.Models;
using Bakanito.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bakanito.Models.ViewModels;

namespace BakanitoWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.ProductRepository.GetAll().ToList();              
            return View(objProductList);
        }

        public IActionResult Create()
        {

            ProductViewModel productViewModel = new()
            {
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Product = new Product()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Add(productViewModel.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productViewModel.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                return View(productViewModel);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? ProductSelected = _unitOfWork.ProductRepository.Get(x => x.Id == id);
            //Product? ProductSelected2 = _db.products.FirstOrDefault(x=>x.Id==id);
            //Product? ProductSelected3 = _db.products.Where(x=>x.Id==id).FirstOrDefault();

            if (ProductSelected == null)
            {
                return NotFound();
            }

            return View(ProductSelected);
        }

        [HttpPost]
        public IActionResult Edit(Product Product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Update(Product);
                _unitOfWork.Save();
                TempData["success"] = "Product edited successfully";
                return RedirectToAction("Index");
            }
            return View(Product);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? ProductSelected = _unitOfWork.ProductRepository.Get(x => x.Id == id);

            if (ProductSelected == null)
            {
                return NotFound();
            }

            return View(ProductSelected);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? ProductSelected = _unitOfWork.ProductRepository.Get(x => x.Id == id);

            if (ProductSelected == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepository.Delete(ProductSelected);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");


        }
    }
}
