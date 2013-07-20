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

        // Semaphore making sure that save is done once Stop returns
        private Semaphore _sem;

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
            if (_timer != null)
            {
                _timer.Cancel();
            }

            _sem = new Semaphore(1, 1);
            _timer = ThreadPoolTimer.CreatePeriodicTimer(SemSave, Period);
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop()
        {
            _timer.Cancel();
            _timer = null;
            _sem.WaitOne();
        }

        // Perform save using the semaphore
        private void SemSave(ThreadPoolTimer timer)
        {
            _sem.WaitOne();
            DoSave(timer);
            _sem.Release();
        }

        /// <summary>
        /// Deriving classes should provide save implementation
        /// </summary>
        /// <param name="timer">Timer object</param>
        protected abstract void DoSave(ThreadPoolTimer timer);

        // Auto-save null object
        class AutoSaveNull : AutoSave<T>
        {
            // Null Save Provider
            class NullSaveProvider : ISaveProvider<T>
            {
                public T Data { get { return default(T); } }
                public Windows.Storage.IStorageFile DestinationFile { get { return null; } }
            }

            public AutoSaveNull()
                : base(new NullSaveProvider(), TimeSpan.MaxValue)
            {
            }

            protected override void DoSave(ThreadPoolTimer timer)
            {
                throw new NotImplementedException();
            }
        }

        // AutoSave null-object
        private static AutoSaveNull _nullObject = new AutoSaveNull();

        /// <summary>
        /// Auto-save null object
        /// </summary>
        public static AutoSave<T> NullObject
        {
            get
            {
                return _nullObject;
            }
        }
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
        /// <param name="timer">Timer object</param>
        protected override void DoSave(ThreadPoolTimer timer)
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
        /// <param name="timer">Timer object</param>
        protected override void DoSave(ThreadPoolTimer timer)
        {
            File.SaveAsync(SaveProvider).Wait();
        }
    }
}
