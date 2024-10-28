using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.Models.Api;
using ClarkCodingChallenge.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.DataMapping
{
	public static class ContactDataMapper
	{
        public static ContactEntity ToEntity(ContactViewModel viewModel)
        {
            return new ContactEntity
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email
            };
        }

        public static ContactViewModel ToViewModel(ContactEntity entity)
        {
            return new ContactViewModel
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };
        }

        public static ContactApiResponseModel ToApiModel(ContactEntity entity)
        {
            return new ContactApiResponseModel
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };
        }
    }
}
