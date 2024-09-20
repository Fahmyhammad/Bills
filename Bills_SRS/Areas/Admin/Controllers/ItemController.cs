using AutoMapper;
using bill_Entities.Const;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bills_SRS.Areas.Admin.Controllers
{
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
        public IActionResult GetTypesByCompanyId(int companyId)
        {
            var types = _db.type.GetAll(t => t.CompanyId == companyId);
            var result = types.Select(t => new {
                id = t.Id,
                typeName = t.TypeName
            });

            return Json(result);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Create()
        {

            var listComp = _db.company.GetAll();

            ItemViewModel itemView = new ItemViewModel
            {
                item = _mapper.Map<tableItem>(new tableItem()),
                CompanyList = listComp,

                TypeList = new List<Types>()

            };
            return View(itemView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]

        public async Task<IActionResult> AddItem(ItemViewModel items)
        {
            if (items != null)
            {
                bool result = _db.item.ItemName(items);
                if (result)
                {
                    TempData["ErrorName"] = "The Type name already exists.";
                    return RedirectToAction(nameof(Create));
                }


                if (items.BuyingPrice > items.SellingPrice)
                {
                    TempData["ErrorPrice"] = "BUYING PRICE Must be less than or equal SELLING PRICE.";
                    return RedirectToAction(nameof(Create));
                }

                var netityData = _mapper.Map<tableItem>(items);


                _db.item.Add(netityData);
                await _db.Complete();
                TempData["Create"] = "Create Item";
                return RedirectToAction(nameof(Index));
            }
            return View(items);
        }


        [HttpGet]
        public JsonResult GetTypesByCompany(int companyId)
        {
            var types = _db.type.GetAll(x => x.CompanyId == companyId);
            return Json(types);
        }



        [HttpGet]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var item = _db.item.GetById(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            var listComp = _db.company.GetAll();

            var itemView = _mapper.Map<ItemViewModel>(item);

            itemView.CompanyList = _db.company.GetAll(); 

            itemView.TypeList = _db.type.GetAll(x => x.CompanyId == item.CompanyId);


            return View(itemView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]

        public async Task<IActionResult> Edit(ItemViewModel items)
        {
            if (items == null)
            {
                return View(items);
            }

            bool result = _db.item.ItemName(items);
            if (result)
            {
                TempData["ErrorName"] = "The Type name already exists.";
                return RedirectToAction(nameof(Edit));
            }
            var value = _db.item.GetById(x => x.Id == items.Id);
            if (value != null)
            {

                value.ItemName = items.ItemName;
                var data = _mapper.Map(items, value);

                if (value.BuyingPrice > value.SellingPrice)
                {
                    TempData["ErrorPrice"] = "BUYING PRICE Must be less than or equal SELLING PRICE.";
                    return RedirectToAction(nameof(Edit));
                }

                _db.item.UpDate(value);
                await _db.Complete();
                TempData["Edit"] = "Edit Item";
                return RedirectToAction("Index");
            }
            return View(items);
        }
        [Authorize(Roles = Constants.Admin)]
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
                return RedirectToAction("Delete");
            }

        }
    }
}