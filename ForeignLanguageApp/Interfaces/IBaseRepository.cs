using System.Collections.Generic;

namespace ForeignLanguageApp.Interfaces
{
    public interface IBaseRepository<T> 
    {
        List<T> GetAll(); 

        public void Update(T entity); 

        public void Add(T entity); 

        public void Delete(T entity); 
    }
}
