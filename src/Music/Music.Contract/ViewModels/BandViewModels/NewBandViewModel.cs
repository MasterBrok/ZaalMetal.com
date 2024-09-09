using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace Music.Contract.ViewModels.BandViewModels;

public record NewBandViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Bio { get; set; }
    public DateTime? DissolvedTimeAt { get; set; }
    public DateTime? FormationDate { get; set; }
    public IFormFile? Icon { get; set; }
    [JsonIgnore]
    public string? IconString { get; set; }
}