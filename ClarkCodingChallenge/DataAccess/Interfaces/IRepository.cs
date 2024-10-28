using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        IEnumerable<T> Where(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
    }
}
