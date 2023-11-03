using eGathering.Domain.Shared;

namespace eGathering.Domain.Errors;

public static class DomainErrors
{
#pragma warning disable CA1034 // 巢狀型別不應可見

    public static class Gathering
#pragma warning restore CA1034 // 巢狀型別不應可見
    {
        public static readonly Error InvitingCreator = new(
            "Gathering.InvitingCreator",
            "Can't send invitation to the gathering creator.");

        public static readonly Error AlreadyPassed = new(
                "Gathering.AlreadyPassed",
                "Can't send invitation for gathering in the past.");
    }

#pragma warning disable CA1034 // 巢狀型別不應可見

    public static class Email
#pragma warning restore CA1034 // 巢狀型別不應可見
    {
        public static readonly Error Empty = new(
            "eGathering.EmailCreate",
            "The email can't be empty");

        public static readonly Error OverSize = new(
            "eGathering.EmailCreate",
            $"The email can't over {ValueObjects.Email.MaxLength}");

        public static readonly Error InvalidFormat = new(
            "eGathering.EmailCreate",
            "The email is not email pattern");
    }

#pragma warning disable CA1034 // 巢狀型別不應可見

    public static class Member
#pragma warning restore CA1034 // 巢狀型別不應可見
    {
        public static readonly Error EmailIsNotUnique = new(
                "Gathering.EmailIsNotUnique",
                "Email was used, please use other email address");
    }
}