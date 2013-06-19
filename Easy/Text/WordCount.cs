using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Text;

namespace Easy.Text
{
    /// <summary>
    /// Implements word counting for an ITextDocument
    /// </summary>
    public class WordCount
    {
        // Document to word count
        private ITextDocument _document;

        /// <summary>
        /// Creates a new instance of WordCount
        /// </summary>
        /// <param name="document">Document</param>
        public WordCount(ITextDocument document)
        {
            this._document = document;
        }

        /// <summary>
        /// Performs an asynchronous word count
        /// </summary>
        /// <returns>Word count</returns>
        public async Task<int> Count()
        {
            string text;
            _document.GetText(TextGetOptions.None, out text);

            return await Task.Run(() =>
                {
                    int wordCount = DoCount(text);
                    return wordCount;
                });
        }

        /// <summary>
        /// Word count implementation
        /// </summary>
        /// <param name="text">String to word count</param>
        /// <returns>Word count</returns>
        private int DoCount(string text)
        {
            if (text == String.Empty)
            {
                return 0;
            }

            // This implementation is much faster than running a regex on the string
            int wc = 0;

            // We have a 2-state state machine: either on a word or not

            // Initial state
            bool onWord = char.IsLetterOrDigit(text[0]);

            // Traverse string
            for (int i = 1; i < text.Length; i++)
            {
                // Change state
                if (!char.IsLetterOrDigit(text[i]) && onWord)
                {
                    onWord = false;
                    // Increment word count only when transitioning from word to non-word
                    wc++;
                }
                // Change state
                else if (char.IsLetterOrDigit(text[i]) && !onWord)
                {
                    onWord = true;
                }
            }

            // Final state - do another increment if we end on word
            if (onWord)
            {
                wc++;
            }

            return wc;
        }
    }
}