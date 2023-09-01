using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Interfaces
{
    public interface IRepository<T> 
        where T : class
    {
        IEnumerable<T>? GetAll();
        Task Create(T entity);
    }
}
