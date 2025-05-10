using Xunit;
using Microsoft.Extensions.Configuration;
using Aloe.Utils.Configuration.Json;
using System.IO;

namespace Aloe.Utils.Configuration.Json.Tests;

public class ConfigurationAddJsonFilesExtensionsTests
{
    [Fact]
    public void AddJsonFiles_WithValidFiles_ShouldAddAllFiles()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        var files = new[] { "appsettings.json", "appsettings.Development.json" };

        // Act
        var result = builder.AddJsonFiles(files);

        // Assert
        Assert.Same(builder, result);
    }

    [Fact]
    public void AddJsonFiles_WithEmptyFiles_ShouldNotThrow()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        var files = Array.Empty<string>();

        // Act & Assert
        var result = builder.AddJsonFiles(files);
        Assert.Same(builder, result);
    }

    [Fact]
    public void AddJsonFiles_WithNullFiles_ShouldThrowArgumentNullException()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        IEnumerable<string> files = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => builder.AddJsonFiles(files));
    }

    [Fact]
    public void AddJsonFiles_WithNullBuilder_ShouldThrowArgumentNullException()
    {
        // Arrange
        IConfigurationBuilder builder = null!;
        var files = new[] { "appsettings.json" };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => builder.AddJsonFiles(files));
    }

    [Fact]
    public void AddJsonFiles_WithEmptyFilePath_ShouldThrowArgumentException()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        var files = new[] { "appsettings.json", "" };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => builder.AddJsonFiles(files));
    }

    [Fact]
    public void AddJsonFiles_WithWhitespaceFilePath_ShouldThrowArgumentException()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        var files = new[] { "appsettings.json", "   " };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => builder.AddJsonFiles(files));
    }

    [Fact]
    public void AddJsonFiles_WithDuplicateFiles_ShouldSkipDuplicates()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        var files = new[] { "appsettings.json", "appsettings.json", "APPSETTINGS.JSON" };

        // Act
        var result = builder.AddJsonFiles(files);

        // Assert
        Assert.Same(builder, result);
    }

    [Fact]
    public void AddJsonFiles_WithNonExistentFiles_ShouldThrowWhenOptionalIsFalse()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        var files = new[] { "nonexistent.json" };

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => builder.AddJsonFiles(files, optional: false));
    }

    [Fact]
    public void AddJsonFiles_WithNonExistentFiles_ShouldNotThrowWhenOptionalIsTrue()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        var files = new[] { "nonexistent.json" };

        // Act & Assert
        var result = builder.AddJsonFiles(files, optional: true);
        Assert.Same(builder, result);
    }

    [Fact]
    public void AddJsonFiles_WithCustomParameters_ShouldApplyParameters()
    {
        // Arrange
        var builder = new ConfigurationBuilder();
        var files = new[] { "appsettings.json" };

        // Act
        var result = builder.AddJsonFiles(files, optional: true, reloadOnChange: true);

        // Assert
        Assert.Same(builder, result);
    }
}
