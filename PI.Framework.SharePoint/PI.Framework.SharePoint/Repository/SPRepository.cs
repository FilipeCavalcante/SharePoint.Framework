using PI.Framework.SharePoint.Base;
using PI.Framework.SharePoint.Extensions;
using PI.Framework.SharePoint.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: CLSCompliant(true)]
namespace PI.Framework.SharePoint
{
    public class SPRepository<TEntity> : BaseRepository, IDisposable, ISPRepository<TEntity> where TEntity : global::PI.Framework.SharePoint.Base.SPBaseEntity, new()
    {
        #region PROPERTIES

        public SPWeb ParentWeb { get; private set; }
        public SPList ParentList { get; private set; }

        private string RelativeWebUrl
        {
            get
            {
                var relativeweburl = base.ListAttribute<TEntity>().RelativeUrl;
                return relativeweburl ?? "";
            }
        }
        private static string ListGuid { get; set; }

        #endregion

        #region CONSTRUCTORS
        public SPRepository(string webUrl = null, bool runWithElevatedPrivileges = false)
        {
            if (runWithElevatedPrivileges)
                SPSecurity.RunWithElevatedPrivileges(() => { InitRepository(webUrl?.ToString()); });
            else
                InitRepository(webUrl?.ToString());
        }
        public SPRepository(string weburl, Guid listguid, bool runWithElevatedPrivileges = false) : this(weburl, runWithElevatedPrivileges)
        {

        }
        public SPRepository(string weburl, string listname, bool runWithElevatedPrivileges = false) : this(weburl, runWithElevatedPrivileges)
        {

        }

        #endregion

        #region CUSTOM METHODS
        private void InitRepository(string weburl)
        {
            try
            {
                ListGuid = ListAttribute<TEntity>().ListGuid;
                ParentWeb = new SPSite((weburl ?? SPContext.Current.Web.Url)).OpenWeb(RelativeWebUrl);
                ParentList = ParentWeb.Lists.Cast<SPList>().FirstOrDefault(f => f.ID == Guid.Parse(ListGuid));
            }
            catch
            {
                throw;
            }
        }
        protected virtual IEnumerable<TEntity> GetEntities(SPListItemCollection items)
        {
            foreach (SPListItem item in items)
                yield return GetEntity(item);

        }
        protected virtual TEntity GetEntity(SPListItem item)
        {
            if (item == null)
                return null;

            TEntity entity = new TEntity();

            entity.Id = item.ID;
            entity.ParentItem = item;
            entity.Load(item);

            return entity;
        }
        protected virtual void Map(TEntity entity, ref SPListItem item)
        {
            item.SetValue<TEntity>(entity);
        }
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

        public IEnumerable<TEntity> GetAll(bool allmetadata)
        {
            try
            {
                var query = new SPQuery();
                query.Query = @"<Where />";

                if (!allmetadata)
                {
                    query.ViewFieldsOnly = true;
                    query.ViewFields = Helper.RetrieveSPFieldRef<TEntity>();
                }

                return GetBy(query);
            }
            catch (Exception) { throw; }
        }

        public IEnumerable<TEntity> FindBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(false).AsQueryable().Where(predicate).AsEnumerable();
        }

        public IEnumerable<TEntity> GetBy(SPQuery query)
        {
            try
            {
                return GetEntities(ParentList.GetItems(query));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity Add(TEntity entity)
        {
            try
            {
                var item = New();
                Map(entity, ref item);
                item.Update();
                return GetEntity(item);
            }
            catch (Exception) { throw; }
        }

        private SPListItem New()
        {
            return ParentList.Items.Add();
        }

        public TEntity Update(TEntity entity)
        {
            try
            {
                var item = FindBy(f => f.Id == entity.Id).FirstOrDefault();
                if (item == null)
                    throw new Exception("Item not found");

                var toupdate = item.ParentItem;
                Map(entity, ref toupdate);
                toupdate.Update();
                return GetEntity(toupdate);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                ParentList.Items.DeleteItemById(id);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Add(IList<TEntity> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(IList<TEntity> entities)
        {
            foreach (var entityid in entities)
                Delete(entityid.Id);
        }

        public void Update(IList<TEntity> entities)
        {
            foreach (var entity in entities)
                Update(entity);
        }

        #endregion
    }
}
