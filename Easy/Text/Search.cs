using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Text;

namespace Easy.Text
{
    /// <summary>
    /// Enables text search on an ITextDocument
    /// </summary>
    public class Search
    {
        // ITextDocument instance to search in
        private ITextDocument _document;

        // Text content
        private string _text;

        // String to search for
        private string _searchString;

        // Current index
        private int _currentIndex;

        /// <summary>
        /// Creates a new instance of Search
        /// </summary>
        /// <param name="document">ITextDocument to serch</param>
        public Search(ITextDocument document)
        {
            this._document = document;
            this._searchString = String.Empty;
            this._currentIndex = -1;
        }

        /// <summary>
        /// Searches for the first occurrence of a string in the text
        /// </summary>
        /// <param name="searchString">String to find</param>
        public void FindFirst(string searchString)
        {
            // This is the equivalent of FindNext starting from the beginning of document
            _currentIndex = -1;
            _searchString = searchString;

            FindNext();
        }

        /// <summary>
        /// Searches for the next occurence of a string in the text after the current position
        /// </summary>
        /// <param name="searchString">String to find</param>
        public void FindNext()
        {
            // Text can always change while we search, so update it
            _document.GetText(TextGetOptions.None, out this._text);

            // Search just in the string after the current find
            string searchIn = _currentIndex < 0 ? _text : _text.Substring(_currentIndex + 1);
            int pos = searchIn.IndexOf(_searchString, StringComparison.CurrentCultureIgnoreCase);

            // If we found something
            if (pos > -1)
            {
                // Select it and update index of find
                _document.Selection.SetRange(_currentIndex + pos + 1, _currentIndex + pos + _searchString.Length + 1);
                _currentIndex = _currentIndex + pos + 1;
            }
            // If we didn't find anything, start from the top and try again
            else if (_currentIndex > -1)
            {
                _currentIndex = -1;
                FindNext();
            }
        }
    }
}
