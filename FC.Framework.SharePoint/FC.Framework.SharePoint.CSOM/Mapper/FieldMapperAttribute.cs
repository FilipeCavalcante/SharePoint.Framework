using System;
using Microsoft.SharePoint.Client;

namespace FC.Framework.SharePoint.CSOM.Mapper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldMapperAttribute : Attribute
    {
        public string InternalName { get; set; }
        public FieldType Type { get; set; } = FieldType.Text;
        public bool Required { get; set; } = false;
        public bool ReadOnly { get; set; } = false;
        public Guid Id { get; set; }
    }
}
