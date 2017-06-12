using FC.Framework.SharePoint.Repository;

namespace FC.Framework.SharePoint.Tests.Entity
{
    public class TaskEntityRepository : SPRepository<TaskEntity>
    {
        public TaskEntityRepository(string weburl) : base(weburl) { }

    }
}
