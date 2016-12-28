using PI.Framework.SharePoint.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PI.Framework.SharePoint.Utilities
{
    public static class Helper
    {
        public static Guid GetFieldName<T>(string property)
        {
            var type = typeof(T);
            var prop = type.GetProperty(property);
            var attr = prop.GetCustomAttributes(typeof(FieldMapperAttribute), true);
            return new Guid(((FieldMapperAttribute)attr.First()).InternalName);
        }

        public static FieldMapperAttribute GetFieldAttribute<T>(string property)
        {
            var type = typeof(T);
            var prop = type.GetProperty(property);
            var attr = prop.GetCustomAttributes(typeof(FieldMapperAttribute), true);
            return (attr.FirstOrDefault(f => f.ToString().Contains("FieldMapperAttribute")) as FieldMapperAttribute);
        }

        public static ListMapperAttribute GetClassAttribute<T>()
        {
            var type = typeof(T);
            var attrs = Attribute.GetCustomAttributes(type);
            return ((ListMapperAttribute)attrs.First());
        }

        public static T GetValue<T>(object data)
        {
            return GetValue((T)data);
        }

        public static T GetValue<T>(T data)
        {
            Type type = typeof(T);
            T value = default(T);

            switch (type.Name)
            {
                case "Object":
                    {
                        value = (T)data;
                    }
                    break;
                case "String":
                    {
                        value = (T)data;
                    }
                    break;
                default:
                    {
                        object result = null;
                        var methodInfo = (from m in type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                                          where m.Name == "TryParse"
                                          select m).FirstOrDefault();

                        if (methodInfo == null)
                            throw new ApplicationException(string.Format("Cannot found 'TryParse' method for '{0}'", type.ToString()));

                        result = methodInfo.Invoke(null, new object[] { data.ToString(), value });

                        if ((result != null) && ((bool)result))
                        {
                            methodInfo = (from m in type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                                          where m.Name == "Parse"
                                          select m).FirstOrDefault();

                            if (methodInfo == null)
                                throw new ApplicationException(string.Format("Cannot found method 'TryParse' for '{0}'", type.ToString()));

                            value = (T)methodInfo.Invoke(null, new object[] { data.ToString() });

                        }
                    }
                    break;
            }


            return (T)value;

        }

        public static string RetrieveSPFieldRef<T>()
        {
            var strBuilder = new StringBuilder();

            var _entity = typeof(T);
            var properties = _entity.GetProperties();

            foreach (var prop in properties)
            {
                var attr = GetFieldAttribute<T>(prop.Name);
                if (attr != null)
                    if (attr.InternalName != null)
                        strBuilder.Append($"<FieldRef Name='{attr.InternalName}' />");
            }

            return strBuilder.ToString();
        }
    }
}
