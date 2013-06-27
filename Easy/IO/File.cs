using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.Storage; 

namespace Easy.IO 
{
    /// <summary>
    /// File utilities
    /// </summary>
    public class File
    {
        /// <summary>
        /// Creates a unique temporary file
        /// </summary>
        /// <returns>Unique temporary file</returns>
        public static async Task<IStorageFile> CreateTemporaryFileAsync()
        {
            // Generate GUID to use as unique name
            string uniqueName = Guid.NewGuid().ToString("N");

            // Create file in app's temporary folder
            return await ApplicationData.Current.TemporaryFolder.CreateFileAsync(uniqueName);
        }

        /// <summary>
        /// Performs a transactional file save
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="content">Content</param>
        public static async Task SaveAsync(IStorageFile file, string content)
        {
            var temp = await CreateTemporaryFileAsync();
            await FileIO.WriteTextAsync(temp, content);
            await temp.MoveAndReplaceAsync(file);
        }

        /// <summary>
        /// Performs a transactional file save
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="buffer">Buffer</param>
        public static async Task SaveAsync(IStorageFile file, byte[] buffer)
        {
            var temp = await CreateTemporaryFileAsync();
            await FileIO.WriteBytesAsync(temp, buffer);
            await temp.MoveAndReplaceAsync(file);
        }

        /// <summary>
        /// Perofrms a transactional file save
        /// </summary>
        /// <param name="provider">Save provider</param>
        public static async Task SaveAsync(ISaveProvider<byte[]> provider)
        {
            await File.SaveAsync(provider.DestinationFile, provider.Data);
        }

        /// <summary>
        /// Performs a transactional file save
        /// </summary>
        /// <param name="provider">Save provider</param>
        public static async Task SaveAsync(ISaveProvider<string> provider)
        {
            await File.SaveAsync(provider.DestinationFile, provider.Data);
        }
    }
} 