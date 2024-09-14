<<<<<<< HEAD
﻿using AutoMapper;
using bill_DataAccess.Data;
using bill_Entities.Const;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
=======
﻿using bill_DataAccess.Data;
using bill_Entities.Models;
using bill_Entities.Repoistory;
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bills_SRS.Areas.Admin.Controllers
{
<<<<<<< HEAD
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
=======
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

>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Create(CompanyViewModel company)
=======
        public async Task<IActionResult> Create(Company company)
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        {
            if (ModelState.IsValid)
            {
                bool result = _db.company.CompanyName(company);
                if (result)
                {
                    TempData["ErrorName"] = "The company name already exists.";
                    return RedirectToAction(nameof(Index));
                }
<<<<<<< HEAD

                var companyModle = _mapper.Map<Company>(company);

                _db.company.Add(companyModle);
=======
                _db.company.Add(company);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
                await _db.Complete();
                TempData["Create"] = "Create Company";
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]
=======

>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
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
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Edit(CompanyViewModel company)
=======
        public async Task<IActionResult> Edit(Company company)
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
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
<<<<<<< HEAD
                if (value != null)
                {
                    _mapper.Map(company , value);

                    _db.company.UpDate(value);
=======
                if(value != null)
                {
                    value.Name = company.Name;
                    value.Notes = company.Notes;


                _db.company.UpDate(value);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
                    await _db.Complete();
                TempData["Edit"] = "Edit Company";
                return RedirectToAction("Index");
                }
               
            }
            return View(company);
        }
<<<<<<< HEAD
        [Authorize(Roles = Constants.Admin)]
=======

>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
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
<<<<<<< HEAD
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

=======
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
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        }
    }
}
