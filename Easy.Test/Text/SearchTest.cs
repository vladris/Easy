using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

using Windows.UI.Text;

using Easy.Text;
using Easy.Test.Text.Mocks;

namespace Easy.Test.Text
{
    /// <summary>
    /// Tests Search
    /// </summary>
    [TestClass]
    public class SearchTest
    {
        // Mock document and Search object to be tested
        private TextDocumentMock _document;
        private Search _search;

        /// <summary>
        /// Initializes the document and Search object to be tested
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _document = new TextDocumentMock();
            _document.Selection = new TextSelectionMock();

            _search = new Search(_document);
        }

        /// <summary>
        /// Tests FindFirst
        /// </summary>
        [TestMethod]
        public void TestFindFirst()
        {
            _document.Text = "some string to find";

            _search.FindFirst("string");

            Assert.AreEqual(5, _document.Selection.StartPosition);
            Assert.AreEqual(11, _document.Selection.EndPosition);
        }

        /// <summary>
        /// Tests FindNext
        /// </summary>
        [TestMethod]
        public void TestFindNext()
        {
            _document.Text = "some string string to find";

            _search.FindFirst("string");
            _search.FindNext("string");

            Assert.AreEqual(12, _document.Selection.StartPosition);
            Assert.AreEqual(18, _document.Selection.EndPosition);
        }

        /// <summary>
        /// Tests FindFirst for a non-existent string
        /// </summary>
        [TestMethod]
        public void TestNotFoundFirst()
        {
            _document.Text = "some string";

            _document.Selection.SetRange(1, 2);

            _search.FindFirst("foo");

            Assert.AreEqual(1, _document.Selection.StartPosition);
            Assert.AreEqual(2, _document.Selection.EndPosition);
        }

        /// <summary>
        /// Tests FindNext for a non-existent string
        /// </summary>
        [TestMethod]
        public void TestNotFoundNext()
        {
            _document.Text = "some string";

            _search.FindFirst("some");

            Assert.AreEqual(0, _document.Selection.StartPosition);
            Assert.AreEqual(4, _document.Selection.EndPosition);

            _search.FindNext("foo");

            Assert.AreEqual(0, _document.Selection.StartPosition);
            Assert.AreEqual(4, _document.Selection.EndPosition);
        }

        /// <summary>
        /// Tests FindNext wrap around
        /// </summary>
        [TestMethod]
        public void TestWrapAround()
        {
            _document.Text = "some foo string to find foo";

            _search.FindFirst("foo");
            _search.FindNext("foo");
            _search.FindNext("foo");

            Assert.AreEqual(5, _document.Selection.StartPosition);
            Assert.AreEqual(8, _document.Selection.EndPosition);
        }

        /// <summary>
        /// Tests FindNext of different string after FindFirst
        /// </summary>
        [TestMethod]
        public void TestFindFirstFindOtherNext()
        {
            _document.Text = "some foo string to find foo";

            _search.FindFirst("string");
            _search.FindNext("foo");

            // Should find second "foo" (the one after "string")
            Assert.AreEqual(24, _document.Selection.StartPosition);
            Assert.AreEqual(27, _document.Selection.EndPosition);
        }
    }
}
