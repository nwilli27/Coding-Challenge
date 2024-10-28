using ClarkCodingChallenge.DataAccess.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClarkCodingChallenge.Tests.DataAccessTests
{
	[TestClass]
	public class InMemoryRepositoryTests
	{
		#region Initialization

		private InMemoryRepository<TestEntity> repository;

        [TestInitialize]
        public void Setup()
        {
            repository = new InMemoryRepository<TestEntity>();
        }

		#endregion

		#region Test Methods

		[TestMethod]
        public void Add_EntityToRepository()
        {
            var entity = new TestEntity { Id = 1, Name = "Test Entity 1" };

            repository.Add(entity);
            var allItems = repository.GetAll();

            Assert.AreEqual(1, allItems.Count());
            Assert.AreEqual(entity, allItems.First());
        }

        [TestMethod]
        public void GetAll_EntitiesFromRepository()
        {
            var entity1 = new TestEntity { Id = 1, Name = "Entity 1" };
            var entity2 = new TestEntity { Id = 2, Name = "Entity 2" };

            repository.Add(entity1);
            repository.Add(entity2);

            var allItems = repository.GetAll().ToList();

            Assert.AreEqual(2, allItems.Count);
            CollectionAssert.Contains(allItems, entity1);
            CollectionAssert.Contains(allItems, entity2);
        }

        [TestMethod]
        public void Where_EntitiesMatchPredicate()
        {
            var entity1 = new TestEntity { Id = 1, Name = "Billy" };
            var entity2 = new TestEntity { Id = 2, Name = "Bob" };
            var entity3 = new TestEntity { Id = 3, Name = "Billy" };

            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);

            var filteredItems = repository.Where(e => e.Name == "Billy").ToList();

            Assert.AreEqual(2, filteredItems.Count);
            Assert.IsTrue(filteredItems.Contains(entity1));
            Assert.IsTrue(filteredItems.Contains(entity3));
        }

		#endregion
	}
}