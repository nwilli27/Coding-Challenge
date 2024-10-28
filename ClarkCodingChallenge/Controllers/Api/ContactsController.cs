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

		private readonly ContactsDataAccess contactsDataAccess;

		#endregion

		#region Construction

		public ContactsController(ContactsDataAccess contactsDataAccess)
		{
			this.contactsDataAccess = contactsDataAccess;
		}

		#endregion

		#region Endpoints

		[HttpGet]
		public IActionResult Search(string lastName, bool sortByLastNameDescending)
		{
			var contacts = this.contactsDataAccess.SearchByLastName(lastName, sortByLastNameDescending);
			var contactResponse = contacts.Select(x => ContactDataMapper.ToApiModel(x));

			return this.Ok(contactResponse);
		}

		#endregion
	}
}
