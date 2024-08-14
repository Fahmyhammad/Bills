using bill_DataAccess.Data;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TypeController : Controller
    {
        private readonly IUnitOfWork _db;
        public TypeController(IUnitOfWork db)
        {
            _db = db;
        }
      
        public IActionResult Index()
        {
            var result =  _db.type.GetAll(IncludeWord : "Company").ToList();
            return View(result);
        }

        public IActionResult Create()
        {
            TypeViewModel TypeView = new TypeViewModel
            {
                type = new Types(),
                
                CompanyList = _db.company.GetAll().Select(x=> new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(TypeView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypeViewModel types)
        {
            if (types != null)
            {
                bool result = _db.type.TypeName(types.type);
                if (result)
                {
                    TempData["ErrorName"] = "The Type name already exists.";
                    return RedirectToAction(nameof(Index));
                }
                Types TypeModel = new Types
                {
                    TypeName = types.type.TypeName,
                    Notes = types.type.Notes,
                    CompanyId = types.type.CompanyId,
                };

                _db.type.Add(TypeModel);
                await _db.Complete();
                TempData["Create"] = "Create Type";
                return RedirectToAction(nameof(Index));
            }
            return View(types);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TypeViewModel TypeView = new TypeViewModel
            {
                type =  _db.type.GetById(x=>x.Id == id),

            CompanyList = _db.company.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(TypeView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TypeViewModel types)
        {
            if (types == null)
            {
                return View(types);
            }

            bool result =  _db.type.TypeName(types.type);
            if (result)
            {
                TempData["ErrorName"] = "The Type name already exists.";
                return RedirectToAction(nameof(Index));
            }

            var value =  _db.type.GetById(s => s.Id == types.type.Id);
            if (value != null)
            {
                value.TypeName = types.type.TypeName; 
                value.Notes = types.type.Notes; 
                value.CompanyId = types.type.CompanyId;
                                                  

                 _db.type.UpDate(value);
                await _db.Complete();
                TempData["Edit"] = "Edit Type";
                return RedirectToAction("Index");
            }

            return View(types);
        }

        public async Task <IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var value =  _db.type.GetById(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletetypes(int? id)
        {
            var result =  _db.type.GetById(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            _db.type.Delete(result);
            await _db.Complete();
            TempData["Delete"] = "Delete Type Successfull";
            return RedirectToAction("Index");
        }
    }
}
