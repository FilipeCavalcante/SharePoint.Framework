﻿using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FC.Framework.SharePoint
{
    public interface ISPRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll(bool allmetadata);
        IEnumerable<TEntity> GetAll(SPQuery query, bool allmetadata);
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
