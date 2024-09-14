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
    public class SalesInvoiceController(IUnitOfWork db, IMapper mapper) : Controller
    {
        private readonly IUnitOfWork _db = db;
        private readonly IMapper _mapper = mapper;

        [AllowAnonymous]
        public IActionResult Index()
        {
            var result = _db.salesInvoice.GetAll(IncludeWord: "client", IncludeWord2: "TableItem");
            var allData = _mapper.Map<IEnumerable<SalesViewModel>>(result);
            return View(allData);
        }

        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Create()
        {
            var salesView = new SalesViewModel
            {

                SalesInvoices = _mapper.Map<SalesInvoice>(new SalesInvoice()),

            };
            return View(salesView);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]
        public async Task<IActionResult> Create(SalesViewModel salesView)
        {
            if (salesView != null)
            {
                salesView.BillsCode = _db.salesInvoice.RandomNum();
                var item = _db.item.GetById(x => x.Id == salesView.TableItemId);
                if (item == null)
                {
                    ModelState.AddModelError("", "The selected item does not exist.");
                    return View(salesView);
                }

                salesView.Price = item.SellingPrice;

                salesView.Total = _db.salesInvoice.CalculatPrice(salesView.Quintity, salesView.Price);

                salesView.NetPrice = _db.salesInvoice.CalculatNetPrice(salesView.Total, salesView.Discount);

                salesView.TheRest = _db.salesInvoice.CalculatTheRest(salesView.NetPrice, salesView.PaidUp);


                var entityData = _mapper.Map<SalesInvoice>(salesView);

                _db.salesInvoice.Add(entityData);
                await _db.Complete();
                TempData["Create"] = "Create Sales";
                return RedirectToAction("Index");

            }

            return View(salesView);
        }


        [AllowAnonymous]
        public IActionResult Ditails(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            SalesViewModel salesView = new SalesViewModel
            {

                SalesInvoices = _db.salesInvoice.GetById(x => x.Id == id),

                ClientsList = _db.client.GetAll().Select(x => new SelectListItem
                {
                    Text = x.ClientName,
                    Value = x.Id.ToString()
                }),
                ItemsList = _db.item.GetAll().Select(x => new SelectListItem
                {
                    Text = x.ItemName,
                    Value = x.Id.ToString()
                }),

            };
            return View(salesView);
        }
        [Authorize(Roles = Constants.Admin)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var value = _db.salesInvoice.GetById(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteBill(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            try
            {
                var result = _db.salesInvoice.GetById(x => x.Id == id);
                if (result == null)
                {
                    return NotFound();
                }
                _db.salesInvoice.Delete(result);
                await _db.Complete();
                TempData["Delete"] = "Delete Sales Successfull";
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