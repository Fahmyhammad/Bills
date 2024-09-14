using AutoMapper;
using bill_Entities.Const;
using bill_Entities.Models;
using bill_Entities.Repoistory;
using bill_Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bills_SRS.Areas.Admin.Controllers
{
    [Area(Constants.Admin)]
    public class ClientController(IUnitOfWork db, IMapper mapper) : Controller
    {
        private readonly IUnitOfWork _db = db;
        private readonly IMapper _mapper = mapper;
        [AllowAnonymous]
        public IActionResult Index()
        {
            var alldata = _db.client.GetAll();
            var result = _mapper.Map<IEnumerable<ClientViewModel>>(alldata);
            return View(result);
        }
        [Authorize(Roles = "Admin,Editor")]

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Editor")]

        public async Task<IActionResult> Create(ClientViewModel client)
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

                var clientEntity = _mapper.Map<Client>(client);

                _db.client.Add(clientEntity);
                await _db.Complete();
                TempData["Create"] = "Create Client";
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }
        [Authorize(Roles = "Admin,Editor")]

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
        [Authorize(Roles = "Admin,Editor")]

        public async Task<IActionResult> Edit(ClientViewModel client)
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

                    _mapper.Map(client, value);
                    value.Number = _db.client.RandomNumber();

                    _db.client.UpDate(value);
                    await _db.Complete();
                    TempData["Edit"] = "Edit client";
                    return RedirectToAction("Index");
                }

            }
            return View(client);
        }
        [Authorize(Roles = Constants.Admin)]
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
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteClient(int? id)
        {
            try
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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting company: {ex.Message}";
                return RedirectToAction("Index");
            }

        }
    }
}
