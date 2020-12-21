using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FormFileUpload.Models
{
    public interface IDataAccessObject : IDisposable
    {
        Task<TObject> GetByIdAsync<TObject>(Guid id) where TObject : IEntity;

        Task<IEnumerable<TObject>> GetAllAsync<TObject>() where TObject : IEntity;

        Task AddAsync<TObject>(TObject entity) where TObject : IEntity;

        Task UpdateByIdAsync<TObject>(Guid id, TObject entity) where TObject : IEntity;

        Task<IEnumerable<TObject>> FindBySpecificationAsync<TObject>(Expression<Func<TObject, bool>> expr) where TObject : IEntity;

        Task DeleteByIdAsync<TObject>(Guid id) where TObject : IEntity;
    }
}
