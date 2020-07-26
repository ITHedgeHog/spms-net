using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SPMS.Common
{
    public class WebhookCall
    {
        [JsonPropertyName("content")]
        public string Content { get; set; } = "";
        [JsonPropertyName("username")]
        public string Username { get; set; } = "";
        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; } = "";
        // ReSharper disable once InconsistentNaming
        [JsonPropertyName("tts")]
        public bool IsTTS { get; set; }
        [JsonPropertyName("embeds")]
        public List<Embed> Embeds { get; set; } = new List<Embed>();

    }


    public class Embed
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("type")] public string Type => "rich";

        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
        [JsonPropertyName("color")]
        public string Colour { get; set; }
        [JsonPropertyName("footer")]
        public string Footer { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }
        [JsonPropertyName("video")]
        public string Video { get; set; }
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("author")] public string Author { get; set; }
        [JsonPropertyName("fields")]
        public List<string> Fields { get; set; }

        //    thumbnail?	embed thumbnail object thumbnail information
        //    video?	embed video object video information
        //    provider?	embed provider object provider information
        //    author?	embed author object author information
        //    fields?	array of embed field objects fields information

    }
}