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
=======
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
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public async Task<IActionResult> Create(SalesViewModel salesView)
        {
            if (salesView != null)
            {
<<<<<<< HEAD
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
=======
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
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
                await _db.Complete();
                TempData["Create"] = "Create Sales";
                return RedirectToAction("Index");

            }

<<<<<<< HEAD
            return View(salesView);
        }

       
        [AllowAnonymous]
        public IActionResult Ditails(int? id)
        {
            if (id == null || id == 0)
=======
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
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
            {
                return NotFound();
            }

            SalesViewModel salesView = new SalesViewModel
            {

<<<<<<< HEAD
                SalesInvoices = _db.salesInvoice.GetById(x => x.Id == id),
=======
                SalesInvoices = _db.salesInvoice.GetById(x=>x.Id == id),
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183

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
<<<<<<< HEAD
        [Authorize(Roles = Constants.Admin)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
=======
        public IActionResult Delete(int? id)
        {
            if(id== null || id == 0)
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
            {
                return NotFound();
            }

            var value = _db.salesInvoice.GetById(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
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

=======
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
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
    }
}
