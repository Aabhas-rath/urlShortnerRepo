using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CutURL.Models
{
    public class URLModel
    {
        [Required]
        [JsonProperty("orignalurl")]
        public string OriginalURL { get; set; }
        [JsonProperty("shroturl")]
        public string ShortURL { get; set; }
        [JsonIgnore]
        public string CustomURL { get; set; }
    }
}