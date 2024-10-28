using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.DataAccess;
using ClarkCodingChallenge.DataMapping;

namespace ClarkCodingChallenge.Controllers
{
    public class ContactsController : Controller
    {
        #region Properties

        private readonly ContactsService contactsService;

        #endregion

        #region Construction

        public ContactsController(ContactsService contactsService)
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
				this.contactsService.AddContact(ContactDataMapper.ToEntity(model));
				this.addConfirmationMessage($"Contact {model.FullName} created successfully.");
                return RedirectToAction(nameof(Create));
            }
            else
            {
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Private Helpers

        private void addConfirmationMessage(string message)
        {
            TempData["ConfirmationMessage"] = message;
        }

        #endregion
    }
}
