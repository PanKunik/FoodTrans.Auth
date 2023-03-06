using Xunit;
using Domain.Blockades;
using Domain.Blockades.ValueObjects;
using System;
using FluentAssertions;

namespace Domain.Unit.Tests;

public class BlockadeTests
{
    [Fact]
    public void Create_WhenInvokedWithProperData_ShouldReturnExpectedObject()
    {
        // Arrange
        var func = () => Blockade.Create(
            BlockedAt.Create(new DateTime(2020, 10, 2)),
            BlockadeRelease.Create(new DateTime(2020, 10, 9)),
            BlockadeReason.Create("Too many failed login attempts").Value
        );

        // Act
        var result = func().Value;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Blockade>();
        result.BlockedAt.Should().BeEquivalentTo(BlockedAt.Create(new DateTime(2020, 10, 2)));
        result.BlockadeRelease.Should().BeEquivalentTo(BlockadeRelease.Create(new DateTime(2020, 10, 9)));
        result.BlockadeReason.Should().BeEquivalentTo(BlockadeReason.Create("Too many failed login attempts").Value);
    }
}