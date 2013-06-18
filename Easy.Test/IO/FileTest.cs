using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

using Windows.Storage;

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
    }
}
