using AutoMapper;
using bill_DataAccess.Data;
using bill_Entities.Const;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area(Constants.Admin)]
    public class CompanyController(IUnitOfWork db, IMapper mapper) : Controller
    {
        private readonly IUnitOfWork _db = db;
        private readonly IMapper _mapper = mapper;

        [AllowAnonymous]
        public IActionResult Index()
        {
            var companies = _db.company.GetAll();
            var result = _mapper.Map<IEnumerable<CompanyViewModel>>(companies);
            return View(result);
        }
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Create(CompanyViewModel company)
        {
            if (ModelState.IsValid)
            {
                bool result = _db.company.CompanyName(company);
                if (result)
                {
                    TempData["ErrorName"] = "The company name already exists.";
                    return RedirectToAction(nameof(Index));
                }

                var companyModle = _mapper.Map<Company>(company);

                _db.company.Add(companyModle);
                await _db.Complete();
                TempData["Create"] = "Create Company";
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }
        [Authorize(Roles = "Admin,Editor")]
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
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Edit(CompanyViewModel company)
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
                if (value != null)
                {
                    _mapper.Map(company , value);

                    _db.company.UpDate(value);
                    await _db.Complete();
                TempData["Edit"] = "Edit Company";
                return RedirectToAction("Index");
                }
               
            }
            return View(company);
        }
        [Authorize(Roles = Constants.Admin)]
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
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteComp(int? id)
        {
            try
            {
                var result = _db.company.GetById(x => x.Id == id);
                if (result == null)
                {
                    return NotFound();
                }
                _db.company.Delete(result);
                await _db.Complete();
                TempData["Delete"] = "Delete Company Successfull";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = $"Error deleting company: {ex.Message}";
                return RedirectToAction("Index");

            }

        }
    }
}
