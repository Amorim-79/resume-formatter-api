using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using ResumeFormatter.Service.Enums.Xml;

namespace ResumeFormatter.Service.Helpers
{
    public class XmlUtils
    {
        public ETextStyles DetectStyles(Paragraph paragraph)
        {
            bool isItalic = false;
            bool isBold = false;
            bool isUnderline = false;

            foreach (Run r in paragraph.Descendants<Run>())
            {
                if (r.RunProperties != null)
                {
                    RunProperties runProperties = r.RunProperties;

                    isBold = runProperties.Bold != null;
                    isItalic = runProperties.Italic != null;
                    isUnderline = runProperties.Underline != null;
                }
                else
                {
                    isBold = false;
                    isItalic = false;
                    isUnderline = false;
                }
            }
            if (isItalic && isBold && isUnderline)
            {
                return ETextStyles.AllStyles;
            }
            else if (isBold && isItalic)
            {
                return ETextStyles.BoldItalic;
            }
            else if (isBold && isUnderline)
            {
                return ETextStyles.BoldUnderLine;
            }
            else if (isItalic && isUnderline)
            {
                return ETextStyles.ItalicUnderLine;
            }
            else if (isItalic)
            {
                return ETextStyles.Italic;
            }
            else if (isBold)
            {
                return ETextStyles.Bold;
            }
            else if (isUnderline)
            {
                return ETextStyles.UnderLine;
            }
            else
            {
                return ETextStyles.NoStyle;
            }
        }

        public string? DetectFontFamily(Paragraph paragraph)
        {
            foreach (Run r in paragraph.Descendants<Run>())
            {
                if (r.RunProperties != null)
                {
                    RunProperties runProperties = r.RunProperties;
                    return runProperties?.RunFonts?.Ascii;
                }
            }
            return null;
        }

        public StringValue? DetectFontSize(Paragraph paragraph)
        {
            foreach (Run r in paragraph.Descendants<Run>())
            {
                if (r.RunProperties != null)
                {
                    RunProperties runProperties = r.RunProperties;
                    return runProperties?.FontSize?.Val;
                }
            }
            return null;
        }

        public JustificationValues DetectParagraphJustification(Paragraph paragraph)
        {
            if (paragraph == null
                || paragraph?.ParagraphProperties == null
                || paragraph?.ParagraphProperties?.Justification == null 
                || paragraph?.ParagraphProperties?.Justification.Val == null)
            {
                return JustificationValues.Start;
            }

            return paragraph.ParagraphProperties.Justification.Val;
        }
    }
}
