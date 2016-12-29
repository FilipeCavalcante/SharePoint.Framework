using PI.Framework.SharePoint.Utilities;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.Linq;

namespace PI.Framework.SharePoint.Extensions
{
    public static class EntityExtensions
    {
        public static void Load<TEntity>(this TEntity entity, SPListItem item)
        {
            var _entity = entity.GetType();
            if (_entity.BaseType.Name != "SPBaseEntity")
                return;
            if (item == null)
                return;

            var properties = _entity.GetProperties();
            foreach (var prop in properties)
            {
                var attrs = Helper.GetFieldAttribute<TEntity>(prop.Name);
                var _fieldName = attrs?.InternalName ?? prop.Name;
                if (item.Fields.Cast<SPField>().Any(f => f.InternalName == _fieldName))
                {
                    var value = item[_fieldName];
                    if (value != null)
                    {
                        var propInfo = _entity.GetProperty(prop.Name);
                        switch (prop.PropertyType.Name)
                        {
                            case "SPFieldUserValue":
                                propInfo.SetValue(entity, new SPFieldUserValue(item.Web, value?.ToString()), null);
                                break;
                            default:
                                propInfo.SetValue(entity, value, null);
                                break;
                        }
                    }
                }
            }
        }
    }
}
