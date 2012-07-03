using System.Collections.Generic;
using System.Reflection;

namespace AppfailReporting.Model
{
    internal class FailSubmissionDto
    {
        public FailSubmissionDto(string apiToken, IEnumerable<FailOccurrenceDto> failOccurences)
        {
            ApiToken = apiToken;
            FailOccurrences = failOccurences;
            ApplicationType = "ASP.NET";
        }

        public string ApiToken { get; private set; }
        public IEnumerable<FailOccurrenceDto> FailOccurrences { get; private set; }
        public string ApplicationType { get; private set; }

        public string ModuleVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}
