using bill_Entities.Models;
using bill_Entities.Repoistory;
using Microsoft.AspNetCore.Mvc;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _db;
        public ClientController(IUnitOfWork db)
        {
            _db= db;
        }
        public IActionResult Index()
        {
            var result = _db.client.GetAll();
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                client.Number = _db.client.RandomNumber();
                bool result = _db.client.ClientName(client);
                if (result)
                {
                    TempData["ErrorName"] = "The Client name already exists.";
                    return RedirectToAction(nameof(Index));
                }
                
                _db.client.Add(client);
                await _db.Complete();
                TempData["Create"] = "Create Client";
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var value = _db.client.GetById(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                bool result = _db.client.ClientName(client);
                if (result)
                {
                    TempData["ErrorName"] = "The client name already exists.";
                    return RedirectToAction(nameof(Index));
                }

                var value = _db.client.GetById(s => s.Id == client.Id);
                if (value != null)
                {
                    value.ClientName = client.ClientName;
                    value.Address = client.Address;
                    value.Phone = client.Phone;


                    _db.client.UpDate(value);
                    await _db.Complete();
                    TempData["Edit"] = "Edit client";
                    return RedirectToAction("Index");
                }

            }
            return View(client);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var value = _db.client.GetById(x => x.Id == id);
            return View(value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteClient(int? id)
        {
            var result = _db.client.GetById(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            _db.client.Delete(result);
            await _db.Complete();
            TempData["Delete"] = "Delete client Successfull";
            return RedirectToAction("Index");
        }
    }
}

