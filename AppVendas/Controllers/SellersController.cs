using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppVendas.Services;
using Microsoft.AspNetCore.Mvc;
using AppVendas.Models;
using AppVendas.Models.ViewModels;

namespace AppVendas.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartamentService _departamentservice;

        public SellersController(SellerService sellerService,DepartamentService departamentService)
        {
            _sellerService = sellerService;
            _departamentservice = departamentService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departament = _departamentservice.FindAll();
            var viewModel = new SellerFormViewModel { Departaments = departament };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Saller saller)
        {
            _sellerService.Insert(saller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
            public IActionResult Edit(int?id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var obj = _sellerService.FindById(id.Value);

                if (obj == null)
                {
                    return NotFound();
                }
            List<Departament> departaments = _departamentservice.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Saller = obj, Departaments = departaments };
            return View(viewModel);

        }
    }
}