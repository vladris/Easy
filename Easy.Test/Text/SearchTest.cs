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
            _search.FindNext();

            Assert.AreEqual(12, _document.Selection.StartPosition);
            Assert.AreEqual(18, _document.Selection.EndPosition);
        }

        /// <summary>
        /// Tests FindNext wrap around
        /// </summary>
        [TestMethod]
        public void WrapAround()
        {
            _document.Text = "some foo string to find foo";

            _search.FindFirst("foo");
            _search.FindNext();
            _search.FindNext();

            Assert.AreEqual(5, _document.Selection.StartPosition);
            Assert.AreEqual(8, _document.Selection.EndPosition);
        }
    }
}
