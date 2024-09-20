using AutoMapper;
using bill_DataAccess.Data;
using bill_Entities.Const;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area(Constants.Admin)]
    public class TypeController(IUnitOfWork db, IMapper mapper) : Controller
    {
        private readonly IUnitOfWork _db = db;
        private readonly IMapper _mapper = mapper;


        [AllowAnonymous]
        public IActionResult Index()
        {
            var result = _db.type.GetAll(IncludeWord: "Company");
            var allData = _mapper.Map<IEnumerable<TypeViewModel>>(result);
            return View(allData);
        }
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Create()
        {
            TypeViewModel TypeView = new TypeViewModel
            {
                type = _mapper.Map<Types>(new Types()),

            };
            return View(TypeView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Create(TypeViewModel types)
        {
            if (types != null)
            {
                bool result = _db.type.TypeName(types);
                if (result)
                {
                    TempData["ErrorName"] = "The Type name already exists.";
                    return RedirectToAction(nameof(Create));
                }

                var EntityData = _mapper.Map<Types>(types);

                _db.type.Add(EntityData);
                await _db.Complete();
                TempData["Create"] = "Create Type";
                return RedirectToAction(nameof(Index));
            }
            return View(types);
        }
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var Type = _db.type.GetById(x => x.Id == id);
            if (Type == null)
                return NotFound();

            var TypeView = _mapper.Map<TypeViewModel>(Type);


            return View(TypeView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Edit(TypeViewModel types)
        {
            if (types == null)
            {
                return View(types);
            }

            bool result = _db.type.TypeName(types);
            if (result)
            {
                TempData["ErrorName"] = "The Type name already exists.";
                return RedirectToAction(nameof(Edit));
            }

            var value = _db.type.GetById(s => s.Id == types.Id);
            if (value != null)
            {

                _mapper.Map(types, value);

                _db.type.UpDate(value);
                await _db.Complete();
                TempData["Edit"] = "Edit Type";
                return RedirectToAction("Index");
            }

            return View(types);
        }
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var value = _db.type.GetById(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Deletetypes(int? id)
        {
            try
            {
                var result = _db.type.GetById(x => x.Id == id);
                if (result == null)
                {
                    return NotFound();
                }
                _db.type.Delete(result);
                await _db.Complete();
                TempData["Delete"] = "Delete Type Successfull";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting company: {ex.Message}";
                return RedirectToAction("Delete");
            }

        }
    }
}