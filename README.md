<!-- omit in toc -->
# DobissSharp 📦

DobissSharp is a C# .NET wrapper for the [domotics system Dobiss NXT](). It contains a REST api wrapper (`MichelMichels.DobissSharp.Api`) and an opiniated C# library which consumes previous wrapper (`MichelMichels.DobissSharp`).

<details>
<summary>Table of Contents</summary>

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
  - [`MichelMichels.DobissSharp.Api`](#michelmichelsdobisssharpapi)
  - [`MichelMichels.DobissSharp`](#michelmichelsdobisssharp)
- [Documentation](#documentation)
- [Credits](#credits)

</details>

---

## Prerequisites
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- A Dobiss NXT system

## Installation

> [!WARNING]
> Package `MichelMichels.DobissSharp` is in development and is subject to breaking changes. Use this at own risk.

| Package name                    | Version                                                                                                                                        | Description                              |
| ------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------- |
| `MichelMichels.DobissSharp.Api` | [![NuGet Version](https://img.shields.io/nuget/v/MichelMichels.DobissSharp.Api)](https://www.nuget.org/packages/MichelMichels.DobissSharp.Api) | Reference implementation of the REST API |
| `MichelMichels.DobissSharp`     | [![NuGet Version](https://img.shields.io/nuget/vpre/MichelMichels.DobissSharp)](https://www.nuget.org/packages/MichelMichels.DobissSharp)      | Opiniated C# class library               |

Get the NuGet packages from [nuget.org](https://www.nuget.org/) or search for `MichelMichels.DobissSharp` in the GUI package manager in Visual Studio.

You can also use the cli of the package manager with following command:

```cli
Install-Package MichelMichels.DobissSharp.Api
Install-Package MichelMichels.DobissSharp
```

## Usage

### `MichelMichels.DobissSharp.Api`

Creating the API client:

```csharp
DobissClientOptions options = new()
{
    BaseUrl = @"http://dobiss.local/",
    SecretKey = "your-secret-api-key",
};
DobissClient apiClient = new(options);
```

Calls: 
```csharp
// Discover
DiscoverResponse response = await apiClient.Discover();

// Status
StatusResponse response = await apiClient.Status();

// Action
ActionRequest body = new()
{
    AddressId = 0,
    ChannelId = 12,
    ActionId = ActionId.Toggle,
};
ActionResponse response = await apiClient.Action(body);
```

### `MichelMichels.DobissSharp`

Creating the service:
```csharp
// See previous code example for creating the API client
DobissService dobiss = new(apiClient);
```

See the interface `IDobissService` for more information. This is still in development and subject to breaking changes.

## Documentation

Visit the [API documentation page of Dobiss NXT](https://support.dobiss.com/books/nl-dobiss-nxt/page/developer-api) for more in-depth information about the API.

## Credits

- Created by [Michel Michels](https://github.com/MichelMichels).