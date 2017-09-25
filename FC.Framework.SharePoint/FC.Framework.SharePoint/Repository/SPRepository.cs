using System;
using System.Collections.Generic;
using System.Linq;
using FC.Framework.SharePoint.Base;
using FC.Framework.SharePoint.Exceptions;
using FC.Framework.SharePoint.Extensions;
using FC.Framework.SharePoint.Utilities;
using Microsoft.SharePoint;

namespace FC.Framework.SharePoint.Repository
{
    public class SPRepository<TEntity> : BaseRepository, IDisposable, ISPRepository<TEntity> where TEntity : SPBaseEntity, new()
    {
        #region PROPERTIES

        public SPWeb ParentWeb { get; private set; }
        public SPList ParentList { get; private set; }

        private string RelativeWebUrl => ListAttribute<TEntity>()?.RelativeUrl ?? throw new ArgumentNullException(paramName:
                                             $"RelativeUrl", message: $"A classe não foi configurada corretamente.");
        private static string ListTitle { get; set; }

        #endregion

        #region CONSTRUCTORS
        public SPRepository(string webUrl = null, bool runWithElevatedPrivileges = false)
        {
            if (runWithElevatedPrivileges)
                SPSecurity.RunWithElevatedPrivileges(() => { InitRepository(webUrl); });
            else
                InitRepository(webUrl);
        }
        public SPRepository(string weburl, string listname, bool runWithElevatedPrivileges = false) : this(weburl, runWithElevatedPrivileges)
        {
            ListTitle = listname;
        }

        #endregion

        #region CUSTOM METHODS
        private void InitRepository(string weburl)
        {
            var listTitle = ListTitle ?? ListAttribute<TEntity>().ListTitle;
            ParentWeb = new SPSite((weburl ?? (SPContext.Current?.Web?.Url ?? throw new ArgumentNullException(message: "Não foi possível iniciar o SPSite com o contexto informado", paramName: $"WebUrl")))).OpenWeb(RelativeWebUrl);
            ParentList = ParentWeb.Lists.Cast<SPList>().First(f => f.Title == listTitle) ?? throw new ListNotFoundException();
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

            var entity = new TEntity
            {
                Id = item.ID,
                ParentItem = item
            };

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
            var query = new SPQuery { Query = @"<Where />" };

            if (!allmetadata)
            {
                query.ViewFieldsOnly = true;
                query.ViewFields = Helper.RetrieveSPFieldRef<TEntity>();
            }

            return GetBy(query);
        }
        public IEnumerable<TEntity> GetAll(SPQuery query = null, bool allmetadata = false)
        {
            var _query = query ?? new SPQuery() { Query = @"<Where />" };
            if (!allmetadata)
            {
                _query.ViewFieldsOnly = true;
                _query.ViewFields = Helper.RetrieveSPFieldRef<TEntity>();
            }

            return GetBy(_query);
        }

        public IEnumerable<TEntity> FindBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(false).AsQueryable().Where(predicate).AsEnumerable();
        }

        public IEnumerable<TEntity> GetBy(SPQuery query)
        {
            return GetEntities(ParentList.GetItems(query));
        }

        public TEntity Add(TEntity entity)
        {
            var item = New();
            Map(entity, ref item);
            item.Update();
            return GetEntity(item);
        }

        private SPListItem New()
        {
            return ParentList.Items.Add();
        }

        public TEntity Update(TEntity entity)
        {
            var item = FindBy(f => f.Id == entity.Id).FirstOrDefault();
            if (item == null)
                throw new Exception("Item not found");

            var toupdate = item.ParentItem;
            Map(entity, ref toupdate);
            toupdate.Update();
            return GetEntity(toupdate);
        }

        public void Delete(int id)
        {
            ParentList.Items.DeleteItemById(id);
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
