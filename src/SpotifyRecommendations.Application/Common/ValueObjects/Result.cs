namespace SpotifyRecommendations.Application.Common.ValueObjects;

public class Result
{
    public bool Succeeded { get; }
    public IEnumerable<string> Errors { get; } = Array.Empty<string>();

    public Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public static Result Error(IEnumerable<string> errors) => new(false, errors);
    public static Result Success => new(true, Array.Empty<string>());
}