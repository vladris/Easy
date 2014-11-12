using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Windows.System.Threading;

namespace Easy.IO
{
    /// <summary>
    /// Abstract base class for auto-save functionality
    /// </summary>
    public abstract class AutoSave<T>
    {
        /// <summary>
        /// Gets the save provider
        /// </summary>
        public ISaveProvider<T> SaveProvider { get; private set; }

        /// <summary>
        /// Gets the timer period
        /// </summary>
        public TimeSpan Period { get; private set; }

        // Internal timer used for the task
        private ThreadPoolTimer _timer = null;

        // Lock to make sure saves don't overlap
        private object _lockObject = new object();

        /// <summary>
        /// Creates a new instance of AutoSave
        /// </summary>
        /// <param name="saveProvider">Save provider</param>
        /// <param name="period">Timer period</param>
        public AutoSave(ISaveProvider<T> saveProvider, TimeSpan period)
        {
            this.SaveProvider = saveProvider;
            this.Period = period;
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void Start()
        {
            Stop();

            _timer = ThreadPoolTimer.CreatePeriodicTimer(SemSave, Period);
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Cancel();
                _timer = null;
            }
        }

        /// <summary>
        /// Perform save
        /// </summary>
        /// <param name="timer">Unused</param>
        private void SemSave(ThreadPoolTimer timer)
        {
            // Critical section
            lock (_lockObject)
            {
                DoSave();
            }
        }

        /// <summary>
        /// Deriving classes should provide save implementation
        /// </summary>
        protected abstract void DoSave();
    }

    /// <summary>
    /// Auto-save for string save provider
    /// </summary>
    public class AutoSaveString : AutoSave<string>
    {
        /// <summary>
        /// Creates a new instance of AutoSaveString
        /// </summary>
        /// <param name="provider">Save provider</param>
        /// <param name="period">Timer period</param>
        public AutoSaveString(ISaveProvider<string> provider, TimeSpan period)
            : base(provider, period)
        {
        }

        /// <summary>
        /// Save implementation
        /// </summary>
        protected override void DoSave()
        {
            File.SaveAsync(SaveProvider).Wait();
        }
    }

    /// <summary>
    /// Auto-save for byte[] save provider
    /// </summary>
    public class AutoSaveBytes : AutoSave<byte[]>
    {
        /// <summary>
        /// Creates a new instance of AutoSaveBytes
        /// </summary>
        /// <param name="provider">Save provider</param>
        /// <param name="period">Timer period</param>
        public AutoSaveBytes(ISaveProvider<byte[]> provider, TimeSpan period)
            : base(provider, period)
        {
        }

        /// <summary>
        /// Save implementation
        /// </summary>
        protected override void DoSave()
        {
            File.SaveAsync(SaveProvider).Wait();
        }
    }
}
