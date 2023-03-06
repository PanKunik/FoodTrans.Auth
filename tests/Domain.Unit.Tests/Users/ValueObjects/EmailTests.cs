using Domain.Users.ValueObjects;
using ErrorOr;
using Domain.Common.Errors;
using System.Linq;
using System.Collections.Generic;

namespace Domain.Unit.Tests.Users.ValueObjects;

public sealed class EmailTests
{
    [Fact]
    public void Create_WhenPassedProperData_ShouldReturnExpectedObject()
    {
        // Arrange
        var func = () => Email.Create("test@example.com");

        // Act
        var result = func();

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();
        result.Value.Value.Should().BeEquivalentTo("test@example.com");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_WhenPassedNullOrEmptyEmail_ShouldReturnErrorAuthEmptyEmail(string value)
    {
        // Arrange
        var func = () => Email.Create(value);

        // Act
        var result = func();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorOr<Email>>();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().BeEquivalentTo(Errors.Auth.EmptyEmail);
    }

    [Theory]
    [InlineData("thisisverylongemailaddressthatcanexceedourlimitofonehundredcharactersforthisfield@verylong-domain.com")]
    [InlineData("thisisverylongemailandevenlongerthanaddressabovethatcanexceedourlimitofonehundredcharactersforthisfield@verylongdomain.com")]
    public void Create_WhenPassedEmailThatIsLongerThan100Characters_ShouldReturnErrorAuthInvalidEmailLength(string value)
    {
        // Arrange
        var func = () => Email.Create(value);

        // Act
        var result = func();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorOr<Email>>();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().BeEquivalentTo(Errors.Auth.InvalidEmailLength);
    }

    [Theory]
    [InlineData(" test@test.com")]
    [InlineData("test-test.com")]
    [InlineData("te)(*&st@example.com")]
    public void Create_WhenPassedEmailThatDoesntMatchRegexPattern_ShouldReturnErrorAuthInvalidEmail(string value)
    {
        // Arrange
        var func = () => Email.Create(value);

        // Act
        var result = func();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ErrorOr<Email>>();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].Should().BeEquivalentTo(Errors.Auth.InvalidEmail);
    }

    [Fact]
    public void ImplicitStringOperator_WhenCastFromEmail_ShouldReturnEmailAsString()
    {
        // Arrange
        Email cut = Email.Create("my-email@my-domain.com").Value;

        // Act
        string result = cut.Value;

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be("my-email@my-domain.com");
    }

    [Fact]
    public void GetEqualityComponents_WhenCalled_ShouldReturnEmail()
    {
        // Arrange
        Email cut = Email.Create("my-email@my-domain.com").Value;

        // Act
        var result = cut.GetEqualityComponents();

        // Assert
        result.Should().HaveCount(1);
        result
            .SequenceEqual(new List<string>{ "my-email@my-domain.com" })
            .Should()
            .BeTrue();
    }
}