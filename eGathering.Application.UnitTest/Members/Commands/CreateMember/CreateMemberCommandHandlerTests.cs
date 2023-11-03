using eGathering.Application.Members.Commands.CreateMember;
using eGathering.Domain.Exceptions.Members;
using eGathering.Domain.Members;
using eGathering.Domain.SeedWork;
using eGathering.Domain.ValueObjects;
using Moq;

namespace eGathering.Application.UnitTest.Members.Commands.CreateMember;

public class CreateMemberCommandHandlerTests
{
    private readonly Mock<IMemberCommandRepository> _memberCommandRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateMemberCommandHandlerTests()
    {
        _memberCommandRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnException_WhenEmailIsNotUnique()
    {
        //  Arrange
        var command = new CreateMemberCommand("first", "last", "alvin.chu@cloudysys.com");

        _memberCommandRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new CreateMemberCommandHandler(
            _memberCommandRepositoryMock.Object);

        //  Act
        var ex = await Assert.ThrowsAsync<MemberEmailDuplicatedException>(() => handler.Handle(command, default)).ConfigureAwait(false);
        //  Assert
        Assert.IsType<MemberEmailDuplicatedException>(ex);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenEmailIsUnique()
    {
        //  Arrange
        var command = new CreateMemberCommand("first", "last", "first.last@gmail.com");

        _memberCommandRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _memberCommandRepositoryMock.SetupGet(x => x.UnitOfWork)
            .Returns(_unitOfWorkMock.Object);

        var handler = new CreateMemberCommandHandler(
            _memberCommandRepositoryMock.Object);

        //  Act
        var result = await handler.Handle(command, default);

        //  Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Handle_Should_CallAddOnRepository_WhenEmailIsUnique()
    {
        //  Arrange
        var command = new CreateMemberCommand("first", "last", "first.last@gmail.com");

        _memberCommandRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _memberCommandRepositoryMock.SetupGet(x => x.UnitOfWork)
            .Returns(_unitOfWorkMock.Object);

        var handler = new CreateMemberCommandHandler(
            _memberCommandRepositoryMock.Object);

        //  Act
        var result = await handler.Handle(command, default);

        //  Assert
        //_memberCommandRepositoryMock.Verify(
        //    x => x.CreateAsync(It.Is<Member>(m => m.Id == result.Value), default),
        //    Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new CreateMemberCommand("first", "last", "first.last@gmail.com");

        _memberCommandRepositoryMock.Setup(
            x => x.IsEmailUniqueAsync(
                It.IsAny<Email>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _memberCommandRepositoryMock.SetupGet(x => x.UnitOfWork)
            .Returns(_unitOfWorkMock.Object);

        var handler = new CreateMemberCommandHandler(
            _memberCommandRepositoryMock.Object);

        //  Act
        await handler.Handle(command, default);

        //  Assert
        _memberCommandRepositoryMock.Verify(
            x => x.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}