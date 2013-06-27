using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;

namespace Easy.IO
{
    /// <summary>
    /// Provides data required for a save operation (IStorageFile and content)
    /// </summary>
    public interface ISaveProvider<T>
    {
        /// <summary>
        /// Gets the file in which data will be saved
        /// </summary>
        IStorageFile DestinationFile { get; }

        /// <summary>
        /// Gets the data to be saved
        /// </summary>
        T Data { get; }
    }
}
