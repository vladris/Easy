using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Text;

namespace Easy.Test.Text.Mocks
{
    /// <summary>
    /// ITextSelection mock
    /// </summary>
    class TextSelectionMock : ITextSelection
    {
        #region ITextSelection
        public int EndKey(TextRangeUnit unit, bool extend)
        {
            throw new NotImplementedException();
        }

        public int HomeKey(TextRangeUnit unit, bool extend)
        {
            throw new NotImplementedException();
        }

        public int MoveDown(TextRangeUnit unit, int count, bool extend)
        {
            throw new NotImplementedException();
        }

        public int MoveLeft(TextRangeUnit unit, int count, bool extend)
        {
            throw new NotImplementedException();
        }

        public int MoveRight(TextRangeUnit unit, int count, bool extend)
        {
            throw new NotImplementedException();
        }

        public int MoveUp(TextRangeUnit unit, int count, bool extend)
        {
            throw new NotImplementedException();
        }

        public SelectionOptions Options
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

        public SelectionType Type
        {
            get { throw new NotImplementedException(); }
        }

        public void TypeText(string value)
        {
            throw new NotImplementedException();
        }

        public bool CanPaste(int format)
        {
            throw new NotImplementedException();
        }

        public void ChangeCase(LetterCase value)
        {
            throw new NotImplementedException();
        }

        public char Character
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

        public ITextCharacterFormat CharacterFormat
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

        public void Collapse(bool value)
        {
            throw new NotImplementedException();
        }

        public void Copy()
        {
            throw new NotImplementedException();
        }

        public void Cut()
        {
            throw new NotImplementedException();
        }

        public int Delete(TextRangeUnit unit, int count)
        {
            throw new NotImplementedException();
        }

        public int EndOf(TextRangeUnit unit, bool extend)
        {
            throw new NotImplementedException();
        }

        public int EndPosition { get; set; }

        public int Expand(TextRangeUnit unit)
        {
            throw new NotImplementedException();
        }

        public int FindText(string value, int scanLength, FindOptions options)
        {
            throw new NotImplementedException();
        }

        public ITextRange FormattedText
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

        public void GetCharacterUtf32(out uint value, int offset)
        {
            throw new NotImplementedException();
        }

        public ITextRange GetClone()
        {
            throw new NotImplementedException();
        }

        public int GetIndex(TextRangeUnit unit)
        {
            throw new NotImplementedException();
        }

        public void GetPoint(HorizontalCharacterAlignment horizontalAlign, VerticalCharacterAlignment verticalAlign, PointOptions options, out Windows.Foundation.Point point)
        {
            throw new NotImplementedException();
        }

        public void GetRect(PointOptions options, out Windows.Foundation.Rect rect, out int hit)
        {
            throw new NotImplementedException();
        }

        public void GetText(TextGetOptions options, out string value)
        {
            throw new NotImplementedException();
        }

        public void GetTextViaStream(TextGetOptions options, Windows.Storage.Streams.IRandomAccessStream value)
        {
            throw new NotImplementedException();
        }

        public RangeGravity Gravity
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

        public bool InRange(ITextRange range)
        {
            throw new NotImplementedException();
        }

        public bool InStory(ITextRange range)
        {
            throw new NotImplementedException();
        }

        public void InsertImage(int width, int height, int ascent, VerticalCharacterAlignment verticalAlign, string alternateText, Windows.Storage.Streams.IRandomAccessStream value)
        {
            throw new NotImplementedException();
        }

        public bool IsEqual(ITextRange range)
        {
            throw new NotImplementedException();
        }

        public int Length
        {
            get { throw new NotImplementedException(); }
        }

        public string Link
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

        public void MatchSelection()
        {
            throw new NotImplementedException();
        }

        public int Move(TextRangeUnit unit, int count)
        {
            throw new NotImplementedException();
        }

        public int MoveEnd(TextRangeUnit unit, int count)
        {
            throw new NotImplementedException();
        }

        public int MoveStart(TextRangeUnit unit, int count)
        {
            throw new NotImplementedException();
        }

        public ITextParagraphFormat ParagraphFormat
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

        public void Paste(int format)
        {
            throw new NotImplementedException();
        }

        public void ScrollIntoView(PointOptions value)
        {
            throw new NotImplementedException();
        }

        public void SetIndex(TextRangeUnit unit, int index, bool extend)
        {
            throw new NotImplementedException();
        }

        public void SetPoint(Windows.Foundation.Point point, PointOptions options, bool extend)
        {
            throw new NotImplementedException();
        }

        public void SetRange(int startPosition, int endPosition)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public void SetText(TextSetOptions options, string value)
        {
            throw new NotImplementedException();
        }

        public void SetTextViaStream(TextSetOptions options, Windows.Storage.Streams.IRandomAccessStream value)
        {
            throw new NotImplementedException();
        }

        public int StartOf(TextRangeUnit unit, bool extend)
        {
            throw new NotImplementedException();
        }

        public int StartPosition { get; set; }

        public int StoryLength
        {
            get { throw new NotImplementedException(); }
        }

        public string Text
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
