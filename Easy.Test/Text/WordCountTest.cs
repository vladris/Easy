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
    /// Tests WordCount
    /// </summary>
    [TestClass]
    public class WordCountTest
    {
        // ITextDocument mock and WordCount object to be tested
        private TextDocumentMock _document;
        private WordCount _wordCount;

        /// <summary>
        /// Initializes the mock and the WordCount object
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _document = new TextDocumentMock();
            _wordCount = new WordCount(_document);
        }

        /// <summary>
        /// Tests word count on an empty text
        /// </summary>
        [TestMethod]
        public void TestEmptyWordCount()
        {
            Assert.AreEqual(0, GetWordCount(String.Empty));
        }

        /// <summary>
        /// Tests word count on a large text
        /// </summary>
        [TestMethod]
        public void TestLargeWordCount()
        {
            StringBuilder text = new StringBuilder();

            const int Words = 2000;

            // Generate words
            for (int i = 0; i < Words; i++)
            {
                text.Append(" ");
                text.Append("word");
                text.Append(i);
            }

            Assert.AreEqual(Words, GetWordCount(text.ToString()));
        }

        /// <summary>
        /// Test some interesting strings to ensure proper wordcount
        /// </summary>
        [TestMethod]
        public void TestFunkyStrings()
        {
            // Whitespace
            Assert.AreEqual(2, GetWordCount("     two\t words \r \n"));

            // Punctuation
            Assert.AreEqual(4, GetWordCount("word1;word2.word3-word4"));
        }

        /// <summary>
        /// Executes word count synchronously for a given string
        /// </summary>
        /// <param name="text">Text string</param>
        /// <returns>Word count</returns>
        private int GetWordCount(string text)
        {
            _document.Text = text;

            var wc = _wordCount.Count();
            wc.Wait();
            return wc.Result;
        }
    }
}
