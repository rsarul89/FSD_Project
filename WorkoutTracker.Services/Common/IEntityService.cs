using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Services
{
    public interface IEntityService<TEntity> : IService where TEntity : class
    {
        TEntity Create(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity Update(TEntity entity);
    }
}
