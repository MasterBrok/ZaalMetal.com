using System.Linq.Expressions;
using Framework.Application;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Framework.Infrastructure;

public class RepositoryBase<TModel> : IRepositoryBase<TModel> where TModel : EntityBase
{
    public readonly DbContext _context;

    public RepositoryBase(DbContext context)
    {
        _context = context;
    }
    public DbState AddEntity(TModel? model)
    {
        return _context.Set<TModel>().Add(model).ConvertEntityEntryToDbState();
    }

    public DbState RemoveEntity(TModel? model)
    {
        return _context.Set<TModel>().Remove(model).ConvertEntityEntryToDbState();
    }

    public DbState AddRangeEntity(IEnumerable<TModel>? model)
    {
        _context.Set<TModel>().AddRange(model);
        return DbState.Added;
    }

    public DbState UpdateEntity(TModel? model)
    {
        _context.Entry(model).State = EntityState.Modified;
        return DbState.Updated;
    }

    public DbState DeleteEntity(TModel? model)
    {
        return _context.Set<TModel>().Remove(model).ConvertEntityEntryToDbState();
    }

    public async Task<string?> GetId(Expression<Func<TModel?, bool>> predicate, CancellationToken token = default)
    {
        return await _context.Set<TModel>().Where(predicate).Select(e => e.Id).SingleOrDefaultAsync(cancellationToken: token);
    }

    public Task<bool> AnyEntityAsync(Expression<Func<TModel, bool>> anyExpression, CancellationToken cancellationToken = default)
    {
        return _context.Set<TModel>().AnyAsync(anyExpression, cancellationToken);
    }
    public TModel? Find(Expression<Func<TModel, bool>> findExpression)
    {
        return _context.Set<TModel>().FirstOrDefault(findExpression);
    }
    public async Task<TModel?> FindAsync(Expression<Func<TModel, bool>> findExpression)
    {
        return await _context.Set<TModel>().FirstOrDefaultAsync(findExpression);
    }




