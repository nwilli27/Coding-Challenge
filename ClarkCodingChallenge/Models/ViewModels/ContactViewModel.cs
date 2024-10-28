using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.Models
{
	public class ContactViewModel
	{
		[Required(ErrorMessage = "Please enter a first name.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter a last name.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "The email address is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		public string FullName => $"{this.FirstName} {this.LastName}";
	}
}
