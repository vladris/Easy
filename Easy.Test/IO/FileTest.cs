using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

using Windows.Storage;
using Windows.Storage.Streams;

using Easy.IO;

namespace Easy.Test.IO
{
    // Tests the File class
    [TestClass]
    public class FileTest
    {
        // Test unique temp files
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

        // Test transactional string save
        [TestMethod]
        public void TestStringSave()
        {
            TestSaveStringAsync().Wait();
        }

        // Test transactional buffer save
        [TestMethod]
        public void TestBufferSave()
        {
            TestSaveBufferAsync().Wait();
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
    }
}
