using ClarkCodingChallenge.DataAccess.Interfaces;
using ClarkCodingChallenge.DataMapping;
using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.Models.Api;
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
		private readonly ContactDataMapper mapper;

		#endregion

		#region Construction

		public ContactsService(IRepository<ContactEntity> contactsRepository, ContactDataMapper mapper)
		{
			this.contactsRepository = contactsRepository;
			this.mapper = mapper;
		}

		#endregion

		#region Methods

		public void AddContact(ContactViewModel contactViewModel)
		{
			this.contactsRepository.Add(this.mapper.ToEntity(contactViewModel));
		}

		public IEnumerable<ContactDTO> SearchContacts(string lastName, bool sortByDescending)
		{
			var contacts = string.IsNullOrEmpty(lastName)
						 ? this.contactsRepository.GetAll()
						 : this.contactsRepository.Where(x => x.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

			contacts = sortByDescending
				     ? contacts.OrderByDescending(x => x.LastName).ThenByDescending(x => x.FirstName)
				     : contacts.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);

			return contacts.Select(x => this.mapper.ToApiModel(x));
		}

		#endregion
	}
}
