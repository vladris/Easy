using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Text;

namespace Easy.Text.Highlight
{
    /// <summary>
    /// Simple Markdown highlighter
    /// </summary>
    public class Markdown : BaseHighlighter
    {
        /// <summary>
        /// Creates a new Markdown highlighter
        /// </summary>
        /// <param name="document"></param>
        public Markdown(ITextDocument document)
            : base(document)
        {
        }

        /// <summary>
        /// Performs default formatting (non-highlighted text)
        /// </summary>
        /// <param name="format">Format of selection</param>
        protected override void DefaultFormatting(ITextCharacterFormat format)
        {
            format.Bold = FormatEffect.Off;
            format.Italic = FormatEffect.Off;
        }

        /// <summary>
        /// Adds highlighting rules for Markdown
        /// </summary>
        protected override void AddHighlightRules()
        {
            // Heading with underline
            AddHighlightRule(@"(^([^\r]+?)|\r([^\r]+?))[ ]*\r(=+|-+)[ ]*\r", 
                (format) => 
                { 
                    format.Bold = FormatEffect.On; 
                });

            // Heading prefixed with pound
            AddHighlightRule(@"(^(\#{1,6})|\r(\#{1,6}))[ ]*(.+?)[ ]*\#*\r", 
                (format) => 
                { 
                    format.Bold = FormatEffect.On; 
                });

            // Italic text
            AddHighlightRule(@"(\*|_)(?=\S)(.+?)(?<=\S)\1", 
                (format) => 
                { 
                    format.Italic = FormatEffect.On;
                });

            // Bold text
            AddHighlightRule(@"(\*\*|__)(?=\S)(.+?[*_]*)(?<=\S)\1", 
                (format) => 
                { 
                    format.Bold = FormatEffect.On;
                    format.Italic = FormatEffect.Off;
                });

            // Bold-italic text
            AddHighlightRule(@"(\*\*\*|___)(?=\S)(.+?[*_]*)(?<=\S)\1", 
                (format) => 
                { 
                    format.Bold = FormatEffect.On; 
                    format.Italic = FormatEffect.On; 
                });
        }
    }
}
