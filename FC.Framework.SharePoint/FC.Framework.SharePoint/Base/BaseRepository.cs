using FC.Framework.SharePoint.Mapper;
using System;
using System.Linq;

namespace FC.Framework.SharePoint.Base
{
    public class BaseRepository
    {
        public ListMapperAttribute ListAttribute<T>()
        {
            var type = typeof(T);
            var attrs = Attribute.GetCustomAttributes(type);
            return ((ListMapperAttribute)attrs.First());
        }
    }
}
