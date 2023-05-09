using System.Reflection;
using Base.Contracts;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Base.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class
    BaseEntityRepository<TDalEntity, TDomainEntity, TDbContext> : BaseEntityRepository<TDalEntity, TDomainEntity, Guid,
        TDbContext>
    where TDalEntity : DomainEntityMetaId<Guid>
    where TDomainEntity : DomainEntityMetaId<Guid>
    where TDbContext : DbContext
{
    public BaseEntityRepository(
        TDbContext dbContext,
        IMapper<TDalEntity, TDomainEntity> mapper
    ) : base(dbContext, mapper)
    {
    }
}

public class BaseEntityRepository<TDalEntity, TDomainEntity, TKey, TDbContext> : IEntityRepository<TDalEntity, TKey>
    where TDalEntity : DomainEntityMetaId<TKey>
    where TDomainEntity : DomainEntityMetaId<TKey>
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly IMapper<TDalEntity, TDomainEntity> Mapper;

    public BaseEntityRepository(
        TDbContext dbContext,
        IMapper<TDalEntity, TDomainEntity> mapper
    )
    {
        RepoDbContext = dbContext;
        RepoDbSet = dbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }

    protected virtual IQueryable<TDomainEntity> CreateQuery(bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public virtual TDalEntity Add(TDalEntity entity, TKey userId)
    {
        entity.CreatedBy  = userId;
        return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }
    public virtual TDalEntity Update(TDalEntity entity, TKey userId)
    {

        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = userId;
        
        return Mapper.Map(RepoDbSet.Update(Mapper.Map(entity)!).Entity)!;
    }


    public virtual TDalEntity Remove(TDalEntity entity, TKey userId)
    {
        entity.DeletedAt = DateTime.UtcNow;
        entity.DeletedBy  = userId;
        return  Update(entity, userId);
    }

    public virtual TDalEntity Remove(TKey id, TKey userId)
    {
        var entity = FirstOrDefault(id);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} was not found");
        }

        return Remove(entity, userId);
    }

    public virtual TDalEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return Mapper.Map(
            CreateQuery(noTracking)
                .FirstOrDefault(a => a.Id.Equals(id) &&a.DeletedAt == null)
        );
    }

    public virtual IEnumerable<TDalEntity> GetAll(bool noTracking = true)
    {
        return CreateQuery(noTracking)
            .ToList().Where(e => e.DeletedAt == null)
            .Select(x => Mapper.Map(x)!);
    }

    public virtual bool Exists(TKey id)
    {
        return RepoDbSet.Any(a => a.Id.Equals(id) &&a.DeletedAt == null);
    }

    public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        return Mapper.Map(
            await CreateQuery(noTracking).FirstOrDefaultAsync(a => a.Id.Equals(id) &&a.DeletedAt == null)
        );
    }

    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(bool noTracking = true)
    {
        return (
                await CreateQuery(noTracking).Where(e => e.DeletedAt == null)
                    .ToListAsync()
            )
            .Select(x => Mapper.Map(x)!);
    }

    public virtual async Task<bool> ExistsAsync(TKey id)
    {
        return await RepoDbSet.AnyAsync(a => a.Id.Equals(id) && a.Id.Equals(id) &&a.DeletedAt == null);
    }

    public virtual async Task<TDalEntity> RemoveAsync(TKey id, TKey userId)
    {
        var entity = await FirstOrDefaultAsync(id);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} was not found");
        }
        return Remove(entity, userId);
    }
}