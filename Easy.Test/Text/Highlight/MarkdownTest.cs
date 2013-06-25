using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

using Windows.UI.Text;

using Easy.Text.Highlight;
using Easy.Test.Text.Mocks;

namespace Easy.Test.Text.Highlight
{
    /// <summary>
    /// Tests Markdown syntax highlighting
    /// </summary>
    [TestClass]
    public class MarkdownTest
    {
        /// <summary>
        /// Markdown highlighter which does not reset formatting to default - this simplifies our
        /// mocks and facilitates highlighter testing
        /// </summary>
        class MarkdownTestHighlighter : Markdown
        {
            /// <summary>
            /// Creates a new instance of MarkdownTestHighlighter
            /// </summary>
            /// <param name="document">Document to highlight</param>
            public MarkdownTestHighlighter(ITextDocument document)
                : base(document)
            {
            }

            /// <summary>
            /// Stub default formatting
            /// </summary>
            /// <param name="format">Not used</param>
            protected override void DefaultFormatting(ITextCharacterFormat format)
            {
                // Do nothing - base implementation messes up TextCharacterFormatMock properties
            }
        }

        // Mock text document
        private TextDocumentMock _document;

        // Mock text range
        // Note: we actually use the _range.Text to set the text as the highlighter
        // retrieves its text from the range, not from the document
        private TextRangeMock _range;

        // Markdown highlighter
        private Markdown _highlighter;

        /// <summary>
        /// Initializes the document, range and highlighter objects
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            // Initialize mock document
            _document = new TextDocumentMock();
            _document.Selection = new TextSelectionMock();

            // Initialize mock range
            _range = new TextRangeMock();
            _range.CharacterFormat = new TextCharacterFormatMock();
            _document.TextRange = _range;

            _highlighter = new MarkdownTestHighlighter(_document);
        }

        /// <summary>
        /// Tests text with nothig to highlight
        /// </summary>
        [TestMethod]
        public void TestNothingToHighlight()
        {
            _range.Text = "This is some text\nthat should not get highlighted.";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);
        }

        /// <summary>
        /// Tests **bold** text
        /// </summary>
        [TestMethod]
        public void TestBold2Start()
        {
            _range.Text = " **bold** ";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Bold);
        }

        /// <summary>
        /// Tests __bold__ text
        /// </summary>
        [TestMethod]
        public void TestBold2Line()
        {
            _range.Text = " __bold__ ";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Bold);
        }

        /// <summary>
        /// Tests *italic* text
        /// </summary>
        [TestMethod]
        public void TestItalic1Star()
        {
            _range.Text = " *italic* ";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);
        }

        /// <summary>
        /// Tests _italic_ text
        /// </summary>
        [TestMethod]
        public void TestItalic1Line()
        {
            _range.Text = " _italic_ ";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);
        }

        /// <summary>
        /// Tests ___bold italic___ text
        /// </summary>
        [TestMethod]
        public void TestBoldItalic3Lines()
        {
            _range.Text = " ___bold italic___ ";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Bold);
        }

        /// <summary>
        /// Tests ***bold italic*** text
        /// </summary>
        [TestMethod]
        public void TestBoldItalic3Stars()
        {
            _range.Text = " ***bold italic*** ";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Bold);
        }

        /// <summary>
        /// Tests 
        /// 
        /// title
        /// =====
        /// </summary>
        [TestMethod]
        public void TestTitleEquals()
        {
            _range.Text = "Title text\r=======\r";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Bold);
        }

        /// <summary>
        /// Tests 
        /// 
        /// title
        /// -----
        /// </summary>
        [TestMethod]
        public void TestTitleDashes()
        {
            _range.Text = "Title text\r-------\r";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Bold);
        }

        /// <summary>
        /// Tests 
        /// 
        /// # title
        /// </summary>
        [TestMethod]
        public void TestTitlePound()
        {
            _range.Text = "# Title text\r";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Bold);
        }

        /// <summary>
        /// Tests 
        /// 
        /// ###### title
        /// </summary>
        [TestMethod]
        public void TestTitlePounds()
        {
            _range.Text = "# Title text\r";

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Bold);

            _highlighter.Highlight();

            Assert.AreEqual(FormatEffect.Off, _range.CharacterFormat.Italic);
            Assert.AreEqual(FormatEffect.On, _range.CharacterFormat.Bold);
        }
    }
}
