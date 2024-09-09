using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Framework.Domain;
using Microsoft.AspNetCore.Http;

namespace Music.Contract.ViewModels.ArtistViewModel;

public record AddArtistViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? NickName { get; set; }
    public string? Bio { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; } = null;
    public State State { get; set; }


    public IFormFileCollection Pictures { get; set; }

    [JsonIgnore] public string FullName => LastName + FirstName;
}