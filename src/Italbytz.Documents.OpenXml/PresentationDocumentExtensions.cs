using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

namespace Italbytz.Documents.OpenXml;

using A = DocumentFormat.OpenXml.Drawing;

public static class PresentationDocumentExtensions
{
    public static void ExportToQuartoMarkdown(
        this PresentationDocument presentationDocument,
        string destinationFile,
        string imageDirectory,
        string title = "Presentation",
        string? author = null)
    {
        ArgumentNullException.ThrowIfNull(presentationDocument);

        if (string.IsNullOrWhiteSpace(destinationFile))
        {
            throw new ArgumentException("Output path cannot be null or empty.", nameof(destinationFile));
        }

        var directory = Path.GetDirectoryName(destinationFile);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var builder = new StringBuilder();
        builder.AppendLine("---");
        builder.AppendLine($"title: \"{title}\"");
        if (!string.IsNullOrWhiteSpace(author))
        {
            builder.AppendLine($"author: \"{author}\"");
        }
        builder.AppendLine("format: revealjs");
        builder.AppendLine("---");
        builder.AppendLine();
        builder.AppendLine("# ");
        builder.AppendLine();

        var presentationPart = presentationDocument.PresentationPart;
        if (presentationPart?.Presentation?.SlideIdList == null)
        {
            File.WriteAllText(destinationFile, builder.ToString());
            return;
        }

        foreach (var slideId in presentationPart.Presentation.SlideIdList.Elements<SlideId>())
        {
            var slidePart = (SlidePart)presentationPart.GetPartById(slideId.RelationshipId!);
            var slideText = ExtractTextFromSlide(slidePart);
            builder.AppendLine(slideText);
            builder.AppendLine();

            if (!slidePart.ImageParts.Any())
            {
                continue;
            }

            builder.AppendLine(":::: {.columns}");
            builder.AppendLine();
            builder.AppendLine("::: {.column width=\"50%\"}");

            foreach (var imagePart in slidePart.ImageParts)
            {
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                var imageFileName = $"{Guid.NewGuid()}{Path.GetExtension(imagePart.Uri.ToString())}";
                var imagePath = Path.Combine(imageDirectory, imageFileName);

                using var imageStream = imagePart.GetStream();
                using var fileStream = File.Create(imagePath);
                imageStream.CopyTo(fileStream);

                builder.AppendLine($"![]({imagePath})");
                builder.AppendLine();
            }

            builder.AppendLine(":::");
            builder.AppendLine("::::");
            builder.AppendLine();
        }

        File.WriteAllText(destinationFile, builder.ToString());
    }

    private static string ExtractTextFromSlide(SlidePart slidePart)
    {
        var builder = new StringBuilder();

        var shapes = slidePart.Slide?.Descendants<Shape>() ?? Enumerable.Empty<Shape>();
        foreach (var shape in shapes)
        {
            if (shape.IsFooter() || shape.IsHeader())
            {
                continue;
            }

            if (shape.IsTitle())
            {
                builder.AppendLine("## " + (shape.TextBody?.InnerText?.Trim() ?? string.Empty));
                builder.AppendLine();

                if (slidePart.ImageParts.Any())
                {
                    builder.AppendLine(":::: {.columns}");
                    builder.AppendLine();
                    builder.AppendLine("::: {.column width=\"50%\"}");
                }

                continue;
            }

            var textBody = shape.TextBody;
            if (textBody == null)
            {
                continue;
            }

            var autoNumber = 0;
            foreach (var paragraph in textBody.Elements<A.Paragraph>())
            {
                var paragraphProperties = paragraph.ParagraphProperties;
                var level = paragraphProperties?.Level?.Value ?? 0;
                var isBulleted = paragraphProperties?.GetFirstChild<A.NoBullet>() == null;
                var isNumbered = paragraphProperties?.GetFirstChild<A.AutoNumberedBullet>() != null;
                var indent = new string(' ', level * 2);
                var hasText = paragraph.Elements<A.Run>().Any(run => !string.IsNullOrWhiteSpace(run.Text?.Text));

                if (hasText)
                {
                    if (isNumbered)
                    {
                        builder.Append(indent + $"{++autoNumber}. ");
                    }
                    else if (isBulleted)
                    {
                        builder.Append(indent + "- ");
                    }
                    else
                    {
                        builder.Append(indent);
                    }
                }

                foreach (var run in paragraph.Elements<A.Run>())
                {
                    var text = run.Text?.Text;
                    builder.Append(string.IsNullOrWhiteSpace(text) ? " " : text);
                }

                if (!hasText)
                {
                    continue;
                }

                builder.AppendLine();
                if (!isBulleted && !isNumbered)
                {
                    builder.AppendLine();
                }
            }
        }

        return builder.ToString().Trim();
    }
}
