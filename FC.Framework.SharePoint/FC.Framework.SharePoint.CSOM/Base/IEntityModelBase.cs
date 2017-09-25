using System;
using System.IO;
using FC.Framework.SharePoint.CSOM.Mapper;

namespace FC.Framework.SharePoint.CSOM.Base
{
    public interface IEntityModelBase
    {
        [FieldMapper(InternalName = "ID", ReadOnly = true)]
        int Id { get; set; }
        [FieldMapper(InternalName = "Title")]
        string Title { get; set; }
        [FieldMapper(InternalName = "Created", ReadOnly = true)]
        DateTime Created { get; set; }
        [FieldMapper(InternalName = "Modified", ReadOnly = true)]
        DateTime Modified { get; set; }
    }
}