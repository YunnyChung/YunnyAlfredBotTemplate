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

    public class ReplyChainData : Document
    {
        [JsonProperty(PropertyName = "id")]
        public string ExternalTicketId { get; set; }

        [JsonProperty(PropertyName = "messageId")]
        public string MessageId { get; set; }
    }
}
