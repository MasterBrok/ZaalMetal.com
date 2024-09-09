using System.Net;
using Framework.Application;
using Microsoft.EntityFrameworkCore;
using Music.Contract.Contracts;
using Music.Contract.ViewModels.ArtistViewModel;
using Music.Domain.Entities.ArtistAgg;

namespace Music.Application;

public class ArtistApplication : IArtistApplication
{
    public BaseResult result { get; set; }


    private readonly IArtistRepository _artistRepository;

    public ArtistApplication(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<BaseResult> Add(AddArtistViewModel entity)
    {
        var slug = $"{entity.LastName}{entity.FirstName}".Slugify();

        var model = new ArtistEntity(entity.FirstName, entity.LastName, entity.NickName, entity.Bio, entity.DateOfBirth,
            entity.DateOfDeath, slug);

        _artistRepository.AddEntity(model);

        return (BaseResult)await _artistRepository.SaveChangesAsync();
    }

    public async Task<BaseResult> Update(EditArtistViewModel entity)
    {
        var find = await _artistRepository.FindAsync(e => e.Id == entity.Id);

        if (find is null)
            return result.NotFound();

        find.Edit(entity.FirstName, entity.LastName, entity.NickName, entity.Bio, entity.DateOfBirth,
            entity.DateOfDeath);

        return (BaseResult)await _artistRepository.SaveChangesAsync();
    }

    public async Task<OperationResult<EditArtistViewModel>> GetDetail(SearchArtistViewModel search)
    {
        var op = new OperationResult<EditArtistViewModel>();
        var find = await _artistRepository.FindAsync(e => e.Id == search.Id, isTrack: false);

        if (find is null)
            return op.NotFound();
        op.Data = new EditArtistViewModel
        {
            Bio = find.Bio,
            Id = find.Id,
            FirstName = find.FirstName,
            LastName = find.LastName,
            State = find.State,
            DateOfBirth = find.DateOfBirth,
            DateOfDeath = find.DateOfDeath,
            NickName = find.NickName
        };
        return op.Done();
    }

    public async Task<OperationResult<List<ArtistViewModel>>> ToViews(SearchArtistViewModel search, PaginationParameters parameters,
        CancellationToken cancellationToken = default)
    {
        var op = new OperationResult<List<ArtistViewModel>>();

        op.Data = await _artistRepository.ToViewsWithInclude(
            selectExpression: e => new ArtistViewModel
            {
                FullName = e.FullName,
                Id = e.Id,
                State = e.State,
                Image = e.Pictures.FirstOrDefault().Path
            }, whereExpression: e => e.LastName.StartsWith(search.LastName), include: e => e.Include(e => e.Pictures), e => e.CreationTimeAt, parameters, cancellationToken);

        return op.What((int)HttpStatusCode.NoContent);
    }

    public async Task<OperationResult<List<KeyValueViewModel<string>>>> ToShort(SearchArtistViewModel search, PaginationParameters parameters, CancellationToken cancellation = default)
    {
        var op = new OperationResult<List<KeyValueViewModel<string>>>();

        op.Data = await _artistRepository.ToViewsAsync(
            e => search.FistName.IsNotNull() ? e.FirstName.StartsWith(search.FistName) : e == null,
            selectExpression: e => new KeyValueViewModel<string>(e.Id, e.FullName), parameters: parameters, token: cancellation);
        return op.What((int)HttpStatusCode.NoContent);
    }

    public async Task<OperationResult<string>> GetFullName(string id)
    {
        var op = new OperationResult<string>();
        if (!await _artistRepository.AnyEntityAsync(e => e.Id == id))
            return op.NotFound();

        op.Data = await _artistRepository.FindAsync(e => e.Id == id, selectExpression: e => e.FirstName + e.LastName);

        return op.Succeeded();
    }
}