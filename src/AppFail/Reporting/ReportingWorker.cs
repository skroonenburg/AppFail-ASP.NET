using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
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
        private static JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();

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
            var failOccurrences = FailQueue.Dequeue(ConfigurationModel.Instance.ReportingMinimumBatchSize);

            if (failOccurrences.Count() > 0)
            {
                var failSubmission = new FailSubmission(ConfigurationModel.Instance.ApiToken, failOccurrences);

                PostToService(failSubmission);
            }

            // Return the wait handle that will signal to
            // call this worker method when the queue is not empty.
            return FailQueue.WaitHandle;
        }

        private static bool PostToService(FailSubmission failSubmission)
        {
            if (!failSubmission.FailOccurrences.Any())
            {
                return true;
            }

            var postRequest = WebRequest.Create(UrlLookup.ReportFailUrl);
            postRequest.Method = "POST";
            postRequest.ContentType = "application/json; charset=utf-8'";
            postRequest.Headers.Add("x-appfail-version", UrlLookup.ReportApiVersion);

            var postData = _javaScriptSerializer.Serialize(failSubmission);

            var encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            postRequest.ContentLength = postData.Length;

            try
            {
                using (var requestStream = postRequest.GetRequestStream())
                {
                    requestStream.Write(data, 0, data.Length);
                }

                //We need to be careful here - if the remote server returned an error: (500) Internal Server Error 
                try
                {
                    using (var responseStream = postRequest.GetResponse().GetResponseStream())
                    {
                        Debug.Assert(responseStream != null, "responseStream != null");
                        using (var reader = new StreamReader(responseStream))
                        {
                            var result = reader.ReadToEnd();
                        }
                    }
                }
                catch (WebException webException)
                {
                    Debug.WriteLine(String.Format("Could not connect to the API - {0}", webException.Message));
                }
                catch(Exception exception)
                {
                    Debug.WriteLine(String.Format("An exception occured - {0}", exception.Message));
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
