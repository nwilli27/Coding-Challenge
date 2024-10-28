using ClarkCodingChallenge.DataAccess.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.DataAccess.Implementations
{
	public class InMemoryRepository<T> : IRepository<T>
	{
		#region Members

		private readonly List<T> items = new List<T>();

		#endregion

		#region IRepository

		public void Add(T entity)
		{
			this.items.Add(entity);
		}

		public IEnumerable<T> Where(Func<T, bool> predicate)
		{
			return this.items.Where(predicate);
		}

		public IEnumerable<T> GetAll()
		{
			return this.items;
		}

		#endregion
	}
}
