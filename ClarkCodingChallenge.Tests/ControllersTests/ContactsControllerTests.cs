using ClarkCodingChallenge.BusinessLogic.Interfaces;
using ClarkCodingChallenge.Controllers;
using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.Models.Api;
using ClarkCodingChallenge.Tests.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ClarkCodingChallenge.Tests.ControllerTest
{
    [TestClass]
    public class ContactsControllerTests
    {
		#region Initialization

		private Mock<IContactsService> mockContactsService;
        private ContactsController controller;
        private ValidationHelper validationHelper;

        [TestInitialize]
        public void Setup()
        {
            mockContactsService = new Mock<IContactsService>();
            controller = new ContactsController(mockContactsService.Object);
            validationHelper = new ValidationHelper();
        }

		#endregion

		#region Action Tests

		[TestMethod]
        public void Index_ReturnsRedirectToCreate()
        {
            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Create", redirectResult.ActionName);
        }

        [TestMethod]
        public void Create_ReturnsViewWithContactViewModel()
        {
            var result = controller.Create();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.Model, typeof(ContactViewModel));
        }

        [TestMethod]
        public void Create_PostValidModelRedirectsToCreate()
        {
            var model = new ContactViewModel { FirstName = "Billy", LastName = "Bob", Email = "test1@gmail.com" };
            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

            var result = controller.Create(model);

            mockContactsService.Verify(s => s.AddContact(model), Times.Once);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Create", redirectResult.ActionName);
            Assert.AreEqual("Contact Billy Bob created successfully.", controller.TempData["ConfirmationMessage"]);
        }

        [TestMethod]
        public void Create_PostInvalidModelReturnsViewWithModel()
        {
            var model = new ContactViewModel();
            controller.ModelState.AddModelError("error", "test error");

            var result = controller.Create(model);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = result as ViewResult;
            Assert.AreEqual(model, viewResult.Model);
            mockContactsService.Verify(s => s.AddContact(It.IsAny<ContactViewModel>()), Times.Never);
        }

        [TestMethod]
        public void Search_ReturnsOkWithContacts()
        {
            var contacts = new List<ContactDTO> { new ContactDTO { FirstName = "Billy", LastName = "Bob", Email = "test@gmail.com" } };
            mockContactsService.Setup(s => s.SearchContacts(It.IsAny<string>(), It.IsAny<bool>())).Returns(contacts);

            var result = controller.Search("Bob", false);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(contacts, okResult.Value);
            var list = okResult.Value as List<ContactDTO>;
            Assert.IsTrue(list.Any(x => x.Email == "test@gmail.com"));
        }

        #endregion

        #region Validation Tests

        [TestMethod]
        public void Contact_FirstName_WhenMissing_ShouldHaveRequiredValidationError()
        {
            var model = new ContactViewModel { LastName = "Bob", Email = "test@example.com" };

            validationHelper.ValidateModel(model);

            Assert.IsTrue(validationHelper.HasValidationError(nameof(model.FirstName), "Please enter a first name."));
        }

        [TestMethod]
        public void Contact_LastName_WhenMissing_ShouldHaveRequiredValidationError()
        {
            var model = new ContactViewModel { FirstName = "Billy", Email = "test@example.com" };

            validationHelper.ValidateModel(model);

            Assert.IsTrue(validationHelper.HasValidationError(nameof(model.LastName), "Please enter a last name."));
        }

        [TestMethod]
        public void Contact_Email_WhenMissing_ShouldHaveRequiredValidationError()
        {
            var model = new ContactViewModel { FirstName = "Billy", LastName = "Bob" };

            validationHelper.ValidateModel(model);

            Assert.IsTrue(validationHelper.HasValidationError(nameof(model.Email), "The email address is required."));
        }

        [TestMethod]
        public void Contact_Email_WhenInvalid_ShouldHaveEmailValidationError()
        {
            var model = new ContactViewModel { FirstName = "Billy", Email = "invalid-email" };

            validationHelper.ValidateModel(model);

            Assert.IsTrue(validationHelper.HasValidationError(nameof(model.Email), "Invalid Email Address."));
        }

        [TestMethod]
        public void Contact_WhenValid_ShouldNotHaveValidationErrors()
        {
            var model = new ContactViewModel { FirstName = "Billy", LastName = "Bob", Email = "test@example.com" };

            validationHelper.ValidateModel(model);

            Assert.IsFalse(validationHelper.HasValidationErrors);
        }

		#endregion
	}
}
