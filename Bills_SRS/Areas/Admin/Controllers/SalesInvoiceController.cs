using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesInvoiceController : Controller
    {
        private readonly IUnitOfWork _db;
        public SalesInvoiceController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var result = _db.salesInvoice.GetAll(IncludeWord: "client", IncludeWord2: "TableItem");
            return View(result);
        }
        public IActionResult Create()
        {
            SalesViewModel salesView = new SalesViewModel {

                SalesInvoices = new SalesInvoice(),

                ClientsList = _db.client.GetAll().Select(x=> new SelectListItem
                {
                    Text = x.ClientName,
                    Value = x.Id.ToString()
                }),
                ItemsList = _db.item.GetAll().Select(x=> new SelectListItem
                {
                    Text = x.ItemName,
                    Value = x.Id.ToString()
                }),
               
        };
            return View(salesView);
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesViewModel salesView)
        {
            if (salesView != null)
            {
                salesView.SalesInvoices.BillsCode = _db.salesInvoice.RandomNum();
                var item = _db.item.GetById(x => x.Id == salesView.SalesInvoices.TableItemId);
                if (item == null)
                {
                    ModelState.AddModelError("", "The selected item does not exist.");
                    salesView.ClientsList = _db.client.GetAll().Select(x => new SelectListItem
                    {
                        Text = x.ClientName,
                        Value = x.Id.ToString()
                    });
                    salesView.ItemsList = _db.item.GetAll().Select(x => new SelectListItem
                    {
                        Text = x.ItemName,
                        Value = x.Id.ToString()
                    });
                    return View(salesView);
                }

                salesView.SalesInvoices.Price = item.SellingPrice;
                SalesInvoice sales = new SalesInvoice
                {
                    DateTime = salesView.SalesInvoices.DateTime,
                    BillsCode = salesView.SalesInvoices.BillsCode,
                    ClientId = salesView.SalesInvoices.ClientId,
                    TableItemId = salesView.SalesInvoices.TableItemId,
                    Quintity = salesView.SalesInvoices.Quintity,
                    Total = salesView.SalesInvoices.Total,
                    Discount = salesView.SalesInvoices.Discount,
                    NetPrice = salesView.SalesInvoices.NetPrice,
                    PaidUp = salesView.SalesInvoices.PaidUp,
                    TheRest = salesView.SalesInvoices.TheRest
                };

                salesView.SalesInvoices.Total = _db.salesInvoice.CalculatPrice(salesView.SalesInvoices.Quintity , salesView.SalesInvoices.Price);

                salesView.SalesInvoices.NetPrice =_db.salesInvoice.CalculatNetPrice(salesView.SalesInvoices.Total , salesView.SalesInvoices.Discount);

                salesView.SalesInvoices.TheRest =_db.salesInvoice.CalculatTheRest(salesView.SalesInvoices.NetPrice , salesView.SalesInvoices.PaidUp);
              
                _db.salesInvoice.Add(salesView.SalesInvoices);
                await _db.Complete();
                TempData["Create"] = "Create Sales";
                return RedirectToAction("Index");

            }

            salesView.ClientsList = _db.client.GetAll().Select(x => new SelectListItem
            {
                Text = x.ClientName,
                Value = x.Id.ToString()
            });
            salesView.ItemsList = _db.item.GetAll().Select(x => new SelectListItem
            {
                Text = x.ItemName,
                Value = x.Id.ToString()
            });

            return View(salesView);
        }
        public IActionResult Ditails(int? id)
        {
            if (id == null|| id == 0)
            {
                return NotFound();
            }

            SalesViewModel salesView = new SalesViewModel
            {

                SalesInvoices = _db.salesInvoice.GetById(x=>x.Id == id),

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
        public IActionResult Delete(int? id)
        {
            if(id== null || id == 0)
            {
                return NotFound();
            }

            var value = _db.salesInvoice.GetById(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBill(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var result = _db.salesInvoice.GetById(x => x.Id == id);
            if(result == null)
            {
                return NotFound();
            }
            _db.salesInvoice.Delete(result);
            await _db.Complete();
            TempData["Delete"] = "Delete Sales Successfull";
            return RedirectToAction("Index");
        }
    }
}
