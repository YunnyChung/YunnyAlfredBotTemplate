using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YunnyEchoBot.Controllers.Helpers
{
    using Microsoft.Azure.Documents;
    using Microsoft.Bot.Schema;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    public class ConversationReferenceData: Document
    {
        [JsonProperty(PropertyName = "id")]
        public string ChannelID { get; set; }

        [JsonProperty(PropertyName = "ChannelConversationReferenceObject")]
        public ConversationReference ChannelConversationReferenceObject { get; set; }
    }
}
