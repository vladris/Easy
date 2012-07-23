using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Utils.Sleep(1000);

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
            Utils.Sleep(1000);

            // Stop auto-save
            autoSave.Stop();

            // Read back text from temp file
            var text = await FileIO.ReadTextAsync(temp);

            // Should be in sync with the udated provider
            Assert.AreEqual("49", text);
        }

        /// <summary>
        /// Enure that Stop() doesn't return while save is ongoing
        /// </summary>
        [TestMethod]
        public void TestAutoSaveStop()
        {
            var autoSave = new AutoSaveStop();

            autoSave.Start();

            // Wait for auto-save to trigger
            autoSave.Semaphore1.WaitOne();

            // Auto-saving...
            autoSave.Semaphore2.Release();

            var before = DateTime.Now;

            // DoSave is sleeping 500 ms now, Stop should return only after DoSave completes
            autoSave.Stop();

            // Should've taken at least 300 ms for DoSave to finish (actually 500 but give some room)
            Assert.IsTrue((DateTime.Now - before).TotalMilliseconds > 300);
        }

        // Used by TestAutoSaveStop
        class AutoSaveStop : AutoSave<string>
        {
            public Semaphore Semaphore1 { get; set; }
            public Semaphore Semaphore2 { get; set; }

            public AutoSaveStop()
                : base(new SaveProviderMock<string>(null, String.Empty), TimeSpan.FromMilliseconds(500))
            {
                Semaphore1 = new Semaphore(0, 1);
                Semaphore2 = new Semaphore(0, 1);
            }

            protected override void DoSave(Windows.System.Threading.ThreadPoolTimer timer)
            {
                Semaphore1.Release();
                Semaphore2.WaitOne();

                Utils.Sleep(500);
            }
        }
    }
}
