using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Windows.UI.Text;

namespace Easy.Text.Highlight
{
    /// <summary>
    /// Base syntax highlighting class
    /// </summary>
    public abstract class BaseHighlighter
    {
        /// <summary>
        /// Text range to use in highlighting
        /// </summary>
        protected ITextRange workerRange;


        /// <summary>
        /// ITextDocument to highlight
        /// </summary>
        protected ITextDocument document;

        // List of regexes to run on the document and corresponding character formats
        private List<Tuple<Regex, ITextCharacterFormat>> _highlights;

        /// <summary>
        /// Creates a new instance of BaseHighlighter
        /// </summary>
        /// <param name="document">Document to highlight</param>
        public BaseHighlighter(ITextDocument document)
        {
            this.document = document;

            // Much faster to keep a worker range rather than to keep requesting one from
            // the document
            this.workerRange = document.GetRange(0, 0);

            this._highlights = new List<Tuple<Regex, ITextCharacterFormat>>();

            // Add highlighting rules
            AddHighlightRules();          
        }

        /// <summary>
        /// Adds a highlighting rule
        /// </summary>
        /// <param name="regex">Regular expression</param>
        /// <param name="format">Associated format</param>
        protected void AddHighlightRule(Regex regex, ITextCharacterFormat format)
        {
            _highlights.Add(new Tuple<Regex, ITextCharacterFormat>(regex, format));
        }

        /// <summary>
        /// Adds a highlighting rule
        /// </summary>
        /// <param name="pattern">Regular expression pattern</param>
        /// <param name="format">Associated format</param>
        protected void AddHighlightRule(string pattern, ITextCharacterFormat format)
        {
            _highlights.Add(new Tuple<Regex, ITextCharacterFormat>(new Regex(pattern), format));
        }

        /// <summary>
        /// Deriving classes should implement this to add the highlighting rules
        /// </summary>
        protected abstract void AddHighlightRules();

        /// <summary>
        /// Selects the text range to highlight
        /// </summary>
        protected virtual void SelectRange()
        {
            // Default is whole document
            workerRange.SetRange(0, 0);
            workerRange.Expand(TextRangeUnit.Story);
        }

        /// <summary>
        /// Performs the highlighting
        /// </summary>
        public virtual void Highlight()
        {
            // Get the text from the worker range
            string text;
            SelectRange();
            workerRange.GetText(TextGetOptions.None, out text);

            // Remember start position of the worker range
            int start = workerRange.StartPosition;

            // For each highlighting rule
            foreach (var highlight in _highlights)
            {
                // For each regex match
                foreach (Match match in highlight.Item1.Matches(text))
                {
                    // Select the match and format it
                    workerRange.StartPosition = workerRange.StartPosition + match.Index;
                    workerRange.EndPosition = workerRange.StartPosition + match.Length;
                    workerRange.CharacterFormat = highlight.Item2;
                }
            }
        }
    }
}
