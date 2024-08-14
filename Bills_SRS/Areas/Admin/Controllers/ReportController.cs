using bill_DataAccess.Data;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _db;
        public ReportController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var result = _db.report.GetAll(IncludeWord: "Sales");
            return View(result);
        }
        public IActionResult Create()
        {
            ReportViewModel reportView = new ReportViewModel
            {
                Report = new Report(),
                SalesList = _db.salesInvoice.GetAll().Select(x => new SelectListItem
                {
                    Text = x.DateTime.ToString("yyyy-MM-dd"),
                    Value = x.Id.ToString()
                })
            };

            return View(reportView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportViewModel model)
        {
            if (model != null)
            {
                Report report = new Report
                {
                    ReportFrom = model.Report.ReportFrom,
                    ReportTo = model.Report.ReportTo,
                    SalesId = model.Report.SalesId
                };
                _db.report.Add(report);
                await _db.Complete();

                TempData["Create"] = "Create Report";
                return RedirectToAction(nameof(Index));
            }

            model.SalesList = _db.salesInvoice.GetAll().Select(x => new SelectListItem
            {
                Text = x.DateTime.ToString("yyyy-MM-dd"),
                Value = x.Id.ToString()
            });

            return View(model);
        }
    }
}
