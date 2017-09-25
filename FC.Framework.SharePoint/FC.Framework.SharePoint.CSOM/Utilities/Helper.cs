using System.Linq;
using FC.Framework.SharePoint.CSOM.Mapper;

namespace FC.Framework.SharePoint.CSOM.Utilities
{
    public static class Helper
    {
        public static FieldMapperAttribute GetFieldAttribute<T>(string property)
        {
            var type = typeof(T);
            var prop = type.GetProperty(property);
            var attr = prop?.GetCustomAttributes(typeof(FieldMapperAttribute), true);
            return (attr?.FirstOrDefault(f => f.ToString().Contains("FieldMapperAttribute")) as FieldMapperAttribute);
        }
    }
}
