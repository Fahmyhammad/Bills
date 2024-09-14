using AutoMapper;
using bill_Entities.Const;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bills_SRS.Areas.Admin.Controllers
{
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Create(UnitViewModel unit)
        {
            if (unit != null)
            {
                bool result = _db.unitss.UnitsName(unit);
                if (result)
                {
                    TempData["ErrorName"] = "The company name already exists.";
                    return RedirectToAction(nameof(Index));
                }
                var entityData = _mapper.Map<Unit>(unit);

                _db.unitss.Add(entityData);
                await _db.Complete();
                TempData["Create"] = "Create Unit";
                return RedirectToAction("Index");
            }

            return View(unit);
        }
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Edit(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var value = _db.unitss.GetById(x => x.Id == id);
            if (value == null)
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

                    _mapper.Map(unit, value);

                    _db.unitss.UpDate(value);
                    await _db.Complete();
                    TempData["Edit"] = "Edit Unit";
                    return RedirectToAction("Index");
                }

            }

            return View(unit);
        }
        [Authorize(Roles = Constants.Admin)]
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


        }

    }
}