using PI.Framework.SharePoint.Mapper;
using PI.Framework.SharePoint.Utilities;
using Microsoft.SharePoint;
using System;

namespace PI.Framework.SharePoint.Extensions
{
    public static class SPListItemExtensions
    {
        public static T GetValue<T>(this SPListItem item, Guid field)
        {
            if (item == null)
                return default(T);

            var value = item[field];
            return Helper.GetValue<T>(value);
        }
        public static object GetValue(this SPListItem item, FieldMapperAttribute attributes)
        {
            try
            {
                return item[attributes.InternalName];
            }
            catch
            {
                //TODO: LogService.AddLog(ex.Message, LogType.Warning);
                return null;
            }
        }
        public static T GetValue<T>(this SPListItem item, string internalName)
        {
            var value = item[internalName];
            return Helper.GetValue<T>(value);
        }
        public static void SetValue<TEntity>(this SPListItem item, TEntity entity)
        {
            var _entity = entity.GetType();
            var properties = _entity.GetProperties();
            foreach (var property in properties)
            {
                var propInfo = _entity.GetProperty(property.Name);
                if (propInfo.GetValue(entity) != null)
                {
                    var attr = Helper.GetFieldAttribute<TEntity>(property.Name);
                    if (attr != null)
                        if (attr.ReadOnly == false)
                            item[attr.InternalName] = propInfo.GetValue(entity);
                }
            }
        }
    }
}
