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

        public IActionResult Upsert(int? id)
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

            if(id == null || id == 0)
            {
                //Insert
                return View(productViewModel);
            }
            else
            {
                //Update
                productViewModel.Product = _unitOfWork.ProductRepository.Get(x=>x.Id==id);
                return View(productViewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
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