    private IQueryable<TResult?> GetQueryable<TResult>(Expression<Func<TModel, bool>>? predicate = null,
        Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null,
        Expression<Func<TModel, TResult>>? selectExpression = null,
        PaginationParameters parameters = null,
        Expression<Func<TModel, object>>? orderExpression = null
            )
    {
        IQueryable<TModel?> query = _context.Set<TModel>();

        if (include is not null) query = include(query);
        if (orderExpression is not null) query = query.OrderByDescending(orderExpression);

        if (parameters is not null)
        {
            int skip = (parameters.CurrentPage - 1) * parameters.ItemPerPage;

            query = query.Skip(skip).Take(parameters.ItemPerPage);
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return query.Select(selectExpression);

    }


    private IQueryable<TModel?> GetQueryable(Expression<Func<TModel, bool>>? predicate = null,
        Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null)
    {
        IQueryable<TModel?> query = _context.Set<TModel>();

        if (include is not null) query = include(query);

        if (predicate is not null) query = query.Where(predicate);
        return query;

    }


    public List<TViewTModel> ToViews<TViewTModel>(Expression<Func<TModel, bool>>? whereExpression
        , Expression<Func<TModel, TViewTModel>>? selectExpression, Expression<Func<TModel, object>>? orderExpression = null)
    {
        IQueryable<TModel> query = To(whereExpression, orderExpression, null);

        if (selectExpression is null)
            return new List<TViewTModel>(0);
        return query.Select(selectExpression).ToList();
    }


    public async Task<List<TViewTModel?>> ToViewsWithInclude<TViewTModel>(
        Expression<Func<TModel, TViewTModel>>? selectExpression = null,
        Expression<Func<TModel, bool>>? whereExpression = null,
        Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null,
        Expression<Func<TModel, object>>? orderExpression = null,
        PaginationParameters parameters = null,
        CancellationToken cancellationToken = default)
    {
        if (selectExpression is null)
            return new List<TViewTModel>(0);

        IQueryable<TViewTModel?>? query = GetQueryable(whereExpression, include, selectExpression, parameters, orderExpression);

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<TResult?> FindAsync<TResult>(Expression<Func<TModel, bool>>? findExpression = null,
        Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null,
        Expression<Func<TModel, TResult>>? selectExpression = null,

        CancellationToken cancellationToken = default)
    {
        IQueryable<TResult?> queryable = GetQueryable<TResult>(findExpression, include, selectExpression);

        return await queryable.FirstOrDefaultAsync(cancellationToken);

    }

    public async Task<TModel?> FindAsync(Expression<Func<TModel, bool>>? findExpression = null,
        Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>? include = null,
        bool isTrack = true,
         CancellationToken cancellationToken = default)
    {
        IQueryable<TModel?> query = GetQueryable(findExpression, include);
        if (!isTrack)
            return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }


    //public async Task<List<TViewTModel>> ToViewsWithInclude<TViewTModel>(Expression<Func<TModel, bool>>? whereExpression,
    //    Expression<Func<TModel, TViewTModel>>? selectExpression, CancellationToken cancellationToken,
    //    params Expression<Func<TModel, object>>[] includesExpression)
    //{
    //    IQueryable<TModel> query = _context.Set<TModel>().AsQueryable();

    //    foreach (var include in includesExpression)
    //    {
    //        query = query.Include(include);
    //    }
    //    if (whereExpression is not null)
    //        query = query.Where(whereExpression);

    //    return await query.Select(selectExpression).ToListAsync(cancellationToken);
    //}


    public async Task<List<TModel>> ToListAsync(Expression<Func<TModel, bool>>? whereExpression, CancellationToken token = default)
    {
        return whereExpression is null ? await _context.Set<TModel>().ToListAsync(cancellationToken: token) : await _context.Set<TModel>().Where(whereExpression).ToListAsync(cancellationToken: token);
    }
    public List<TModel> ToList(Expression<Func<TModel, bool>>? whereExpression)
    {
        return whereExpression is null ? _context.Set<TModel>().ToList() : _context.Set<TModel>().Where(whereExpression).ToList();
    }


    public async Task<int> CountAsync(Expression<Func<TModel, bool>>? countExpression, CancellationToken token = default)
    {
        if (countExpression is not null)
            return await _context.Set<TModel>().CountAsync(countExpression, token);
        return await _context.Set<TModel>().CountAsync(cancellationToken: token);
    }

    public async Task<List<TViewTModel>> ToViewsAsync<TViewTModel>(Expression<Func<TModel, bool>> whereExpression,
        Expression<Func<TModel, TViewTModel>>? selectExpression, CancellationToken? token = default,
        Expression<Func<TModel, object>>? orderExpression = null, PaginationParameters parameters = null)
    {
        IQueryable<TModel> query = To(whereExpression, orderExpression, parameters);
        if (selectExpression is null)
            return new List<TViewTModel>(0);

        return await query.Select(selectExpression).ToListAsync();
    }


    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            Console.WriteLine("C" + _context.ChangeTracker.Entries().Count());
            foreach (var e in _context.ChangeTracker.Entries())
            {
                Console.WriteLine($"Entity : {e.Entity.GetType().Name}");
            }

            return await _context.SaveChangesAsync() != 0 ? true : false;
        }
        catch (Exception e)
        {
            //Console.WriteLine("ex: " + e);
            //Console.WriteLine("\n\n\n");
            Console.WriteLine(e.InnerException?.Message);
            return false;
        }
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }

    public TResult? Find<TResult>(Expression<Func<TModel, bool>> whereExpression
        , Expression<Func<TModel, TResult>> selectExpression, Func<TResult, bool>? findFunc)
    {
        var query = _context.Set<TModel>().Where(whereExpression)
            .Select(selectExpression);
        if (findFunc is null)
            return query.FirstOrDefault();

        return query.FirstOrDefault(findFunc);
    }

    private IQueryable<TModel> To(Expression<Func<TModel, bool>>? whereExpression, Expression<Func<TModel, object>>? orderExpression, PaginationParameters parameters)
    {
        IQueryable<TModel> query = _context.Set<TModel>();

        if (parameters is not null)
        {
            int skip = (parameters.CurrentPage - 1) * parameters.ItemPerPage;
            query = query.Skip(skip).Take(parameters.ItemPerPage);
        }

        if (whereExpression is not null)
            query = _context.Set<TModel>().Where(whereExpression);

        if (orderExpression is not null)
            query = query.OrderByDescending(orderExpression);

        return query;
    }
}