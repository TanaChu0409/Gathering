using eGathering.Domain.SeedWork;

namespace eGathering.Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 100;

    private FirstName(string firstName)
    {
        Value = firstName;
    }

    public string Value { get; }

    public static FirstName Create(string firstName)
    {
        if (firstName.Length > MaxLength)
        {
            throw new ArgumentException($"{nameof(FirstName)} can't over {MaxLength}");
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException($"{nameof(FirstName)} can't be empty");
        }

        return new FirstName(firstName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}