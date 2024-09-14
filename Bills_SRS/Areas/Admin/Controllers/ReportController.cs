<<<<<<< HEAD
﻿using AutoMapper;
using bill_DataAccess.Data;
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
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area("Admin")]
<<<<<<< HEAD
    [AllowAnonymous]
    public class ReportController(IUnitOfWork db,IMapper mapper) : Controller
    {
        private readonly IUnitOfWork _db= db;
        private readonly IMapper _mapper = mapper;
       

        public IActionResult Index()
        {
            var result = _db.report.GetAll(IncludeWord: "Sales");
            var allDaata = _mapper.Map<IEnumerable<ReportViewModel>>(result);
            return View(allDaata);
=======
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
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        }
        public IActionResult Create()
        {
            ReportViewModel reportView = new ReportViewModel
            {
<<<<<<< HEAD
                Report = _mapper.Map<Report>(new Report()),
               
=======
                Report = new Report(),
                SalesList = _db.salesInvoice.GetAll().Select(x => new SelectListItem
                {
                    Text = x.DateTime.ToString("yyyy-MM-dd"),
                    Value = x.Id.ToString()
                })
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
            };

            return View(reportView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportViewModel model)
        {
            if (model != null)
            {
<<<<<<< HEAD
                //Report report = new Report
                //{
                //    ReportFrom = model.Report.ReportFrom,
                //    ReportTo = model.Report.ReportTo,
                //    SalesId = model.Report.SalesId
                //};

                var DataEntity = _mapper.Map<Report>(model);

                _db.report.Add(DataEntity);
=======
                Report report = new Report
                {
                    ReportFrom = model.Report.ReportFrom,
                    ReportTo = model.Report.ReportTo,
                    SalesId = model.Report.SalesId
                };
                _db.report.Add(report);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
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
