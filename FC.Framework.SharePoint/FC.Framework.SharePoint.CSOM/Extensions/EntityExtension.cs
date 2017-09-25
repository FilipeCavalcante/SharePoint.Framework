using System;
using System.Linq;
using FC.Framework.SharePoint.CSOM.Utilities;
using Microsoft.SharePoint.Client;

namespace FC.Framework.SharePoint.CSOM.Extensions
{
    public static class EntityExtension
    {
        public static void Load<TEntity>(this TEntity entity, ListItem item)
        {
            var entityType = entity.GetType();
            if (entityType.BaseType?.Name != $"EntityModelBase") throw new Exception($"TEntity is derived from 'EntityModelBase'");
            if (item == null)
                return;

            var properties = entityType.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var attrs = Helper.GetFieldAttribute<TEntity>(propertyInfo.Name);
                var fieldName = attrs?.InternalName ?? propertyInfo.Name;
                if (item.FieldValues.Any(s => s.Key == fieldName))
                {
                    var value = item[fieldName];
                    if (value == null) continue;
                    var property = entityType.GetProperty(propertyInfo.Name);
                    switch (propertyInfo.PropertyType.Name)
                    {
                        default:
                            property?.SetValue(entity, value, null);
                            break;
                    }
                }
            }
        }
    }
}
