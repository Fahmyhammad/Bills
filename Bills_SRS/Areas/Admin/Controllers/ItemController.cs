using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bills_SRS.Areas.Admin.Controllers
{
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
        public IActionResult Create()
        {
            ItemViewModel itemView = new ItemViewModel
            {
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
            };
            return View(itemView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(ItemViewModel items)
        {
            if (items != null)
            {
                bool result = _db.item.ItemName(items.item);
                if (result)
                {
                    TempData["ErrorName"] = "The Type name already exists.";
                    return RedirectToAction(nameof(Index));
                }
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
                {
                    TempData["ErrorPrice"] = "BUYING PRICE Must be less than or equal SELLING PRICE.";
                    return RedirectToAction(nameof(Index));
                }

                _db.item.Add(itemModel);
                await _db.Complete();
                TempData["Create"] = "Create Item";
                return RedirectToAction(nameof(Index));
            }
            return View(items);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> Edit(ItemViewModel items)
        {
            if (items == null)
            {
                return View(items);
            }

            bool result = _db.item.ItemName(items.item);
            if (result)
            {
                TempData["ErrorName"] = "The Type name already exists.";
                return View(items);
            }
            var value = _db.item.GetById(x => x.Id == items.item.Id);
            if (value != null)
            {
                value.ItemName = items.item.ItemName;
                value.BuyingPrice = items.item.BuyingPrice;
                value.SellingPrice = items.item.SellingPrice;
                value.CompanyId = items.item.CompanyId;
                value.TypeId = items.item.TypeId;
                value.Notes = items.item.Notes;

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
        }
    }
}
