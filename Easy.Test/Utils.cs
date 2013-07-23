using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Test
{
    class Utils
    {
        /// <summary>
        /// Simple sleep implementation
        /// </summary>
        /// <param name="milliseconds">Milliseconds to sleep</param>
        public static void Sleep(int milliseconds)
        {
            DateTime start = DateTime.Now;
            while ((DateTime.Now - start).TotalMilliseconds < milliseconds) ;
        }
    }
}
