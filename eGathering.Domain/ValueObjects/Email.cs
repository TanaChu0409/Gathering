﻿using eGathering.Domain.Errors;
using eGathering.Domain.SeedWork;
using eGathering.Domain.Shared;

namespace eGathering.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 200;

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string email) =>
        Result.Create(email)
                 .Ensure(
                     e => !string.IsNullOrWhiteSpace(e),
                     DomainErrors.Email.Empty)
                 .Ensure(
                     e => e.Length <= MaxLength,
                     DomainErrors.Email.OverSize)
                 .Ensure(
                     e => e.Split('@').Length == 2,
                     DomainErrors.Email.InvalidFormat)
                 .Map(e => new Email(e));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}