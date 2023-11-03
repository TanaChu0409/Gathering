using System.Reflection;

namespace eGathering.ArchitectureTest;

public class ArchitectureTests
{
    private readonly string _persistenceNameSpace = Persistence.AssemblyReference.Namespace;
    private readonly string _apiNameSpace = Api.AssemblyReference.Namespace;
    private readonly string _applicationNameSpace = Application.AssemblyReference.Namespace;

    private static Assembly DomainAssembly
        => Domain.AssemblyReference.Assembly;

    private static Assembly ApplicationAssembly
        => Application.AssemblyReference.Assembly;

    private static Assembly PersistenceAssembly
        => Persistence.AssemblyReference.Assembly;

    private static Assembly InfrastructureAssembly
        => Infrastructure.AssemblyReference.Assembly;

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var types = Types.InAssembly(DomainAssembly);
        var otherProjects = new[]
        {
            _applicationNameSpace,
            _persistenceNameSpace,
            _apiNameSpace,
        };

        // Act
        var testResult = types.ShouldNot()
                              .HaveDependencyOnAll(otherProjects)
                              .GetResult();

        // Assert
        Assert.True(testResult.IsSuccessful);
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var types = Types.InAssembly(ApplicationAssembly);
        var otherProjects = new[]
        {
            _persistenceNameSpace,
            _apiNameSpace,
        };

        // Act
        var testResult = types.ShouldNot()
                              .HaveDependencyOnAll(otherProjects)
                              .GetResult();

        // Assert
        Assert.True(testResult.IsSuccessful);
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var types = Types.InAssembly(InfrastructureAssembly);
        var otherProjects = new[]
        {
            _apiNameSpace,
        };

        // Act
        var testResult = types.ShouldNot()
                              .HaveDependencyOnAll(otherProjects)
                              .GetResult();

        // Assert
        Assert.True(testResult.IsSuccessful);
    }

    [Fact]
    public void Persistence_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var types = Types.InAssembly(PersistenceAssembly);
        var otherProjects = new[]
        {
            _apiNameSpace,
        };

        // Act
        var testResult = types.ShouldNot()
                              .HaveDependencyOnAll(otherProjects)
                              .GetResult();

        // Assert
        Assert.True(testResult.IsSuccessful);
    }
}