using System.ComponentModel.DataAnnotations;

namespace Framework.Application;

public record PaginationParameters
{
    /// <summary>
    /// 1
    /// </summary>
    [Required]
    public int CurrentPage { get; set; } = 1;

    /// <summary>
    /// 10
    /// </summary>
    [Required]
    public int ItemPerPage { get; set; } = 2;
}