using System.Collections.Generic;

namespace AppfailReporting.Model
{
    internal class FailSubmission
    {
        public FailSubmission(string apiToken, IEnumerable<FailOccurrence> failOccurences)
        {
            ApiToken = apiToken;
            FailOccurrences = failOccurences;
        }

        public string ApiToken { get; private set; }
        public IEnumerable<FailOccurrence> FailOccurrences { get; private set; }
    }
}
