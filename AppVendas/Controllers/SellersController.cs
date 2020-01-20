using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppVendas.Services;
using Microsoft.AspNetCore.Mvc;
using AppVendas.Models;
using AppVendas.Models.ViewModels;
using AppVendas.Services.Exceptions;
using System.Diagnostics;

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
        public async Task<IActionResult> Index()
        {
            var list = await  _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task <IActionResult> Create()
        {
            var departament = await _departamentservice.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departaments = departament };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Saller saller)
        {
            if (!ModelState.IsValid)
            {
                var departaments = await _departamentservice.FindAllAsync();
                var viewModel = new SellerFormViewModel { Saller = saller, Departaments = departaments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(saller);
            return RedirectToAction(nameof(Index));
        }

        public async Task <IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),new { message = "Id não existente"});
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existente" });
            }

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existente" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não existente" });
            }

            return View(obj);
        }
            public async  Task<IActionResult> Edit(int?id)
            {
                if (id == null)
                {
                return RedirectToAction(nameof(Error), new { message = "Id não existente" });
            }

                var obj = await _sellerService.FindByIdAsync(id.Value);

                if (obj == null)
                {
                return RedirectToAction(nameof(Error),new { message = "Id não existente" });
            }
            List<Departament> departaments =await  _departamentservice.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Saller = obj, Departaments = departaments };
            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Saller saller)
        {
            if (!ModelState.IsValid)
            {
                var departaments = await _departamentservice.FindAllAsync();
                var viewModel = new SellerFormViewModel { Saller = saller, Departaments = departaments };
                return View(viewModel);
            }
            if (id != saller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não corresponde" });
            }
            try
            {


                await _sellerService.UpdateAsync(saller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}