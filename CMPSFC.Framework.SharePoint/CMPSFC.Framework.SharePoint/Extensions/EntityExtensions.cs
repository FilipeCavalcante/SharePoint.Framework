using PI.Framework.SharePoint.Utilities;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace PI.Framework.SharePoint.Extensions
{
    public static class EntityExtensions
    {
        public static void Load<TEntity>(this TEntity entity, SPListItem item)
        {
            var _web = item.Web;
            var _entity = entity.GetType();
            var properties = _entity.GetProperties();

            foreach (var prop in properties)
            {
                var attr = Helper.GetFieldAttribute<TEntity>(prop.Name);
                if (attr != null)
                {
                    var propInfo = _entity.GetProperty(prop.Name);
                    var value = item.GetValue(attr);

                    if (value != null)
                    {
                        switch (attr.ColumnType)
                        {
                            case SPFieldType.User:
                                {
                                    if (attr.DataType.Name == "SPFieldUserValue")
                                    {
                                        var uservalue = new SPFieldUserValue(item.Web, value.ToString());
                                        propInfo.SetValue(entity, uservalue, null);
                                    }
                                    else
                                    {
                                        var userCollection = new SPFieldUserValueCollection();
                                        var ids = new List<int>();
                                        foreach (var userId in ids)
                                            userCollection.Add(new SPFieldUserValue(_web, string.Empty)); //TODO: Criar a função para retornar vários usuários 

                                        propInfo.SetValue(entity, userCollection, null);
                                    }
                                }
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
