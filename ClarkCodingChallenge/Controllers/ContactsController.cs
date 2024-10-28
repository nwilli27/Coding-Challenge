using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.DataAccess;
using ClarkCodingChallenge.BusinessLogic.Interfaces;

namespace ClarkCodingChallenge.Controllers
{
    public class ContactsController : Controller
    {
        #region Properties

        private readonly IContactsService contactsService;

        #endregion

        #region Construction

        public ContactsController(IContactsService contactsService)
		{
            this.contactsService = contactsService;
		}

        #endregion

        #region Actions

        public IActionResult Index() => RedirectToAction("Create");

        public IActionResult Create() => View(new ContactViewModel());

        [HttpPost]
        public IActionResult Create(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
				this.contactsService.AddContact(model);
				this.addConfirmationMessage($"Contact {model.FullName} created successfully.");
                return RedirectToAction(nameof(Create));
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Search(string lastName, bool sortByDescending) => this.Ok(this.contactsService.SearchContacts(lastName, sortByDescending));

        #endregion

        #region Private Helpers

        private void addConfirmationMessage(string message)
        {
            TempData["ConfirmationMessage"] = message;
        }

        #endregion
    }
}
