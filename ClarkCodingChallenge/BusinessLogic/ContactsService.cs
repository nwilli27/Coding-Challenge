using ClarkCodingChallenge.DataAccess.Interfaces;
using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClarkCodingChallenge.DataAccess
{
    public class ContactsService
    {
		#region Members

		private readonly IRepository<ContactEntity> contactsRepository;

		#endregion

		#region Construction

		public ContactsService(IRepository<ContactEntity> contactsRepository)
		{
			this.contactsRepository = contactsRepository;
		}

		#endregion

		#region Methods

		public void AddContact(ContactEntity contact)
		{
			this.contactsRepository.Add(contact);
		}

		public IEnumerable<ContactEntity> SearchContacts(string lastName, bool sortByLastNameDescending)
		{
			var contacts = string.IsNullOrEmpty(lastName)
						 ? this.contactsRepository.GetAll()
						 : this.contactsRepository.Where(x => x.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

			return sortByLastNameDescending
				 ? contacts.OrderByDescending(x => x.LastName).ThenBy(x => x.FirstName)
				 : contacts.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
		}

		#endregion
	}
}
