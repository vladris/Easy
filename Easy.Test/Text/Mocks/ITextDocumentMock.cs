using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Text;

namespace Easy.Test.Text.Mocks
{
    /// <summary>
    /// ITextDocument mock
    /// </summary>
    class TextDocumentMock : ITextDocument
    {
        /// <summary>
        /// Text returned by GetText
        /// </summary>
        public string Text { get; set; }

        #region ITextDocument
        public int ApplyDisplayUpdates()
        {
            throw new NotImplementedException();
        }

        public int BatchDisplayUpdates()
        {
            throw new NotImplementedException();
        }

        public void BeginUndoGroup()
        {
            throw new NotImplementedException();
        }

        public bool CanCopy()
        {
            throw new NotImplementedException();
        }

        public bool CanPaste()
        {
            throw new NotImplementedException();
        }

        public bool CanRedo()
        {
            throw new NotImplementedException();
        }

        public bool CanUndo()
        {
            throw new NotImplementedException();
        }

        public CaretType CaretType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float DefaultTabStop
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void EndUndoGroup()
        {
            throw new NotImplementedException();
        }

        public ITextCharacterFormat GetDefaultCharacterFormat()
        {
            throw new NotImplementedException();
        }

        public ITextParagraphFormat GetDefaultParagraphFormat()
        {
            throw new NotImplementedException();
        }

        public ITextRange GetRange(int startPosition, int endPosition)
        {
            throw new NotImplementedException();
        }

        public ITextRange GetRangeFromPoint(Windows.Foundation.Point point, PointOptions options)
        {
            throw new NotImplementedException();
        }

        public void GetText(TextGetOptions options, out string value)
        {
            value = Text;
        }

        public void LoadFromStream(TextSetOptions options, Windows.Storage.Streams.IRandomAccessStream value)
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void SaveToStream(TextGetOptions options, Windows.Storage.Streams.IRandomAccessStream value)
        {
            throw new NotImplementedException();
        }

        public ITextSelection Selection
        {
            get { throw new NotImplementedException(); }
        }

        public void SetDefaultCharacterFormat(ITextCharacterFormat value)
        {
            throw new NotImplementedException();
        }

        public void SetDefaultParagraphFormat(ITextParagraphFormat value)
        {
            throw new NotImplementedException();
        }

        public void SetText(TextSetOptions options, string value)
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public uint UndoLimit
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
