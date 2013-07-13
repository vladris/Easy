using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;

using Easy.IO;

namespace Easy.Test.IO.Mocks
{
    // Mock ISaveProvider implementation
    class SaveProviderMock<T> : ISaveProvider<T>
    {
        public IStorageFile DestinationFile { get; set; }
        public T Data { get; set; }

        public SaveProviderMock(IStorageFile destinationFile, T data)
        {
            DestinationFile = destinationFile;
            Data = data;
        }
    }
}
