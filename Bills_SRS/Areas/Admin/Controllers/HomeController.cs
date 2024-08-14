using bill_Entities.Repoistory;
using Microsoft.AspNetCore.Mvc;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _db;
        public HomeController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.Bills = _db.salesInvoice.GetAll().Count();
            ViewBag.Company = _db.company.GetAll().Count();
            ViewBag.Units = _db.unitss.GetAll().Count();
            return View();
        }
    }
}
