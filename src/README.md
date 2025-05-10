# Aloe.Utils.Configuration.Json

A lightweight utility for flexible handling of JSON configuration files.

## Main Features

* Extension methods for loading multiple JSON configurations
* Extension methods for conditional loading

## Supported Environments

* .NET 9 and later
* Used in conjunction with Microsoft.Extensions.Configuration.Json

## Install

Using NuGet Package Manager:

```cmd
Install-Package Aloe.Utils.Configuration.Json
```

Or using .NET CLI:

```cmd
dotnet add package Aloe.Utils.Configuration.Json
```

## Usage

```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Aloe.Utils.Configuration.Json;

var isDebug = true;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    // Multiple files can be specified
    .AddJsonFiles(["appsettings.PostgreSql.json", "appsettings.Serilog.json" ])
    // Conditions can be specified within the method chain
    .AddJsonFileIf(isDebug, "appsettings.Debug.json");
```

## License

MIT License
