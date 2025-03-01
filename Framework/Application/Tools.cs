﻿using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Framework.Application;

public static class Tools
{
    public static DbState ConvertEntityEntryToDbState(this EntityEntry entity)
    {
        switch (entity.State)
        {
            case EntityState.Added: return DbState.Added;
            case EntityState.Deleted: return DbState.Removed;
            case EntityState.Modified: return DbState.Updated;
            default:
                return DbState.None;
        }
    }

    public static string GenerateTag()
    {
        return Random.Shared.Next(100000,000000).ToString();
    }



    public static OperationResult<T> ParseResult<T>(this bool save, string message = "")
    {
        if (!save)
            return new OperationResult<T>().Failed(OperationMessage.Database).Set(HttpStatusCode.BadRequest);
        return new OperationResult<T>().Succeeded(message);
    }

    public static BaseResult ParseResult(this bool save, string message = OperationMessage.Done,string error = "")
    {
        if (!save)
            return new BaseResult().Set(HttpStatusCode.BadRequest).Failed(OperationMessage.Database,error);
        return new BaseResult().Set(HttpStatusCode.OK).Succeeded(message);
    }


    public static string Slugify(this string phrase)
    {
        var s = phrase.RemoveDiacritics().ToLower();
        s = Regex.Replace(s, @"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200Fa-z0-9\s-]",
            ""); // remove invalid characters
        s = Regex.Replace(s, @"\s+", " ").Trim(); // single space
        s = s.Substring(0, s.Length <= 100 ? s.Length : 45).Trim(); // cut and trim
        s = Regex.Replace(s, @"\s", "-"); // insert hyphens        
        s = Regex.Replace(s, @"‌", "-"); // half space
        return s.ToLower();
    }

    static string RemoveDiacritics(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        var normalizedString = text.Normalize(NormalizationForm.FormKC);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static string ToFileName(this DateTime date)
    {
        return $"{date.Year:0000}-{date.Month:00}-{date.Day:00}-{date.Hour:00}-{date.Minute:00}-{date.Second:00}";
    }

    public static bool IsNotNull(this string? val)
    {
        if (!string.IsNullOrWhiteSpace(val))
            return true;
        return false;
    }

    public static bool IsNull(params string[] values)
    {
        foreach (var value in values)
        {
            if (string.IsNullOrWhiteSpace(value))
                return true;
        }

        return false;
    }

}