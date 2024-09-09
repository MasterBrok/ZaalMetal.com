namespace Framework.Application;

public interface IApplication<in TAddViewModel, TEditViewModel, in TObjectSearch, TViewModel>
{
    BaseResult result { get; set; }
    Task<BaseResult> Add(TAddViewModel entity);
    Task<BaseResult> Update(TEditViewModel entity);
    Task<OperationResult<TEditViewModel>> GetDetail(TObjectSearch search);
    Task<OperationResult<List<TViewModel>>> ToViews(TObjectSearch search, PaginationParameters parameters,
        CancellationToken cancellationToken = default);

    Task<OperationResult<List<KeyValueViewModel<string>>>> ToShort(TObjectSearch search,PaginationParameters parameters,CancellationToken cancellation = default);
}