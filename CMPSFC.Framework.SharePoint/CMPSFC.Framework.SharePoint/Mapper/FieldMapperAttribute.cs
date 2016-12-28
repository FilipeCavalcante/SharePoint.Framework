using Microsoft.SharePoint;
using System;

namespace PI.Framework.SharePoint.Mapper
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FieldMapperAttribute : Attribute
    {
        public string InternalName { get; set; }
        public SPFieldType ColumnType { get; set; }
        public Type DataType { get; set; }
        public bool ReadOnly { get; set; } = false;
    }
}
