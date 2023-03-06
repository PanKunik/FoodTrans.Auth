using Domain.Users.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Unit.Tests.Users.ValueObjects;

public sealed class LastNameTests
{
    [Fact]
    public void Create_WhenPassedValidData_ShouldReturnExpectedObject()
    {
        // Arrange
        var func = () => LastName.Create("Doe");

        // Act
        var cut = func();

        // Assert
        cut.Should().NotBeNull();
        cut.Should().BeOfType<ErrorOr<LastName>>();
        cut.Value.Should().NotBeNull();
        cut.Value.Should().BeOfType<LastName>();
        cut.Value.Value.Should().BeEquivalentTo("Doe");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Create_WhenPassedLastNameThatIsNullOrWhiteSpace_ShouldReturnErrorAuthEmptyLastName(string value)
    {
        // Arrange
        var func = () => LastName.Create(value);

        // Act
        var cut = func();

        // Assert
        cut.Should().NotBeNull();
        cut.Should().BeOfType<ErrorOr<LastName>>();
        cut.Errors.Should().HaveCount(1);
        cut.Errors[0].Should().BeEquivalentTo(Errors.Auth.EmptyLastName);
    }

    [Theory]
    [InlineData("J")]
    [InlineData("Verylonglastnamethatexceedsourlimitoffiftycharacter")]
    [InlineData("Verylonglastnamethatexceedsourlimitoffiftycharacters")]
    public void Create_WhenPassedLastNameThatHasLengthLowerThanTwoOrGreaterThan50_ShouldReturnErrorAuthInvalidLastNameLength(string value)
    {
        // Arrange
        var func = () => LastName.Create(value);

        // Act
        var result = func();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorOr<LastName>>();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().BeEquivalentTo(Errors.Auth.InvalidLastNameLength);
    }

    [Fact]
    public void ImplicitStringOperator_WhenCastFromLastName_ShouldReturnLastNameAsString()
    {
        // Arrange
        LastName cut = LastName.Create("Potter").Value;

        // Act
        string result = cut.Value;

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be("Potter");
    }

    [Fact]
    public void GetEqualityComponents_WhenCalled_ShouldReturnLastName()
    {
        // Arrange
        LastName cut = LastName.Create("Doe").Value;

        // Act
        var result = cut.GetEqualityComponents();

        // Assert
        result.Should().HaveCount(1);
        result
            .SequenceEqual(new List<string>{ "Doe" })
            .Should()
            .BeTrue();
    }
}