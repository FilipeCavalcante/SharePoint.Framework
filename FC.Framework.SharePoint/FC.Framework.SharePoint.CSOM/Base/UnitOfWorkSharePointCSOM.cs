using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security;
using FC.Framework.SharePoint.CSOM.Extensions;
using Microsoft.SharePoint.Client;

namespace FC.Framework.SharePoint.CSOM.Base
{
    public class UnitOfWorkSharePointCSOM<TEntity> : ClientContext, IUnitOfWorkSharePointCSOM<TEntity> where TEntity : new()
    {
        public UnitOfWorkSharePointCSOM(string webFullUrl) : base(webFullUrl)
        {
            Context = this;
            ContextWeb = this.Web;
            LoadAndExecute(ContextWeb);
        }

        public UnitOfWorkSharePointCSOM(Uri webFullUrl) : base(webFullUrl)
        {
        }

        public UnitOfWorkSharePointCSOM(string webFullUrl, string username, string password) : base(webFullUrl)
        {
            var secureString = new SecureString();
            foreach (var c in password)
                secureString.AppendChar(c);

            Credentials = new SharePointOnlineCredentials(username, secureString);
            Context = this;
            ContextWeb = this.Web;
            LoadAndExecute(ContextWeb);
        }

        public ClientContext Context { get; }
        public Web ContextWeb { get; }

        public void LoadAndExecute<T>(T clientObject)
        {
            Load(clientObject as ClientObject);
            ExecuteQuery();
        }

        public TEntity GetById(int itemId)
        {
            //TODO: criar atributos para mapear nomenclatura de listas;
            var list = Web.Lists.GetByTitle(string.Empty);
            var listItem = list.GetItemById(itemId);
            LoadAndExecute(listItem);

            //TODO: Criar função de parse das entidades;
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            var itemCollections = Web.Lists.GetByTitle("Blacklist").GetItems(new CamlQuery());
            LoadAndExecute(itemCollections);
            return GetEntities(itemCollections);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Where(CamlQuery query)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int itemId)
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

        internal virtual IEnumerable<TEntity> GetEntities(ListItemCollection collection)
        {
            foreach (var item in collection)
                yield return GetEntity(item);
        }

        private TEntity GetEntity(ListItem item)
        {
            var entity = new TEntity();
            entity.Load(item);
            return entity;
        }
    }
}
