using PI.Framework.SharePoint.Mapper;
using System;
using System.Linq;

namespace PI.Framework.SharePoint.Base
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
