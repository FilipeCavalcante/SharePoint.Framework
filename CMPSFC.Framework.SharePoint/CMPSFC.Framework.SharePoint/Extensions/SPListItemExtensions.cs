using PI.Framework.SharePoint.Mapper;
using PI.Framework.SharePoint.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Framework.SharePoint.Extensions
{
    public static class SPListItemExtensions
    {
        public static T GetValue<T>(this SPListItem item, Guid fieldUid)
        {
            var value = item[fieldUid];
            return Helper.GetValue<T>(value);
        }
        public static object GetValue(this SPListItem item, FieldMapperAttribute attributes)
        {
            try
            {
                return item[attributes.InternalName];
            }
            catch (Exception ex)
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
                        if (attr.ReadOnly == null || attr.ReadOnly == false)
                            item[attr.InternalName] = propInfo.GetValue(entity);
                }
            }
        }
    }
}
