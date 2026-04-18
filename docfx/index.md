# nuget-foundation

`nuget-foundation` is the first consolidated repository in the NuGet refactoring and currently contains these packages:

- `Italbytz.Common.Abstractions`
- `Italbytz.Common.Random`
- `Italbytz.Documents.OpenXml`

## Guide

Use `Guides > Shared building blocks` for a quick orientation across the common contracts and Open XML helpers that other repositories build on top of.

## Local validation

```bash
dotnet restore nuget-foundation.sln
dotnet test nuget-foundation.sln -v minimal
dotnet tool restore
dotnet tool run docfx docfx/docfx.json
```

## Documentation goals

- keep package discovery simple
- provide API reference generated from XML documentation
- document the migration from historical package names to the new `Italbytz.*` structure
