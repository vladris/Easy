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
            : base(document, ResetHighlight)
        {
        }

        /// <summary>
        /// Reset highlight action
        /// </summary>
        /// <param name="format">Default formatting</param>
        private static void ResetHighlight(ITextCharacterFormat format)
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

            // Bold text
            AddHighlightRule(@"(\*\*|__)(?=\S)(.+?[*_]*)(?<=\S)\1", 
                (format) => 
                { 
                    format.Bold = FormatEffect.On;
                    format.Italic = FormatEffect.Off;
                });

            // Italic text
            AddHighlightRule(@"(\*|_)(?=\S)(.+?)(?<=\S)\1", 
                (format) => 
                { 
                    format.Bold = FormatEffect.Off;
                    format.Italic = FormatEffect.On;
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
