using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPSFC.Framework.SharePoint.Mapper
{
    public sealed class FieldMapperAttribute : Attribute
    {
        public string InternalName { get; set; }
        public SPFieldType ColumnType { get; set; }
        public Type DataType { get; set; }
        public bool ReadOnly { get; set; }
    }
}
