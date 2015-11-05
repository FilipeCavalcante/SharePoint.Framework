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
        IEnumerable<TEntity> Retrieve();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Retrieve(SPQuery query);


        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(int id);


        void Add(IList<TEntity> entities);
        void Update(IList<TEntity> entities);
        void Delete(IList<int> entities);
    }
}
