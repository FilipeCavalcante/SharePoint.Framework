using CMPSFC.Framework.SharePoint.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPSFC.Framework.SharePoint.Base
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
