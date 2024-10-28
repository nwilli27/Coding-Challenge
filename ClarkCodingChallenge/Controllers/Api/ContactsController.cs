using ClarkCodingChallenge.DataAccess;
using ClarkCodingChallenge.DataMapping;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.Controllers.Api
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ContactsController : ControllerBase
	{
		#region Members

		private readonly ContactsService contactsService;

		#endregion

		#region Construction

		public ContactsController(ContactsService contactsService)
		{
			this.contactsService = contactsService;
		}

		#endregion

		#region Endpoints

		[HttpGet]
		public IActionResult Search(string lastName, bool sortByLastNameDescending)
		{
			var contacts = this.contactsService.SearchContacts(lastName, sortByLastNameDescending);
			var contactResponse = contacts.Select(x => ContactDataMapper.ToApiModel(x));

			return this.Ok(contactResponse);
		}

		#endregion
	}
}
