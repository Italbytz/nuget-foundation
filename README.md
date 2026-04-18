# nuget-foundation

[![Documentation](https://img.shields.io/badge/Documentation-GitHub%20Pages-blue?style=for-the-badge)](https://italbytz.github.io/nuget-foundation/)

`nuget-foundation` bundles the first shared building blocks of the refactored `Italbytz.*` package family.

It is intended for developers who need reusable **common abstractions**, **random helpers**, and **Open XML utilities** without pulling in larger domain packages.

## Packages in this repository

### `Italbytz.Common.Abstractions`
Shared interfaces and result types for repository, service, source, and sink patterns.

### `Italbytz.Common.Random`
Small reusable extensions around random number generation.

### `Italbytz.Documents.OpenXml`
Helpers for extracting and converting content from presentation documents via Open XML.

## Which package should I use?

- Use `Italbytz.Common.Abstractions` if you need general reusable contracts.
- Use `Italbytz.Common.Random` for lightweight helper extensions.
- Use `Italbytz.Documents.OpenXml` for PowerPoint/Open XML document handling.

Music-related contracts and clients now live in the dedicated `nuget-music` repository.

## Documentation

API documentation is generated with `docfx` and published via GitHub Pages:

- `https://italbytz.github.io/nuget-foundation/`

The doc site now also includes a short guide to the shared building blocks in this repo, covering common service/result abstractions and Open XML helpers.

If the URL still returns `404`, wait until the `CI` workflow on `main` has completed the first Pages publish run.

## Quality checks

This repository includes:

- a `GitHub Actions` workflow in `.github/workflows/ci.yml`
- automated `restore`, `build`, `test`, `pack`, and docs publication
- a `docfx` setup under `docfx/`

## Release model

- `nuget-foundation` now follows the stable `1.0.x` line for the already consolidated foundation packages
- a pushed tag such as `v1.0.1` triggers the release-ready pipeline in GitHub Actions
- if the repository secret `NUGET_API_KEY` is configured, the workflow also publishes `.nupkg` and `.snupkg` files to NuGet

## Local validation

```bash
dotnet restore nuget-foundation.sln
dotnet test nuget-foundation.sln -v minimal
dotnet pack nuget-foundation.sln -c Release -v minimal
dotnet tool restore
dotnet tool run docfx docfx/docfx.json
```
