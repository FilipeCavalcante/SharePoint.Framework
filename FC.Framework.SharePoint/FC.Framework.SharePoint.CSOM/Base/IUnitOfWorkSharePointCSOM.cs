using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.SharePoint.Client;

namespace FC.Framework.SharePoint.CSOM.Base
{
    public interface IUnitOfWorkSharePointCSOM<TEntity>
    {
        ClientContext Context { get; }
        Web ContextWeb { get; }
        void LoadAndExecute<T>(T clientObject);
        TEntity GetById(int itemId);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Where(CamlQuery query);
        bool Delete(int itemId);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
