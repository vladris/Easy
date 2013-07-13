using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

using Windows.Storage;
using Windows.Storage.Streams;

using Easy.IO;
using Easy.Test.IO.Mocks;

namespace Easy.Test.IO
{
    /// <summary>
    /// Tests the File class
    /// </summary>
    [TestClass]
    public class FileTest
    {
        /// <summary>
        /// Test unique temp files
        /// </summary>
        [TestMethod]
        public void TestGetTemp()
        {
            var tempFiles = new HashSet<string>();

            // Generate 20 temp files
            for (int i = 0; i < 20; i++)
            {
                var fileTask = File.CreateTemporaryFileAsync();
                fileTask.Wait();
    
                // File name should not have been previously generated
                Assert.IsFalse(tempFiles.Contains(fileTask.Result.Name));

                // Add to hash set
                tempFiles.Add(fileTask.Result.Name);
            }
        }

        /// <summary>
        /// Test transactional string save
        /// </summary>
        [TestMethod]
        public void TestStringSave()
        {
            TestSaveStringAsync().Wait();
        }

        /// <summary>
        /// Test transactional buffer save
        /// </summary>
        [TestMethod]
        public void TestBufferSave()
        {
            TestSaveBufferAsync().Wait();
        }

        /// <summary>
        /// Test transactional string provider save
        /// </summary>
        [TestMethod]
        public void TestStringProviderSave()
        {
            TestSaveStringProviderAsync().Wait();
        }

        /// <summary>
        /// Test transactional buffer provider save
        /// </summary>
        [TestMethod]
        public void TestBufferProviderSave()
        {
            TestSaveBufferProviderAsync().Wait();
        }

        // Async save string test
        private async Task TestSaveStringAsync()
        {
            var tempFile = await File.CreateTemporaryFileAsync();

            string content = "Content";

            await File.SaveAsync(tempFile, content);

            string read = await FileIO.ReadTextAsync(tempFile);

            Assert.AreEqual(content, read);
        }

        // Async save buffer test
        private async Task TestSaveBufferAsync()
        {
            var tempFile = await File.CreateTemporaryFileAsync();

            byte[] buffer = new byte[] { 5, 4, 3, 2, 1 };

            await File.SaveAsync(tempFile, buffer);

            var reader = DataReader.FromBuffer(await FileIO.ReadBufferAsync(tempFile));
            byte[] read = new byte[5];
            reader.ReadBytes(read);

            for (int i = 0; i < buffer.Length; i++)
            {
                Assert.AreEqual(buffer[i], read[i]);
            }
        }

        // Async save with string provider test
        private async Task TestSaveStringProviderAsync()
        {
            var tempFile = await File.CreateTemporaryFileAsync();

            string content = "Content";

            var provider = new SaveProviderMock<string>(tempFile, content);

            await File.SaveAsync(provider);

            string read = await FileIO.ReadTextAsync(tempFile);

            Assert.AreEqual(content, read);
        }

        // Async save with bytes[] provider test
        private async Task TestSaveBufferProviderAsync()
        {
            var tempFile = await File.CreateTemporaryFileAsync();

            byte[] buffer = new byte[] { 5, 4, 3, 2, 1 };

            var provider = new SaveProviderMock<byte[]>(tempFile, buffer);

            await File.SaveAsync(provider);

            var reader = DataReader.FromBuffer(await FileIO.ReadBufferAsync(tempFile));
            byte[] read = new byte[5];
            reader.ReadBytes(read);

            for (int i = 0; i < buffer.Length; i++)
            {
                Assert.AreEqual(buffer[i], read[i]);
            }
        }
    }
}
