using PI.Framework.SharePoint.Mapper;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Framework.SharePoint.Base
{
    public class SPBaseEntity
    {
        [FieldMapper(ReadOnly = true)]
        public int Id { get; set; }

        [FieldMapper(InternalName = "Title", DataType = typeof(String), ColumnType = SPFieldType.Text, ReadOnly = false)]
        public string Title { get; set; }

        [FieldMapper(InternalName = "Created", DataType = typeof(DateTime), ColumnType = SPFieldType.DateTime, ReadOnly = true)]
        public DateTime Created { get; set; }

        [FieldMapper(InternalName = "Modified", DataType = typeof(DateTime), ColumnType = SPFieldType.DateTime, ReadOnly = true)]
        public DateTime Modified { get; set; }

        [FieldMapper(InternalName = "Author", DataType = typeof(SPFieldUserValue), ColumnType = SPFieldType.User, ReadOnly = true)]
        public SPFieldUserValue Author { get; set; }

        [FieldMapper(InternalName = "Editor", DataType = typeof(SPFieldUserValue), ColumnType = SPFieldType.User, ReadOnly = true)]
        public SPFieldUserValue Editor { get; set; }

        [FieldMapper(ReadOnly = true, IsSystemProperty = true)]
        public SPListItem ParentItem { get; set; }

    }
}
