﻿

using SmartSalon.Application.Errors;

namespace SmartSalon.Application.ResultObject;

public class Result : IResult
{
    private IEnumerable<Error>? _errors;
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get => !IsSuccess; }
    public IEnumerable<Error>? Errors
    {
        get
        {
            if (IsSuccess)
            {
                throw new InvalidOperationException("You are trying to access the errors property of a successful result");
            }

            return _errors;
        }
        private set { _errors = value; }
    }

    //public only for automapper, intended to be used with the factory methods
    public Result() { }

    public static Result Failure(IEnumerable<Error> errors)
        => new Result()
        {
            IsSuccess = false,
            Errors = errors
        };

    public static Result Failure(Error error)
        => new Result()
        {
            IsSuccess = false,
            Errors = [error]
        };

    public static Result Success()
        => new Result()
        {
            IsSuccess = true,
        };

    public static implicit operator Result(Error error) => Failure(error);

}

public class Result<TValue> : IResult
{
    private TValue? _value;
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get => !IsSuccess; }

    public IEnumerable<Error>? Errors { get; private set; }

    public TValue Value
    {
        get
        {
            if (IsFailure)
            {
                throw new InvalidOperationException("Failed results doesn't have a value");
            }

            if (_value is null)
            {
                throw new InvalidOperationException("You cannot access the Value property until you give it a non null value");
            }

            return _value;
        }
        set
        {
            if (value is null)
            {
                throw new InvalidOperationException("Your result value cannot be null");
            }

            _value = value;
        }
    }

    //public only for automapper, intended to be used with the factory methods
    public Result() { }

    public static Result<TValue> Failure(IEnumerable<Error> errors)
        => new Result<TValue>()
        {
            IsSuccess = false,
            Errors = errors
        };

    public static Result<TValue> Failure(Error error)
        => new Result<TValue>()
        {
            IsSuccess = false,
            Errors = [error]
        };

    public static Result<TValue> Success(TValue value)
        => new Result<TValue>
        {
            IsSuccess = true,
            Value = value
        };

    public static implicit operator Result<TValue>(Error error) => Failure(error);

    public static implicit operator Result<TValue>(TValue value) => Success(value);
}