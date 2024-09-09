using System.Linq.Expressions;
using Framework.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Framework.Infrastructure;
public interface IRepositoryBase<TEntityType>
{
    DbState AddEntity(TEntityType? model);
    DbState RemoveEntity(TEntityType? model);
    DbState AddRangeEntity(IEnumerable<TEntityType>? model);
    DbState UpdateEntity(TEntityType? model);
    DbState DeleteEntity(TEntityType? model);
    Task<string?> GetId(Expression<Func<TEntityType?, bool>> predicate, CancellationToken token = default);

    Task<bool> AnyEntityAsync(Expression<Func<TEntityType, bool>> anyExpression, CancellationToken cancellationToken = default);

    TResult? Find<TResult>(Expression<Func<TEntityType, bool>> whereExpression
        , Expression<Func<TEntityType, TResult>> selectExpression, Func<TResult, bool>? findFunc);

    //Task<TResult?> FindAsync<TResult>(Expression<Func<TEntityType, bool>> whereExpression
    //    , Expression<Func<TEntityType, TResult>> selectExpression, Expression<Func<TResult?, bool>>? findFunc);
    TEntityType? Find(Expression<Func<TEntityType, bool>> findExpression);
    //Task<TEntityType?> FindAsync(Expression<Func<TEntityType, bool>> findExpression);

    List<TViewModel> ToViews<TViewModel>(Expression<Func<TEntityType, bool>> whereExpression,
        Expression<Func<TEntityType, TViewModel>> selectExpression, Expression<Func<TEntityType, object>>? orderExpression = null);

    //public Task<List<TViewTEntityType>> ToViewsWithInclude<TViewTEntityType>(Expression<Func<TEntityType, bool>>? whereExpression, Expression<Func<TEntityType, TViewTEntityType>>? selectExpression, CancellationToken cancellationToken,
    //    params Expression<Func<TEntityType, object>>[] includesExpression);
    Task<List<TViewModel>> ToViewsAsync<TViewModel>(Expression<Func<TEntityType, bool>> whereExpression,
        Expression<Func<TEntityType, TViewModel>> selectExpression, CancellationToken? token = default,
        Expression<Func<TEntityType, object>>? orderExpression = null, PaginationParameters parameters = null);

    //Task<List<TEntityType>> ToListAsync(Expression<Func<TEntityType, bool>>? whereExpression, CancellationToken token = default);
    List<TEntityType> ToList(Expression<Func<TEntityType, bool>>? whereExpression);

    Task<List<TViewTModel?>> ToViewsWithInclude<TViewTModel>(
        Expression<Func<TEntityType, TViewTModel>>? selectExpression = null,
        Expression<Func<TEntityType, bool>>? whereExpression = null,
        Func<IQueryable<TEntityType>, IIncludableQueryable<TEntityType, object>>? include = null,
        Expression<Func<TEntityType, object>>? orderExpression = null,
        PaginationParameters parameters = null,
        CancellationToken cancellationToken = default);

    Task<TResult?> FindAsync<TResult>(
         Expression<Func<TEntityType, bool>>? findExpression = null,
         Func<IQueryable<TEntityType>, IIncludableQueryable<TEntityType, object>>? include = null,
         Expression<Func<TEntityType, TResult>>? selectExpression = null,
         CancellationToken cancellationToken = default);
   
    Task<TEntityType?> FindAsync(
        Expression<Func<TEntityType, bool>>? findExpression = null,
        Func<IQueryable<TEntityType>, IIncludableQueryable<TEntityType, object>>? include = null,
        bool isTrack = true,
        CancellationToken cancellationToken = default);


    Task<int> CountAsync(Expression<Func<TEntityType, bool>>? countExpression, CancellationToken token = default);

    Task<bool> SaveChangesAsync();
    bool SaveChanges();

}