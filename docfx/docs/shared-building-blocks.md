# Shared building blocks

`nuget-foundation` contains the low-level building blocks that the other refactored repositories depend on.

## Package overview

| Package | Purpose | Typical entry points |
| --- | --- | --- |
| `Italbytz.Common.Abstractions` | generic contracts and minimal result wrappers | `Result<T>`, `IService<...>`, `IAsyncService<...>`, `IDataSource<...>`, `IDataSink<...>`, `ICrudRepository<...>` |
| `Italbytz.Common.Random` | lightweight random-related helpers | random extensions used by higher-level packages |
| `Italbytz.Documents.OpenXml` | helpers for presentation and slide extraction workflows | `PresentationConverter`, `PresentationDocumentExtensions`, `ShapeExtensions`, `TextInSlide` |
| `Italbytz.Music.Abstractions` | contracts for music search and track/collection models | `ISearchTerm`, `ISearchTracksService`, `ITrackEntity`, `ICollectionEntity` |

## Common contracts

The common abstractions package is intentionally small and reusable. It provides a stable base for service, repository, source, and sink patterns across the other domains.

Examples include:

- `Result<T>` as a compact wrapper for returning mapped values
- `IService<TInDto, TOutDto>` and `IAsyncService<TInDto, TOutDto>` for sync and async service contracts
- repository and data-flow abstractions such as `IDataSource<...>` and `IDataSink<...>`

## Open XML helpers

The `Italbytz.Documents.OpenXml` package is intended for presentation-oriented document workflows.

A simple example is converting a presentation into Quarto-flavored Markdown:

```csharp
using Italbytz.Documents.OpenXml;

PresentationConverter.ConvertToQuartoMarkdown(
    presentationFile: "slides.pptx",
    destinationFile: "slides.qmd",
    imageDirectory: "images",
    title: "Presentation",
    author: "Italbytz");
```

## Music abstractions

`Italbytz.Music.Abstractions` contains only contracts. It is designed as a base for later clients or services, for example an iTunes- or catalog-related integration package.

If you only need search terms, track metadata, or collection entities, this is the right package to depend on without pulling in any concrete provider implementation.

## Historical mapping

This consolidated repo replaces several older foundations of the package family:

- `Italbytz.Ports.Common` → `Italbytz.Common.Abstractions`
- `Italbytz.Extensions.Random` → `Italbytz.Common.Random`
- `Italbytz.OpenXml` → `Italbytz.Documents.OpenXml`
- `Italbytz.Ports.Music` → `Italbytz.Music.Abstractions`

That means consumers now get the same conceptual base from one shared repository with a unified build, test, and documentation setup.