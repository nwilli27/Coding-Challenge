using ClarkCodingChallenge.DataAccess;
using ClarkCodingChallenge.DataAccess.Interfaces;
using ClarkCodingChallenge.DataMapping;
using ClarkCodingChallenge.Models;
using ClarkCodingChallenge.Models.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClarkCodingChallenge.Tests.BusinessLogicTests
{
    [TestClass]
    public class ContactsBusinessLogicTests
    {
        private Mock<IRepository<ContactEntity>> mockRepository;
        private ContactDataMapper mapper;
        private ContactsService service;

        [TestInitialize]
        public void Setup()
        {
            mockRepository = new Mock<IRepository<ContactEntity>>();
            mapper = new ContactDataMapper();
            service = new ContactsService(mockRepository.Object, mapper);
        }

        [TestMethod]
        public void AddContact_CallsRepositoryWithMappedEntity()
        {
            var contactViewModel = new ContactViewModel 
            { 
                FirstName = "Billy", 
                LastName = "Bob", 
                Email = "test1@gmail.com" 
            };

            var expectedEntity = new ContactEntity 
            {
                FirstName = "Billy",
                LastName = "Bob",
                Email = "test1@gmail.com"
            };

            service.AddContact(contactViewModel);

            mockRepository.Verify(repo => repo.Add(It.Is<ContactEntity>(entity =>
                entity.FirstName == expectedEntity.FirstName &&
                entity.LastName == expectedEntity.LastName &&
                entity.Email == expectedEntity.Email
            )), Times.Once);
        }

        [DataTestMethod]
        [DataRow(null, false)]
        [DataRow(null, true)]
        [DataRow("Billy", false)]
        [DataRow("Billy", true)]
        public void SearchContacts_ReturnsSortedAndFilterResults(string lastName, bool sortByDescending)
        {
            var contacts = new List<ContactEntity>
            {
                new ContactEntity { FirstName = "Test1", LastName = "Billy", Email = "test1@gmail.com" },
                new ContactEntity { FirstName = "Test2", LastName = "Bob", Email = "test2@gmail.com" },
                new ContactEntity { FirstName = "Test3", LastName = "Billy", Email = "test3@gmail.com" },
                new ContactEntity { FirstName = "Test4", LastName = "Williams", Email = "test4@gmail.com" }
            };

            mockRepository.Setup(r => r.GetAll()).Returns(contacts);
            mockRepository.Setup(r => r.Where(It.IsAny<Func<ContactEntity, bool>>()))
                          .Returns((Func<ContactEntity, bool> predicate) => contacts.Where(predicate));

            var results = service.SearchContacts(lastName, sortByDescending).ToList();

            if (!string.IsNullOrEmpty(lastName))
            {
                Assert.IsTrue(results.All(r => string.Equals(r.LastName, lastName, StringComparison.OrdinalIgnoreCase)), "All returned contacts match the given last name.");
            }
            else
            {
                Assert.AreEqual(contacts.Count(), results.Count, "All contacts returned with no given last name.");
            }

            var expectedOrder = results.OrderBy(r => r.LastName).ThenBy(r => r.FirstName);
            if (sortByDescending)
            {
                expectedOrder = expectedOrder.OrderByDescending(r => r.LastName).ThenByDescending(r => r.FirstName);
            }

            CollectionAssert.AreEqual(expectedOrder.ToList(), results, "Results are not sorted as expected.");
        }
    }
}
