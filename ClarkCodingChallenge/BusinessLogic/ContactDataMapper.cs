using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.Models.Api;
using ClarkCodingChallenge.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.DataMapping
{
	public class ContactDataMapper
	{
        public ContactEntity ToEntity(ContactViewModel viewModel)
        {
            return new ContactEntity
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email
            };
        }

        public ContactViewModel ToViewModel(ContactEntity entity)
        {
            return new ContactViewModel
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };
        }

        public ContactDTO ToApiModel(ContactEntity entity)
        {
            return new ContactDTO
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };
        }
    }
}
