using FC.Framework.SharePoint.Mapper;
using FC.Framework.SharePoint.Utilities;
using Microsoft.SharePoint;
using System;
using System.Linq;

namespace FC.Framework.SharePoint.Extensions
{
    public static class SPListItemExtensions
    {
        public static void SetValue<TEntity>(this SPListItem item, TEntity entity)
        {
            var _entity = entity.GetType();
            if (_entity.BaseType.Name != "SPBaseEntity")
                return;

            var properties = _entity.GetProperties();
            foreach (var property in properties)
            {
                var propInfo = _entity.GetProperty(property.Name);
                var attrs = Helper.GetFieldAttribute<TEntity>(property.Name);
                if (item.Fields.Cast<SPField>().Any(f => f.InternalName == (attrs?.InternalName ?? property.Name)))
                    if (propInfo.GetValue(entity) != null)
                        if (attrs == null || !attrs.ReadOnly)
                            item[attrs?.InternalName ?? property.Name] = propInfo.GetValue(entity);
            }
        }
    }
}
