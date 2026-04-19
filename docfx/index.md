# nuget-foundation

`nuget-foundation` provides the shared building blocks used across the wider `Italbytz.*` package family.

Use this repository if you need reusable common abstractions and lightweight random helpers without pulling in domain-specific packages.

## Packages

- `Italbytz.Common.Abstractions`
- `Italbytz.Common.Random`

## Related repositories

- Open XML document helpers live in `nuget-documents`.
- Music-specific packages live in `nuget-music`.

## Guide

Use `Guides > Shared building blocks` for a quick orientation across the common contracts and helper packages that other repositories build on top of.

## What you can do with nuget-foundation

- build on shared service, source, sink, and repository contracts
- reuse lightweight random helper extensions across application code and libraries
- navigate generated API documentation for the public foundation packages

## Local validation

```bash
dotnet restore nuget-foundation.sln
dotnet test nuget-foundation.sln -v minimal
dotnet pack nuget-foundation.sln -c Release -v minimal
dotnet tool restore
dotnet tool run docfx docfx/docfx.json
```
