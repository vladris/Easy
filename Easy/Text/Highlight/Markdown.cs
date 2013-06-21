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
    class Markdown : BaseHighlighter
    {
        /// <summary>
        /// Bold character format
        /// </summary>
        protected ITextCharacterFormat Bold;

        /// <summary>
        /// Italic character format
        /// </summary>
        protected ITextCharacterFormat Italic;

        /// <summary>
        /// Bold-italic character format
        /// </summary>
        protected ITextCharacterFormat BoldItalic;

        /// <summary>
        /// Creates a new Markdown highlighter
        /// </summary>
        /// <param name="document"></param>
        public Markdown(ITextDocument document)
            : base(document)
        {
            InitializeFormats();
        }

        /// <summary>
        /// Initialize highlighting formats
        /// </summary>
        public void InitializeFormats()
        {
            // Initialize formats
            Bold = document.GetDefaultCharacterFormat().GetClone();
            Bold.Bold = FormatEffect.On;

            Italic = document.GetDefaultCharacterFormat().GetClone();
            Italic.Italic = FormatEffect.On;

            BoldItalic = document.GetDefaultCharacterFormat().GetClone();
            BoldItalic.Bold = FormatEffect.On;
            BoldItalic.Italic = FormatEffect.On;
        }

        /// <summary>
        /// Adds highlighting rules for Markdown
        /// </summary>
        protected override void AddHighlightRules()
        {
            // Heading with underline
            AddHighlightRule(@"(^([^\r]+?)|\r([^\r]+?))[ ]*\r(=+|-+)[ ]*\r", Bold);

            // Heading prefixed with pound
            AddHighlightRule(@"(^(\#{1,6})|\r(\#{1,6}))[ ]*(.+?)[ ]*\#*\r", Bold);

            // Bold text
            AddHighlightRule(@"(\*\*|__)(?=\S)(.+?[*_]*)(?<=\S)\1", Bold);

            // Italic text
            AddHighlightRule(@"(\*|_)(?=\S)(.+?)(?<=\S)\1", Italic);

            // Bold-italic text
            AddHighlightRule(@"(\*\*\*|___)(?=\S)(.+?[*_]*)(?<=\S)\1", BoldItalic);
        }
    }
}
