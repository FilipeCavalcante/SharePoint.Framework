using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMPSFC.Framework.SharePoint
{
    public interface ISPRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll(bool allmetadata);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetBy(SPQuery query);


        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        
        
        void Delete(int id);
        void Add(IList<TEntity> entities);
        void Update(IList<TEntity> entities);
        void Delete(IList<TEntity> entities);
    }
}
