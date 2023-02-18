using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeFormatter.Service.Helpers
{
    public class XmlUtils
    {
        public TextStyles DetectStyles(Paragraph pTag)
        {

            bool isItalic = false;
            bool isBold = false;
            bool isUnderline = false;
            foreach (Run r in pTag.Descendants<Run>())
            {
                if (r.RunProperties != null)
                {
                    RunProperties runProperties = r.RunProperties;

                    isBold = runProperties.Bold != null ? true : false;
                    isItalic = runProperties.Italic != null ? true : false;
                    isUnderline = runProperties.Underline != null ? true : false;
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
                return TextStyles.AllStyles;
            }
            else if (isBold && isItalic)
            {
                return TextStyles.BoldItalic;
            }
            else if (isBold && isUnderline)
            {
                return TextStyles.BoldUnderLine;
            }
            else if (isItalic && isUnderline)
            {
                return TextStyles.ItalicUnderLine;
            }
            else if (isItalic)
            {
                return TextStyles.Italic;
            }
            else if (isBold)
            {
                return TextStyles.Bold;
            }
            else if (isUnderline)
            {
                return TextStyles.UnderLine;
            }
            else
            {
                return TextStyles.NoStyle;
            }

        }
    }

    public enum TextStyles
    {
        AllStyles = 1,
        BoldItalic = 2,
        BoldUnderLine = 3,
        ItalicUnderLine = 4,
        Italic = 5,
        Bold = 6,
        UnderLine = 7,
        NoStyle = 0
    }
}
