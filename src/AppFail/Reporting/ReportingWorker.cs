using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using AppFail.Helpers;
using AppFail.Model;

namespace AppFail.Reporting
{
    /// <summary>
    /// Runs in the background and reports failures to appfail.net periodically.
    /// </summary>
    internal class ReportingWorker : BackgroundWorker
    {
        // Singleton instance
        private static ReportingWorker _instance = new ReportingWorker();

        private ReportingWorker()
        {}

        /// <summary>
        /// Gets the singleton instance of the reporting worker.
        /// </summary>
        public static ReportingWorker Instance
        {
            get { return _instance; }
        }

        public override WaitHandle DoWork()
        {
            // Send fail reports one-by-one
            // TODO: Send in batches
            var nextFailReport = FailQueue.Peek();
            
            if (nextFailReport != null && PostFailToService(nextFailReport))
            {
                // Remove this entry from the queue.
                FailQueue.Dequeue();
            }

            // Return the wait handle that will signal to
            // call this worker method when the queue is not empty.
            return FailQueue.WaitHandle;
        }

        private static bool PostFailToService(FailReport report)
        {
            var postRequest = WebRequest.Create(UrlLookup.ReportFailUrl);
            postRequest.Method = "POST";
            postRequest.ContentType = "application/x-www-form-urlencoded";
            var postData = "";
            foreach (var property in report.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = property.GetValue(report, null);
                postData += String.Format("{0}={1}&", HttpUtility.UrlEncode(property.Name), HttpUtility.UrlEncode(value != null ? value.ToString() : ""));
            }

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            postRequest.ContentLength = postData.Length;

            try
            {
                using (var requestStream = postRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }

                using (var responseStream = postRequest.GetResponse().GetResponseStream())
                using (var reader = new StreamReader(responseStream))
                {
                    var result = reader.ReadToEnd();
                }

                return true;
            }
            catch (IOException)
            {

            }

            return false;
        }
    }
}
