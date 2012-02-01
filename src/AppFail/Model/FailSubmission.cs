using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFail.Model
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
