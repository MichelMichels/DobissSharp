using MichelMichels.DobissSharp.Api.Models;

namespace MichelMichels.DobissSharp.Api.Tests.Models;

[TestClass]
public class SubjectTests
{
    [TestMethod]
    public void Init()
    {
        // Arrange

        // Act
        Subject subject = new();

        // Assert
        Assert.IsNotNull(subject.Name);
        Assert.IsNotNull(subject.Tags);
        Assert.IsNull(subject.Settings);
        Assert.IsNull(subject.Dimmable);
    }
}
