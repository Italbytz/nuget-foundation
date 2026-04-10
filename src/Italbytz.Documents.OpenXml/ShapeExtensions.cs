using DocumentFormat.OpenXml.Presentation;

namespace Italbytz.Documents.OpenXml;

public static class ShapeExtensions
{
    public static bool IsHeader(this Shape shape)
    {
        var placeholderShape = shape.NonVisualShapeProperties?
            .ApplicationNonVisualDrawingProperties?
            .GetFirstChild<PlaceholderShape>();

        return placeholderShape?.Type is { HasValue: true } &&
               placeholderShape.Type == PlaceholderValues.Header;
    }

    public static bool IsFooter(this Shape shape)
    {
        var placeholderShape = shape.NonVisualShapeProperties?
            .ApplicationNonVisualDrawingProperties?
            .GetFirstChild<PlaceholderShape>();

        return placeholderShape?.Type is { HasValue: true } &&
               (placeholderShape.Type == PlaceholderValues.Footer ||
                placeholderShape.Type == PlaceholderValues.SlideNumber ||
                placeholderShape.Type == PlaceholderValues.DateAndTime);
    }

    public static bool IsTitle(this Shape shape)
    {
        var placeholderShape = shape.NonVisualShapeProperties?
            .ApplicationNonVisualDrawingProperties?
            .GetFirstChild<PlaceholderShape>();

        return placeholderShape?.Type is { HasValue: true } &&
               (placeholderShape.Type == PlaceholderValues.Title ||
                placeholderShape.Type == PlaceholderValues.CenteredTitle);
    }
}
