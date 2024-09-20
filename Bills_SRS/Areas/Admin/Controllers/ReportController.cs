using AutoMapper;
using bill_DataAccess.Data;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class ReportController(IUnitOfWork db, IMapper mapper) : Controller
    {
        private readonly IUnitOfWork _db = db;
        private readonly IMapper _mapper = mapper;


        public IActionResult Index()
        {
            var result = _db.report.GetAll(IncludeWord: "Sales");
            var allDaata = _mapper.Map<IEnumerable<ReportViewModel>>(result);
            return View(allDaata);
        }
        public IActionResult Create()
        {
            ReportViewModel reportView = new ReportViewModel
            {
                Report = _mapper.Map<Report>(new Report()),

            };

            return View(reportView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportViewModel model)
        {
            if (model != null)
            {
               
                var DataEntity = _mapper.Map<Report>(model);

                _db.report.Add(DataEntity);
                await _db.Complete();

                TempData["Create"] = "Create Report";
                return RedirectToAction(nameof(Create));
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