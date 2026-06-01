namespace Scripters.Regula.Platform.Shared.Application.Patterns;

/// <summary>
/// Represents the outcome of an operation that can succeed with a value or fail with an error.
/// </summary>
/// <typeparam name="TValue">The type of the successful result value.</typeparam>
/// <typeparam name="TError">The type of the error information.</typeparam>
public abstract record Result<TValue, TError>
{
    private Result() { }

    /// <summary>
    /// Represents a successful operation outcome.
    /// </summary>
    /// <param name="Value">The resulting value from the successful operation.</param>
    public sealed record Success(TValue Value) : Result<TValue, TError>;

    /// <summary>
    /// Represents a failed operation outcome.
    /// </summary>
    /// <param name="Error">The error information from the failed operation.</param>
    public sealed record Failure(TError Error) : Result<TValue, TError>;

    /// <summary>
    /// Determines whether the result represents a successful outcome.
    /// </summary>
    public bool IsSuccess => this is Success;

    /// <summary>
    /// Determines whether the result represents a failed outcome.
    /// </summary>
    public bool IsFailure => this is Failure;

    public static implicit operator Result<TValue, TError>(TValue value) => new Success(value);
    public static implicit operator Result<TValue, TError>(TError error) => new Failure(error);

    /// <summary>
    /// Applies a transformation function to the success value if the result is successful.
    /// </summary>
    /// <typeparam name="TNext">The type of the next result value.</typeparam>
    /// <param name="onSuccess">Function to apply if successful.</param>
    /// <returns>A new Result with the transformed value, or the current Failure.</returns>
    public Result<TNext, TError> Map<TNext>(Func<TValue, TNext> onSuccess) =>
        this switch
        {
            Success s => new Result<TNext, TError>.Success(onSuccess(s.Value)),
            Failure f => new Result<TNext, TError>.Failure(f.Error),
            _ => throw new InvalidOperationException("Unknown Result type")
        };
        
    /// <summary>
    /// Binds a function that returns a Result to the success value.
    /// </summary>
    /// <typeparam name="TNext">The type of the next result value.</typeparam>
    /// <param name="onSuccess">Function to apply if successful, which returns a new Result.</param>
    /// <returns>The new Result, or the current Failure.</returns>
    public Result<TNext, TError> Bind<TNext>(Func<TValue, Result<TNext, TError>> onSuccess) =>
        this switch
        {
            Success s => onSuccess(s.Value),
            Failure f => new Result<TNext, TError>.Failure(f.Error),
            _ => throw new InvalidOperationException("Unknown Result type")
        };

    /// <summary>
    /// Applies a function to either the success or failure case.
    /// </summary>
    /// <typeparam name="TResult">The type of the final result.</typeparam>
    /// <param name="onSuccess">Function to apply if successful.</param>
    /// <param name="onFailure">Function to apply if failed.</param>
    /// <returns>The result of applying the appropriate function.</returns>
    public TResult Fold<TResult>(Func<TValue, TResult> onSuccess, Func<TError, TResult> onFailure) =>
        this switch
        {
            Success s => onSuccess(s.Value),
            Failure f => onFailure(f.Error),
            _ => throw new InvalidOperationException("Unknown Result type")
        };

    /// <summary>
    /// Executes an action based on the result type without transforming the value.
    /// </summary>
    /// <param name="onSuccess">Action to execute if successful.</param>
    /// <param name="onFailure">Action to execute if failed.</param>
    public void Match(Action<TValue> onSuccess, Action<TError> onFailure)
    {
        switch (this)
        {
            case Success s:
                onSuccess(s.Value);
                break;
            case Failure f:
                onFailure(f.Error);
                break;
        }
    }
}