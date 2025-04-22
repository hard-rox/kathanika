using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// Represents MARC21 041 Language Code field
/// </summary>
public record LanguageCode : ValueObject
{
    /// <summary>
    /// Language code of text/sound track or separate title (R)
    /// Indicates the language of the main content when a resource has more than one language.
    /// </summary>
    public string TextOrSoundTrack { get; }

    /// <summary>
    /// Language code of original and/or intermediate translations of text (R)
    /// Used for translations to indicate the language of the original and/or any intermediate translations.
    /// </summary>
    public string? OriginalLanguage { get; }

    private LanguageCode(string textOrSoundTrack, string? originalLanguage = null)
    {
        TextOrSoundTrack = textOrSoundTrack;
        OriginalLanguage = originalLanguage;
    }

    internal static KnResult<LanguageCode> Create(string textOrSoundTrack, string? originalLanguage = null)
    {
        return string.IsNullOrWhiteSpace(textOrSoundTrack)
            ? KnResult.Failure<LanguageCode>(BibRecordAggregateErrors.LanguageCodeInvalid)
            : KnResult.Success(new LanguageCode(textOrSoundTrack, originalLanguage));
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return TextOrSoundTrack;
        yield return OriginalLanguage;
    }
}