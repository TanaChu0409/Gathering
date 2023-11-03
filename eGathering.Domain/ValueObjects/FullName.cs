using eGathering.Domain.SeedWork;

namespace eGathering.Domain.ValueObjects;

public sealed class FullName : ValueObject
{
    public const int MaxLength = 100;

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }

    public string LastName { get; }

    public static FullName Create(string firstName, string lastName)
    {
        if (firstName.Length > MaxLength)
        {
            throw new ArgumentException($"{nameof(FirstName)} can't over {MaxLength}");
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException($"{nameof(FirstName)} can't be empty");
        }

        if (lastName.Length > MaxLength)
        {
            throw new ArgumentException($"{nameof(lastName)} can't over {MaxLength}");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException($"{nameof(lastName)} can't be empty");
        }

        return new FullName(firstName, lastName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return FirstName;
        yield return LastName;
    }
}