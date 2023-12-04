using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SqsMessageConsummer
{
    public class MessageDto
    {
        [JsonPropertyName("pk")]
        public string Pk => Id;

        [JsonPropertyName("sk")]
        public string Sk => Pk;

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
