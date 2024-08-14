using bill_Entities.Models;
using bill_Entities.Repoistory;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bills_SRS.Areas.Admin.Controllers
{
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Unit unit)
        {
            if (unit != null)
            {
                bool result = _db.unitss.UnitsName(unit);
                if (result)
                {
                    TempData["ErrorName"] = "The company name already exists.";
                    return RedirectToAction(nameof(Index));
                }
                _db.unitss.Add(unit);
                await _db.Complete();
                TempData["Create"] = "Create Unit";
                return RedirectToAction("Index");
            }

            return View(unit);
        }

        public IActionResult Edit(int id)
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
        public async Task<IActionResult> Edit(Unit unit)
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


                    _db.unitss.UpDate(value);
                    await _db.Complete();
                    TempData["Edit"] = "Edit Unit";
                    return RedirectToAction("Index");
                }

            }

            return View(unit);
        }
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

        }

    }
}
