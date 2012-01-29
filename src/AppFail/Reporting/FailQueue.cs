using System;
using System.Collections.Generic;
using System.Threading;
using AppFail.Model;

namespace AppFail.Reporting
{
    /// <summary>
    /// Manages a queue of failures that need to be reported to appfail.net.
    /// Locked to provide thread safe access.
    /// </summary>
    internal static class FailQueue
    {
        private static Queue<FailReport> _failQueue = new Queue<FailReport>();
        private static ReaderWriterLockSlim _queueLock = new ReaderWriterLockSlim();
        private static ManualResetEvent _queueSignal = new ManualResetEvent(false);
        
        public static void Enqueue(FailReport failReport)
        {
            // When recording a new fail report, make sure that the reporting worker is running
            ReportingWorker.Instance.Start();

            try
            {
                _queueLock.EnterWriteLock();
                _failQueue.Enqueue(failReport);
                
                // Signal that there is something in the queue
                _queueSignal.Set();
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

        public static FailReport Dequeue()
        {
            try
            {
                _queueLock.EnterWriteLock();
                var report = _failQueue.Dequeue();

                // if the queue holds no items, reset the queue signal
                if (_failQueue.Count == 0)
                {
                    _queueSignal.Reset();
                }

                return report;
            }
            finally
            {
                _queueLock.ExitWriteLock();
            }
        }

        public static FailReport Peek()
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
