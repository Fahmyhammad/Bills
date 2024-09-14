<<<<<<< HEAD
﻿using bill_Entities.Const;
using bill_Entities.Repoistory;
using Microsoft.AspNetCore.Authorization;
=======
﻿using bill_Entities.Repoistory;
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
using Microsoft.AspNetCore.Mvc;

namespace Bills_SRS.Areas.Admin.Controllers
{
<<<<<<< HEAD
    [Area(Constants.Admin)]
    [AllowAnonymous]
    public class HomeController(IUnitOfWork db) : Controller
    {
        private readonly IUnitOfWork _db = db;
       
=======
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _db;
        public HomeController(IUnitOfWork db)
        {
            _db = db;
        }
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public IActionResult Index()
        {
            ViewBag.Bills = _db.salesInvoice.GetAll().Count();
            ViewBag.Company = _db.company.GetAll().Count();
            ViewBag.Units = _db.unitss.GetAll().Count();
            return View();
        }
    }
}
