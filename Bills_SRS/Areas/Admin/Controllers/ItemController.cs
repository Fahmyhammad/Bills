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
using bill_Entities.ViewModel;
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bills_SRS.Areas.Admin.Controllers
{
<<<<<<< HEAD
    [Area(Constants.Admin)]
    public class ItemController(IUnitOfWork db, IMapper mapper) : Controller
    {
        private readonly IUnitOfWork _db = db;
        private readonly IMapper _mapper = mapper;

        [AllowAnonymous]
        public IActionResult Index()
        {
            var result = _db.item.GetAll(IncludeWord: "company", IncludeWord2: "type");
            var allData = _mapper.Map<IEnumerable<ItemViewModel>>(result);
            return View(allData);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Editor")]

=======
    [Area("Admin")]
    public class ItemController : Controller
    {
        private readonly IUnitOfWork _db;
        public ItemController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var result = _db.item.GetAll(IncludeWord: "company", IncludeWord2: "type");
            return View(result);
        }
        [HttpGet]
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public IActionResult Create()
        {
            ItemViewModel itemView = new ItemViewModel
            {
<<<<<<< HEAD
                item = _mapper.Map<tableItem>(new tableItem()),
=======
                item = new tableItem(),

                CompanyList = _db.company.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                TypeList = _db.type.GetAll().Select(x => new SelectListItem
                {
                    Text = x.TypeName,
                    Value = x.Id.ToString()
                })
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
            };
            return View(itemView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]

=======
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public async Task<IActionResult> AddItem(ItemViewModel items)
        {
            if (items != null)
            {
<<<<<<< HEAD
                bool result = _db.item.ItemName(items);
=======
                bool result = _db.item.ItemName(items.item);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
                if (result)
                {
                    TempData["ErrorName"] = "The Type name already exists.";
                    return RedirectToAction(nameof(Index));
                }
<<<<<<< HEAD


                if (items.BuyingPrice > items.SellingPrice)
=======
                tableItem itemModel = new tableItem
                {
                    ItemName = items.item.ItemName,
                    SellingPrice = items.item.SellingPrice,
                    BuyingPrice = items.item.BuyingPrice,
                    Notes = items.item.Notes,
                    CompanyId = items.item.CompanyId,
                    TypeId = items.item.TypeId
                };
                
                if (itemModel.BuyingPrice > itemModel.SellingPrice)
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
                {
                    TempData["ErrorPrice"] = "BUYING PRICE Must be less than or equal SELLING PRICE.";
                    return RedirectToAction(nameof(Index));
                }

<<<<<<< HEAD
                var netityData = _mapper.Map<tableItem>(items);


                _db.item.Add(netityData);
=======
                _db.item.Add(itemModel);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
                await _db.Complete();
                TempData["Create"] = "Create Item";
                return RedirectToAction(nameof(Index));
            }
            return View(items);
        }
        [HttpGet]
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]

=======
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
<<<<<<< HEAD

            var item = _db.item.GetById(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            var itemView = _mapper.Map<ItemViewModel>(item);

            return View(itemView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]

=======
            ItemViewModel itemView = new ItemViewModel
            {
                item = _db.item.GetById(x => x.Id == id),

                CompanyList = _db.company.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                TypeList = _db.type.GetAll().Select(x => new SelectListItem
                {
                    Text = x.TypeName,
                    Value = x.Id.ToString()
                })
            };
            return View(itemView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public async Task<IActionResult> Edit(ItemViewModel items)
        {
            if (items == null)
            {
                return View(items);
            }

<<<<<<< HEAD
            bool result = _db.item.ItemName(items);
=======
            bool result = _db.item.ItemName(items.item);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
            if (result)
            {
                TempData["ErrorName"] = "The Type name already exists.";
                return View(items);
            }
<<<<<<< HEAD
            var value = _db.item.GetById(x => x.Id == items.Id);
            if (value != null)
            {

                value.ItemName = items.ItemName;
                var data = _mapper.Map(items, value);
=======
            var value = _db.item.GetById(x => x.Id == items.item.Id);
            if (value != null)
            {
                value.ItemName = items.item.ItemName;
                value.BuyingPrice = items.item.BuyingPrice;
                value.SellingPrice = items.item.SellingPrice;
                value.CompanyId = items.item.CompanyId;
                value.TypeId = items.item.TypeId;
                value.Notes = items.item.Notes;
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183

                if (value.BuyingPrice > value.SellingPrice)
                {
                    TempData["ErrorPrice"] = "BUYING PRICE Must be less than or equal SELLING PRICE.";
                    return View(items);
                }

                _db.item.UpDate(value);
                await _db.Complete();
                TempData["Edit"] = "Edit Item";
                return RedirectToAction("Index");
            }
            return View(items);
        }
<<<<<<< HEAD
        [Authorize(Roles = Constants.Admin)]
=======
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var value = _db.item.GetById(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteItem(int? id)
        {
            try
            {
                var result = _db.item.GetById(x => x.Id == id);
                if (result == null)
                {
                    return NotFound();
                }
                _db.item.Delete(result);
                await _db.Complete();
                TempData["Delete"] = "Delete Item Successfull";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = $"Error deleting company: {ex.Message}";
                return RedirectToAction("Index");
            }

=======
        public async Task<IActionResult> DeleteItem(int? id)
        {
            var result = _db.item.GetById(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            _db.item.Delete(result);
            await _db.Complete();
            TempData["Delete"] = "Delete Item Successfull";
            return RedirectToAction("Index");
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        }
    }
}
