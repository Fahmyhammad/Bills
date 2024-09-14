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
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
using Microsoft.AspNetCore.Mvc;

namespace Bills_SRS.Areas.Admin.Controllers
{
<<<<<<< HEAD
    [Area(Constants.Admin)]
    public class ClientController(IUnitOfWork db , IMapper mapper) : Controller
    {
        private readonly IUnitOfWork _db = db;
        private readonly IMapper _mapper=mapper;
        [AllowAnonymous]
        public IActionResult Index()
        {
            var alldata = _db.client.GetAll();
            var result = _mapper.Map<IEnumerable<ClientViewModel>>(alldata);
            return View(result);
        }
        [Authorize(Roles = "Admin,Editor")]

=======
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
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]

        public async Task<IActionResult> Create(ClientViewModel client)
=======
        public async Task<IActionResult> Create(Client client)
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
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
<<<<<<< HEAD

                var clientEntity = _mapper.Map<Client>(client);

                _db.client.Add(clientEntity);
=======
                
                _db.client.Add(client);
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
                await _db.Complete();
                TempData["Create"] = "Create Client";
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]
=======
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183

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
<<<<<<< HEAD
        [Authorize(Roles = "Admin,Editor")]

        public async Task<IActionResult> Edit(ClientViewModel client)
=======
        public async Task<IActionResult> Edit(Client client)
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
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
<<<<<<< HEAD
                  
                    _mapper.Map(client,value);
                    value.Number = _db.client.RandomNumber();
=======
                    value.ClientName = client.ClientName;
                    value.Address = client.Address;
                    value.Phone = client.Phone;

>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183

                    _db.client.UpDate(value);
                    await _db.Complete();
                    TempData["Edit"] = "Edit client";
                    return RedirectToAction("Index");
                }

            }
            return View(client);
        }
<<<<<<< HEAD
        [Authorize(Roles = Constants.Admin)]
=======

>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
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
<<<<<<< HEAD
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

=======
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
>>>>>>> 7ee9e6f3704b6ad185026200ad492c1c0f1e7183
        }
    }
}

