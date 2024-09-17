using bill_Entities.Const;
using bill_Entities.Repoistory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection.Metadata;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area(Constants.Admin)]
    [AllowAnonymous]
    public class HomeController(IUnitOfWork db) : Controller
    {
        private readonly IUnitOfWork _db = db;

        public IActionResult Index()
        {
            ViewBag.Bills = _db.salesInvoice.GetAll().Count();
            ViewBag.Company = _db.company.GetAll().Count();
            ViewBag.Units = _db.unitss.GetAll().Count();
            return View();
        }
    }
}