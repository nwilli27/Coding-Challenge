using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.BusinessLogic.Interfaces
{
	public interface IContactsService
	{
		void AddContact(ContactViewModel contactViewModel);
		IEnumerable<ContactDTO> SearchContacts(string lastName, bool sortByDescending);
	}
}
