public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error? Error { get; }
    public IReadOnlyList<ValidationError> ValidationErrors { get; }

    private Result(bool isSuccess, T? value, Error? error, List<ValidationError>? validationErrors)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        ValidationErrors = validationErrors ?? new List<ValidationError>();
    }

    public static Result<T> Success(T value)
        => new(true, value, null, null);

    public static Result<T> Failure(Error error)
        => new(false, default, error, null);

    public static Result<T> ValidationFailure(List<ValidationError> errors)
        => new(false, default, null, errors);
}