using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using Italbytz.Documents.OpenXml;

namespace Italbytz.Documents.OpenXml.Tests;

using A = DocumentFormat.OpenXml.Drawing;

[TestClass]
public sealed class PresentationConverterTests
{
    [TestMethod]
    public void ConvertToQuartoMarkdown_WritesExpectedMarkdown()
    {
        var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDirectory);

        try
        {
            var presentationPath = Path.Combine(tempDirectory, "sample.pptx");
            var markdownPath = Path.Combine(tempDirectory, "sample.qmd");
            var imageDirectory = Path.Combine(tempDirectory, "images");

            CreatePresentation(presentationPath, "Slide Title", "Bullet point");

            PresentationConverter.ConvertToQuartoMarkdown(
                presentationPath,
                markdownPath,
                imageDirectory,
                "Demo Presentation",
                "Robin Nunkesser");

            Assert.IsTrue(File.Exists(markdownPath));
            var markdown = File.ReadAllText(markdownPath);
            StringAssert.Contains(markdown, "title: \"Demo Presentation\"");
            StringAssert.Contains(markdown, "## Slide Title");
            StringAssert.Contains(markdown, "- Bullet point");
        }
        finally
        {
            if (Directory.Exists(tempDirectory))
            {
                Directory.Delete(tempDirectory, recursive: true);
            }
        }
    }

    private static void CreatePresentation(string filePath, string title, string bullet)
    {
        using var presentationDocument = PresentationDocument.Create(filePath, PresentationDocumentType.Presentation);
        var presentationPart = presentationDocument.AddPresentationPart();
        presentationPart.Presentation = new Presentation();

        var slidePart = presentationPart.AddNewPart<SlidePart>("rId1");
        slidePart.Slide = new Slide(
            new CommonSlideData(
                new ShapeTree(
                    new NonVisualGroupShapeProperties(
                        new NonVisualDrawingProperties { Id = 1U, Name = string.Empty },
                        new NonVisualGroupShapeDrawingProperties(),
                        new ApplicationNonVisualDrawingProperties()),
                    new GroupShapeProperties(),
                    CreateShape(2U, "Title", title, PlaceholderValues.Title),
                    CreateShape(3U, "Content", bullet, null))),
            new ColorMapOverride(new A.MasterColorMapping()));

        presentationPart.Presentation.SlideIdList = new SlideIdList(
            new SlideId { Id = 256U, RelationshipId = "rId1" });
        presentationPart.Presentation.SlideSize = new SlideSize { Cx = 9144000, Cy = 6858000 };
        presentationPart.Presentation.NotesSize = new NotesSize { Cx = 6858000, Cy = 9144000 };
        presentationPart.Presentation.Save();
    }

    private static Shape CreateShape(uint id, string name, string text, PlaceholderValues? placeholderType)
    {
        var applicationProperties = new ApplicationNonVisualDrawingProperties();
        if (placeholderType.HasValue)
        {
            applicationProperties.Append(new PlaceholderShape { Type = placeholderType.Value });
        }

        return new Shape(
            new NonVisualShapeProperties(
                new NonVisualDrawingProperties { Id = id, Name = name },
                new NonVisualShapeDrawingProperties(new A.ShapeLocks { NoGrouping = true }),
                applicationProperties),
            new ShapeProperties(),
            new TextBody(
                new A.BodyProperties(),
                new A.ListStyle(),
                new A.Paragraph(new A.Run(new A.Text(text)))));
    }
}
