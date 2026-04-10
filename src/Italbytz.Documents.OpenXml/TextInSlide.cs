using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

namespace Italbytz.Documents.OpenXml;

public static class TextInSlide
{
    public static string[] GetAllTextInSlide(string presentationFile, int slideIndex)
    {
        using var presentationDocument = PresentationDocument.Open(presentationFile, false);
        return GetAllTextInSlide(presentationDocument, slideIndex);
    }

    private static string[] GetAllTextInSlide(PresentationDocument presentationDocument, int slideIndex)
    {
        if (slideIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(slideIndex));
        }

        var presentationPart = presentationDocument.PresentationPart;
        if (presentationPart?.Presentation?.SlideIdList == null)
        {
            return [];
        }

        var slideIds = presentationPart.Presentation.SlideIdList.ChildElements;
        if (slideIndex >= slideIds.Count)
        {
            return [];
        }

        var relationshipId = ((SlideId)slideIds[slideIndex]).RelationshipId;
        if (relationshipId == null)
        {
            return [];
        }

        var slidePart = (SlidePart)presentationPart.GetPartById(relationshipId!);
        return GetAllTextInSlide(slidePart);
    }

    private static string[] GetAllTextInSlide(SlidePart slidePart)
    {
        ArgumentNullException.ThrowIfNull(slidePart);

        var texts = new LinkedList<string>();
        if (slidePart.Slide == null)
        {
            return [];
        }

        foreach (var paragraph in slidePart.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Paragraph>())
        {
            var paragraphText = new StringBuilder();
            foreach (var text in paragraph.Descendants<DocumentFormat.OpenXml.Drawing.Text>())
            {
                paragraphText.Append(text.Text);
            }

            if (paragraphText.Length > 0)
            {
                texts.AddLast(paragraphText.ToString());
            }
        }

        return texts.ToArray();
    }
}
