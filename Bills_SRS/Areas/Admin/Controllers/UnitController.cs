<<<<<<< HEAD
﻿using AutoMapper;
using bill_Entities.Const;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
=======
﻿using bill_Entities.Models;
using bill_Entities.Repoistory;
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bills_SRS.Areas.Admin.Controllers
{
<<<<<<< HEAD
    [Area(Constants.Admin)]
    public class UnitController(IUnitOfWork db, IMapper mapper) : Controller
    {
        private readonly IUnitOfWork _db = db;
        private readonly IMapper _mapper = mapper;


        [AllowAnonymous]
        public IActionResult Index()
        {
            var result = _db.unitss.GetAll();
            var allData = _mapper.Map<IEnumerable<UnitViewModel>>(result);
            return View(allData);
        }
        [Authorize(Roles = "Admin,Editor")]
=======
    [Area("Admin")]
    public class UnitController : Controller
    {
        private readonly IUnitOfWork _db;
        public UnitController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var result = _db.unitss.GetAll();
            return View(result);
        }
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Create(UnitViewModel unit)
=======
        public async Task<IActionResult> Create(Unit unit)
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        {
            if (unit != null)
            {
                bool result = _db.unitss.UnitsName(unit);
                if (result)
                {
                    TempData["ErrorName"] = "The company name already exists.";
                    return RedirectToAction(nameof(Index));
                }
<<<<<<< HEAD
                var entityData = _mapper.Map<Unit>(unit);

                _db.unitss.Add(entityData);
=======
                _db.unitss.Add(unit);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
                await _db.Complete();
                TempData["Create"] = "Create Unit";
                return RedirectToAction("Index");
            }

            return View(unit);
        }
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]
=======

>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public IActionResult Edit(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var value = _db.unitss.GetById(x => x.Id == id);
<<<<<<< HEAD
            if(value == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<UnitViewModel>(value);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Edit(UnitViewModel unit)
=======
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Unit unit)
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        {
            if (unit != null)
            {
                bool result = _db.unitss.UnitsName(unit);
                if (result)
                {
                    TempData["ErrorName"] = "The company name already exists.";
                    return RedirectToAction(nameof(Index));
                }

                var value = _db.unitss.GetById(x => x.Id == unit.Id);
                if (value != null)
                {
                    value.UnitName = unit.UnitName;
                    value.Notes = unit.Notes;

<<<<<<< HEAD
                    _mapper.Map(unit, value);
=======
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183

                    _db.unitss.UpDate(value);
                    await _db.Complete();
                    TempData["Edit"] = "Edit Unit";
                    return RedirectToAction("Index");
                }

            }

            return View(unit);
        }
<<<<<<< HEAD
        [Authorize(Roles = Constants.Admin)]
=======
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public IActionResult Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var value = _db.unitss.GetById(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            try
            {
                var value = _db.unitss.GetById(x => x.Id == id);
                if (value == null)
                {
                    return NotFound();
                }
                _db.unitss.Delete(value);
                await _db.Complete();
                TempData["Delete"] = "Delete Unit";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting company: {ex.Message}";
                return RedirectToAction("Index");
            }
            
=======
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var value = _db.unitss.GetById(x => x.Id == id);
            if (value == null)
            {
                return NotFound();
            }
            _db.unitss.Delete(value);
            await _db.Complete();
            TempData["Delete"] = "Delete Unit";
            return RedirectToAction("Index");
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183

        }

    }
}
