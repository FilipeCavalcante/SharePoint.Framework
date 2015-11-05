using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: CLSCompliant(true)]
namespace CMPSFC.Framework.SharePoint
{
    public class SPRepository<TEntity> : IDisposable, ISPRepository<TEntity> where TEntity : global::CMPSFC.Framework.SharePoint.Base.SPBaseEntity, new()
    {

        #region PROPERTIES
        public SPWeb ParentWeb { get; private set; }
        public SPList ParentList { get; private set; }
        private static string RelativeWebUrl { get { return ""; } }
        private static Guid ListGuid { get { return Guid.NewGuid(); } }
        #endregion

        #region CONSTRUCTORS
        public SPRepository()
        {


        }
        public SPRepository(string weburl, Guid listguid)
        {
            ParentWeb = new SPSite(weburl).OpenWeb(RelativeWebUrl);

            ParentList = this.ParentWeb.Lists.Cast<SPList>().FirstOrDefault(f => f.ID == listguid);

            if (ParentList == null)
                throw new SPException(string.Format("The list '{0}' (GUID: '{1}') cannot be found on web '{2}'", listguid, RelativeWebUrl));

        }
        public SPRepository(object listidentifier) { }
        public SPRepository(bool elevateprivileges) { }
        public SPRepository(SPWeb web, bool elevateprivileges) { }
        public SPRepository(SPWeb web, string listname, bool elevateprivileges) { }
        #endregion

        #region METHODS
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

        public IEnumerable<TEntity> Retrieve()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Retrieve(SPQuery query)
        {
            throw new NotImplementedException();
        }

        public TEntity Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(IList<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(IList<int> entities)
        {
            throw new NotImplementedException();
        }


        public void Update(IList<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
