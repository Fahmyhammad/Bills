using bill_DataAccess.Data;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _db;
        public CompanyController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var result = _db.company.GetAll();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                bool result = _db.company.CompanyName(company);
                if (result)
                {
                    TempData["ErrorName"] = "The company name already exists.";
                    return RedirectToAction(nameof(Index));
                }
                _db.company.Add(company);
                await _db.Complete();
                TempData["Create"] = "Create Company";
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var value =  _db.company.GetById(x=>x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                bool result = _db.company.CompanyName(company);
                if (result)
                {
                    TempData["ErrorName"] = "The company name already exists.";
                    return RedirectToAction(nameof(Index));
                }

                var value = _db.company.GetById(s => s.Id ==  company.Id);
                if(value != null)
                {
                    value.Name = company.Name;
                    value.Notes = company.Notes;


                _db.company.UpDate(value);
                    await _db.Complete();
                TempData["Edit"] = "Edit Company";
                return RedirectToAction("Index");
                }
               
            }
            return View(company);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var value = _db.company.GetById(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComp(int? id)
        {
            var result =  _db.company.GetById(x=>x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            _db.company.Delete(result);
           await _db.Complete();
            TempData["Delete"] = "Delete Company Successfull";
            return RedirectToAction("Index");
        }
    }
}
