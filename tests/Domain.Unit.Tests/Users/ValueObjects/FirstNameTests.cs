using Domain.Common.Errors;
using System.Linq;
using Domain.Users.ValueObjects;
using ErrorOr;
using System.Collections.Generic;

namespace Domain.Unit.Tests.Users.ValueObjects;

public sealed class FirstNameTests
{
    [Fact]
    public void Create_WhenPassedValidData_ShouldReturnExpectedObject()
    {
        // Arrange
        var func = () => FirstName.Create("Joseph");

        // Act
        var result = func();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorOr<FirstName>>();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<FirstName>();
        result.Value.Value.Should().BeEquivalentTo("Joseph");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void Create_WhenPassedFirstNameThatIsNullOrWhiteSpace_ShouldReturnErrorAuthEmptyFirstName(string value)
    {
        // Arrange
        var func = () => FirstName.Create(value);

        // Act
        var result = func();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorOr<FirstName>>();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().BeEquivalentTo(Errors.Auth.EmptyFirstName);
    }

    [Theory]
    [InlineData("B")]
    [InlineData("Verylongfirstnamethatexceedsourlimitfiftycharacters")]
    [InlineData("Verylongfirstnamethatexceedsourlimitoffiftycharacters")]
    public void Create_WhenPassedFirstNameThatHasLengthLowerThanTwoOrGreaterThan50_ShouldReturnErrorAuthInvalidFirstNameLength(string value)
    {
        // Arrange
        var func = () => FirstName.Create(value);

        // Act
        var result = func();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorOr<FirstName>>();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().BeEquivalentTo(Errors.Auth.InvalidFirstNameLength);
    }

    [Fact]
    public void ImplicitStringOperator_WhenCastFromFirstName_ShouldReturnFirstNameAsString()
    {
        // Arrange
        FirstName cut = FirstName.Create("Chris").Value;

        // Act
        string result = cut.Value;

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be("Chris");
    }

    [Fact]
    public void GetEqualityComponents_WhenCalled_ShouldReturnFirstName()
    {
        // Arrange
        FirstName cut = FirstName.Create("Joe").Value;

        // Act
        var result = cut.GetEqualityComponents();

        // Assert
        result.Should().HaveCount(1);
        result
            .SequenceEqual(new List<string>{ "Joe" })
            .Should()
            .BeTrue();
    }
}