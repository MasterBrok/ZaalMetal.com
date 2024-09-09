using System.Net;
using Framework.Application;
using Music.Contract.Contracts;
using Music.Contract.ViewModels.BandViewModels;
using Music.Domain.Entities.BandAgg;

namespace Music.Application;

public class BandApplication : IBandApplication
{
    private readonly IBandRepository _bandRepository;
    public BaseResult result { get; set; }

    public BandApplication(IBandRepository bandRepository)
    {
        _bandRepository = bandRepository;
    }

    public async Task<BaseResult> Add(NewBandViewModel entity)
    {
        if (await _bandRepository.AnyEntityAsync(e => e.Title == entity.Name))
            return result.Set(HttpStatusCode.BadRequest).Failed(OperationMessage.Duplicated);

        var tag = Tools.GenerateTag();
        var slug = entity.Name.Slugify();

        var model = new BandEntity(entity.Name, entity.Bio, entity.FormationDate, entity.DissolvedTimeAt, tag, slug, entity.IconString);
        _bandRepository.AddEntity(model);
        return (BaseResult)await _bandRepository.SaveChangesAsync();
    }

    public async Task<BaseResult> Update(EditBandViewModel entity)
    {
        var model = await _bandRepository.FindAsync(e => e.Id == entity.Id);

        if (model is null)
            return result.Set(HttpStatusCode.NotFound).Failed(OperationMessage.NotFound);

        model.Edit(entity.Name, entity.Bio, entity.FormationDate, entity.DissolvedTimeAt, entity.IconString);
        return (BaseResult)await _bandRepository.SaveChangesAsync();
    }

    public async Task<OperationResult<EditBandViewModel>> GetDetail(SearchBandViewModel search)
    {
        var find = await _bandRepository.FindAsync(e => e.Id == search.Id,isTrack:false);

        var op = new OperationResult<EditBandViewModel>();

        if (find is null)
            return op.NotFound();

        op.Data = new()
        {
            Bio = find.Bio,
            DissolvedTimeAt = find.DissolvedTimeAt,
            FormationDate = find.FormationDate,
            Id = find.Id,
            Name = find.Title,
        };
        return op.Done();
    }

    public async Task<OperationResult<List<BandViewModel>>> ToViews(SearchBandViewModel search, PaginationParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var op = new OperationResult<List<BandViewModel>>();

        op.Data = await
            _bandRepository.ToViewsAsync(e => e.Id == search.Id || e.Title == search.Name,
                e => new BandViewModel
                {
                    Id = e.Id,
                    Name = e.Title,
                    Tag = e.Tag,
                    CreateTimeAt = e.CreationTimeAt.ToString("G"),

                }, parameters: parameters, token: cancellationToken);

        return op.What((int)HttpStatusCode.NoContent);
    }

    public async Task<OperationResult<List<KeyValueViewModel<string>>>> ToShort(SearchBandViewModel search, PaginationParameters parameters, CancellationToken cancellation = default)
    {
        var op = new OperationResult<List<KeyValueViewModel<string>>>();

        op.Data = await _bandRepository.ToViewsAsync(e => search.Name.IsNotNull() && e.Title == search.Name,
             selectExpression: e => new KeyValueViewModel<string>(e.Id, e.Title), parameters: parameters, token: cancellation);
        return op.What((int)HttpStatusCode.NoContent);
    }
}