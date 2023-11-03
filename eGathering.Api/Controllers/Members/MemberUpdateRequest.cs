namespace eGathering.Api.Controllers.Members;
public record MemberUpdateRequest(Guid Id, string FirstName, string LastName, string Email);