namespace Kathanika.Domain.Primitives;

public class KnResult
{
    private KnResult(
        bool isSuccess,
        KnError? error = null
    ) : this(
        isSuccess,
        error is not null ? [error] : null
    )
    {
    }

    protected KnResult(
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

    public static KnResult Success()
    {
        return new KnResult(true, error: null);
    }

    public static KnResult<TValue> Success<TValue>(TValue value)
        where TValue : class
    {
        return KnResult<TValue>.Success(value);
    }

    public static KnResult Failure(KnError error)
    {
        return new KnResult(false, error);
    }

    public static KnResult Failure(IEnumerable<KnError> errors)
    {
        return new KnResult(false, errors.ToArray());
    }

    public static KnResult<TValue> Failure<TValue>(IEnumerable<KnError> errors)
        where TValue : class
    {
        return KnResult<TValue>.Failure(errors);
    }

    public static KnResult<TValue> Failure<TValue>(KnError error)
        where TValue : class
    {
        return KnResult<TValue>.Failure(error);
    }
}

public class KnResult<TValue> : KnResult where TValue : class
{
    private readonly TValue? _value;

    private KnResult(
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

    private KnResult(
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

    public static KnResult<TValue> Success(TValue value)
    {
        return new KnResult<TValue>(true, value, error: null);
    }

    public new static KnResult<TValue> Failure(IEnumerable<KnError> errors)
    {
        return new KnResult<TValue>(false, errors: errors.ToArray());
    }

    public new static KnResult<TValue> Failure(KnError error)
    {
        return new KnResult<TValue>(false, errors: [error]);
    }
}