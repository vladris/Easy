using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

using Windows.Storage;
using Windows.Storage.Streams;

using Easy.IO;
using Easy.Test.IO.Mocks;

namespace Easy.Test.IO
{
    /// <summary>
    /// Tests the AutoSave classes
    /// </summary>
    [TestClass]
    public class AutoSaveTest
    {
        /// <summary>
        /// Tests bytes auto-save
        /// </summary>
        [TestMethod]
        public void TestAutoSaveBytes()
        {
            TestAutoSaveBytesAsync().Wait();
        }

        // Async test bytes auto-save
        private async Task TestAutoSaveBytesAsync()
        {
            // Create a temp file and mock provider
            var temp = await File.CreateTemporaryFileAsync();
            var mockProvider = new SaveProviderMock<byte[]>(temp, new byte[5]);
            
            // Setup an auto-save every 10 ms
            AutoSaveBytes autoSave = new AutoSaveBytes(mockProvider, TimeSpan.FromMilliseconds(500));
            autoSave.Start();

            // Update provider data a few times
            for (byte i = 0; i < 50; i++)
            {
                mockProvider.Data[4] = i;
            }

            // Make sure auto-save has time to trigger and finish
            Sleep(1000);

            // Stop auto-save
            autoSave.Stop();

            // Read back data from temp file
            var reader = DataReader.FromBuffer(await FileIO.ReadBufferAsync(temp));
            byte[] buffer = new byte[5];
            reader.ReadBytes(buffer);

            // Should be in sync with the udated provider
            Assert.AreEqual(0, buffer[0]);
            Assert.AreEqual(0, buffer[1]);
            Assert.AreEqual(0, buffer[2]);
            Assert.AreEqual(0, buffer[3]);
            Assert.AreEqual(49, buffer[4]);
        }

        /// <summary>
        /// Tests string auto-save
        /// </summary>
        [TestMethod]
        public void TestAutoSaveString()
        {
            TestAutoSaveStringAsync().Wait();
        }

        // Async test string auto-save
        private async Task TestAutoSaveStringAsync()
        {
            // Create a temp file and mock provider
            var temp = await File.CreateTemporaryFileAsync();
            var mockProvider = new SaveProviderMock<string>(temp, "");
            
            // Setup an auto-save every 10 ms
            AutoSaveString autoSave = new AutoSaveString(mockProvider, TimeSpan.FromMilliseconds(500));
            autoSave.Start();

            // Update provider data a few times
            for (int i = 0; i < 50; i++)
            {
                mockProvider.Data = i.ToString();
            }

            // Make sure auto-save has time to trigger and finish
            Sleep(1000);

            // Stop auto-save
            autoSave.Stop();

            // Read back text from temp file
            var text = await FileIO.ReadTextAsync(temp);

            // Should be in sync with the udated provider
            Assert.AreEqual("49", text);
        }

        // Simple sleep implementation
        private void Sleep(int milliseconds)
        {
            DateTime start = DateTime.Now;
            while ((DateTime.Now - start).TotalMilliseconds < milliseconds) ;
        }
    }
}
