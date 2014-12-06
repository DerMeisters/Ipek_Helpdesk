using Abp.Domain.Entities;
using Abp.EntityFramework.Repositories;

namespace Ipek_Helpdesk.EntityFramework.Repositories
{
    public abstract class Ipek_HelpdeskRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<Ipek_HelpdeskDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
    }

    public abstract class Ipek_HelpdeskRepositoryBase<TEntity> : Ipek_HelpdeskRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {

    }
}
