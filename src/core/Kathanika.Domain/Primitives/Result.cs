namespace Kathanika.Domain.Primitives;

public class Result
{
    private Result(
        bool isSuccess,
        KnError? error = null
    ) : this(
        isSuccess,
        error is not null ? [error] : null
    )
    {
    }

    protected Result(
        bool isSuccess,
        KnError[]? errors = null
    )
    {
        if ((isSuccess && errors is not null)
            || (!isSuccess && (errors is null || errors.Length == 0)))
            throw new ArgumentException("Invalid Result", nameof(errors));

        IsSuccess = isSuccess;
        Errors = errors!;
    }

    public bool IsSuccess { get; protected init; }
    public bool IsFailure => !IsSuccess;
    public KnError[] Errors { get; protected init; }

    public static Result Success()
    {
        return new Result(true, error: null);
    }

    public static Result<TValue> Success<TValue>(TValue value)
        where TValue : class
    {
        return Result<TValue>.Success(value);
    }

    public static Result Failure(KnError error)
    {
        return new Result(false, error);
    }

    public static Result Failure(IEnumerable<KnError> errors)
    {
        return new Result(false, errors.ToArray());
    }

    public static Result<TValue> Failure<TValue>(IEnumerable<KnError> errors)
        where TValue : class
    {
        return Result<TValue>.Failure(errors);
    }

    public static Result<TValue> Failure<TValue>(KnError error)
        where TValue : class
    {
        return Result<TValue>.Failure(error);
    }
}

public class Result<TValue> : Result where TValue : class
{
    private readonly TValue? _value;

    private Result(
        bool isSuccess,
        TValue? value = null,
        KnError? error = null
    ) : this(
        isSuccess,
        value,
        error is not null ? [error] : null
    )
    {
    }

    private Result(
        bool isSuccess,
        TValue? value = null,
        KnError[]? errors = null
    ) : base(isSuccess, errors)
    {
        if ((isSuccess && value is null)
            || (!isSuccess && value is not null))
            throw new ArgumentException("Invalid Result", nameof(value));

        IsSuccess = isSuccess;
        Errors = errors ?? [];
        _value = value;
    }

    public TValue Value => IsSuccess && _value is not null
        ? _value
        : throw new InvalidDataException("Cannot access Value from failure result");

    public static Result<TValue> Success(TValue value)
    {
        return new Result<TValue>(true, value, error: null);
    }

    public new static Result<TValue> Failure(IEnumerable<KnError> errors)
    {
        return new Result<TValue>(false, errors: errors.ToArray());
    }

    public new static Result<TValue> Failure(KnError error)
    {
        return new Result<TValue>(false, errors: [error]);
    }
}