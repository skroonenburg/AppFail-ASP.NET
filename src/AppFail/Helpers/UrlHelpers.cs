using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppfailReporting.Helpers
{
    internal static class UrlHelpers
    {
        public static string NormalizeUrl(this string url)
        {
            if (url == null)
            {
                return url;
            }

            if (url.StartsWith("~"))
            {
                return url.Substring(1, url.Length);
            }

            return url;
        }

        public static bool UrlsAreEqual(string urlA, string urlB)
        {
            if (urlA == null && urlB == null)
            {
                return true;
            }

            if ((urlA == null && urlB != null) || (urlB == null && urlA != null))
            {
                return false;
            }

            return urlA.NormalizeUrl().Equals(urlB.NormalizeUrl(), StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool UrlStartsWith(string url, string value)
        {
            if (url == null && value == null)
            {
                return true;
            }

            if ((url == null && value != null) || (value == null && url != null))
            {
                return false;
            }

            return url.ToUpperInvariant().StartsWith(value.NormalizeUrl().ToUpperInvariant());
        }

        public static bool UrlContains(string url, string value)
        {
            if (url == null && value == null)
            {
                return true;
            }

            if ((url == null && value != null) || (value == null && url != null))
            {
                return false;
            }

            return url.ToUpperInvariant().Contains(value.NormalizeUrl().ToUpperInvariant());
        }

    }
}
