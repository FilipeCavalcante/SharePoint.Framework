using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPSFC.Framework.SharePoint.Base
{
    public class SPBaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public object Author { get; set; }
        public object Editor { get; set; }

    }
}
