// This class simply mimics fake JSON Object.
// EchoBot 'test' command leverages this data for testing purpose.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YunnyEchoBot.Bots
{
    public class MockData
    {
        public string Id;
        public string Title;
        public int Severity;
        public List<string> Mentions;
        public string Status;

        // construct a mock data object with default values.
        // As this is for testing, I create a set of testing mentions here.
        public MockData(string id, string title, int severity, string status)
        {
            Id = id;
            Title = title;
            Severity = severity;
            Mentions = new List<string> { "sunchung@microsoft.com", "dmxtst14@microsoft.com" };
            Status = status;
        }
    }
}
