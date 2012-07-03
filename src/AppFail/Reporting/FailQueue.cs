using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AppfailReporting.Model;

namespace AppfailReporting.Reporting
{
    /// <summary>
    /// Manages a queue of failures that need to be reported to appfail.net.
    /// Locked to provide thread safe access.
    /// </summary>
    internal static class FailQueue
    {
        private static Queue<FailOccurrenceDto> _failQueue = new Queue<FailOccurrenceDto>();
        private static ReaderWriterLockSlim _queueLock = new ReaderWriterLockSlim();
        private static ManualResetEvent _queueSignal = new ManualResetEvent(false);

        public static void Enqueue(FailOccurrenceDto failReport)
        {
            // When recording a new fail report, make sure that the reporting worker is running
            ReportingWorker.Instance.Start();

            try
            {
                _queueLock.EnterWriteLock();
                _failQueue.Enqueue(failReport);
                
                // Signal that the queue is at least at the minimum batch size
                if (_failQueue.Count >= ConfigurationModel.Instance.ReportingMinimumBatchSize)
                {
                    _queueSignal.Set();
                }
            }
            finally
            {
                _queueLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Returns a wait handle that will eb signalled while there are items in the queue.
        /// </summary>
        public static WaitHandle WaitHandle
        {
            get { return _queueSignal; }
        }

        public static FailOccurrenceDto Dequeue()
        {
            return Dequeue(1).FirstOrDefault();
        }

        public static IEnumerable<FailOccurrenceDto> Dequeue(int count)
        {
            try
            {
                _queueLock.EnterWriteLock();

                var returnCount = Math.Min(count, _failQueue.Count);
                var fails = new FailOccurrenceDto[returnCount];

                for (var i = 0; i < returnCount; i++)
                {
                    fails[i] = _failQueue.Dequeue();
                }

                // if the queue holds less than the batch size, reset the queue signal
                if (_failQueue.Count < ConfigurationModel.Instance.ReportingMinimumBatchSize)
                {
                    _queueSignal.Reset();
                }

                return fails;
            }
            finally
            {
                _queueLock.ExitWriteLock();
            }
        }

        public static FailOccurrenceDto Peek()
        {
            try
            {
                _queueLock.EnterReadLock();
                if (_failQueue.Count == 0)
                {
                    return null;
                }

                return _failQueue.Peek();
            }
            catch(InvalidOperationException)
            {
                // Queue was empty
                return null;
            }
            finally
            {
                _queueLock.ExitReadLock();
            }
        }
    }
}
