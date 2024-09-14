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
using bill_Entities.ViewModel;
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bills_SRS.Areas.Admin.Controllers
{
<<<<<<< HEAD
    [Area(Constants.Admin)]
    public class TypeController(IUnitOfWork db,IMapper mapper) : Controller
    {
        private readonly IUnitOfWork _db = db;
        private readonly IMapper _mapper = mapper;
       

        [AllowAnonymous]
        public IActionResult Index()
        {
            var result =  _db.type.GetAll(IncludeWord : "Company");
            var allData = _mapper.Map<IEnumerable<TypeViewModel>>(result);
            return View(allData);
        }
        [Authorize(Roles = "Admin,Editor")]
=======
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

>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public IActionResult Create()
        {
            TypeViewModel TypeView = new TypeViewModel
            {
<<<<<<< HEAD
                type = _mapper.Map<Types>(new Types()),
               
=======
                type = new Types(),
                
                CompanyList = _db.company.GetAll().Select(x=> new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
            };
            return View(TypeView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]
=======
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public async Task<IActionResult> Create(TypeViewModel types)
        {
            if (types != null)
            {
<<<<<<< HEAD
                bool result = _db.type.TypeName(types);
=======
                bool result = _db.type.TypeName(types.type);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
                if (result)
                {
                    TempData["ErrorName"] = "The Type name already exists.";
                    return RedirectToAction(nameof(Index));
                }
<<<<<<< HEAD

                var EntityData = _mapper.Map<Types>(types);

                _db.type.Add(EntityData);
=======
                Types TypeModel = new Types
                {
                    TypeName = types.type.TypeName,
                    Notes = types.type.Notes,
                    CompanyId = types.type.CompanyId,
                };

                _db.type.Add(TypeModel);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
                await _db.Complete();
                TempData["Create"] = "Create Type";
                return RedirectToAction(nameof(Index));
            }
            return View(types);
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
<<<<<<< HEAD

            var Type = _db.type.GetById(x => x.Id == id);
            if (Type == null)
                return NotFound();

            var TypeView = _mapper.Map<TypeViewModel>(Type);


=======
            TypeViewModel TypeView = new TypeViewModel
            {
                type =  _db.type.GetById(x=>x.Id == id),

            CompanyList = _db.company.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
            return View(TypeView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]
=======
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public async Task<IActionResult> Edit(TypeViewModel types)
        {
            if (types == null)
            {
                return View(types);
            }

<<<<<<< HEAD
            bool result =  _db.type.TypeName(types);
=======
            bool result =  _db.type.TypeName(types.type);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
            if (result)
            {
                TempData["ErrorName"] = "The Type name already exists.";
                return RedirectToAction(nameof(Index));
            }

<<<<<<< HEAD
            var value =  _db.type.GetById(s => s.Id == types.Id);
            if (value != null)
            {
            
                _mapper.Map(types, value);
=======
            var value =  _db.type.GetById(s => s.Id == types.type.Id);
            if (value != null)
            {
                value.TypeName = types.type.TypeName; 
                value.Notes = types.type.Notes; 
                value.CompanyId = types.type.CompanyId;
                                                  
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183

                 _db.type.UpDate(value);
                await _db.Complete();
                TempData["Edit"] = "Edit Type";
                return RedirectToAction("Index");
            }

            return View(types);
        }
<<<<<<< HEAD
        [Authorize(Roles = Constants.Admin)]
=======

>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
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
<<<<<<< HEAD
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
                return RedirectToAction("Index");
            }
           
=======
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
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        }
    }
}
