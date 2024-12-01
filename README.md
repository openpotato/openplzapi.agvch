[![NuGet Gallery](https://img.shields.io/badge/NuGet%20Gallery-openplzapi.AGVCH-blue.svg)](https://www.nuget.org/packages/openplzapi.agvch/)
![GitHub](https://img.shields.io/github/license/openpotato/openplzapi.agvch)

# OpenPlzApi.AGVCH

A [.NET](https://dotnet.microsoft.com/) client library for direct access to the REST services of the application of the Swiss communes (AGVCH) provided by the [Swiss Federal Statistical Office](https://www.bfs.admin.ch/bfs/en/home.html).

+ Supports .NET 8 and .NET 9
+ Supports the following AGVCH API endpoints:
    + Snapshot of the communes (Snapshot der Gemeinden): `../api/communes/snapshot`
	+ Mutations of the communes (Mutationen der Gemeinden): `../api/communes/mutations`
	+ Levels of the communes (Raumgliederungen der Gemeinden): `../api/communes/levels`

## Installation

```
dotnet add package OpenPlzApi.AGVCH
```

## Getting started

Documentation is available in the [GitHub wiki](https://github.com/openpotato/openplzapi.agvch/wiki).

## Can I help?

Yes, that would be much appreciated. The best way to help is to post a response via the Issue Tracker and/or submit a Pull Request.
