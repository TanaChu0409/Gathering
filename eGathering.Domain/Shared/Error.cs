namespace eGathering.Domain.Shared;

public sealed class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? left, Error? right)
    {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=(Error? left, Error? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        return obj is Error;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }

    public bool Equals(Error? other)
    {
        return other is not null && other.Code == this.Code && other.Message == this.Message;
    }
}