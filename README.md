# nuget-foundation

Foundation repository for the first refactored `Italbytz.*` packages.

## Current packages

- `Italbytz.Common.Abstractions`
- `Italbytz.Common.Random`
- `Italbytz.Documents.OpenXml`
- `Italbytz.Music.Abstractions`

## Migration notice

Older articles, slides, and repositories may still refer to historical names such as:

- `Italbytz.Ports.Common`
- `Italbytz.Extensions.Random`
- `Italbytz.OpenXml`
- `Italbytz.Ports.Music`
- `nuget-ports-common`
- `nuget-extensions-random`
- `nuget-openxml`
- `nuget-ports-music`

Please use the new package names for all new development.

## Build and docs

This repository now includes a baseline `GitHub Actions` workflow in `.github/workflows/ci.yml` and a `docfx` setup under `docfx/`.

Local validation commands:

```bash
dotnet restore nuget-foundation.sln
dotnet test nuget-foundation.sln -v minimal
dotnet pack nuget-foundation.sln -c Release -v minimal
dotnet tool restore
dotnet tool run docfx docfx/docfx.json
```