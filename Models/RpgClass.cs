using System.Text.Json.Serialization;

namespace Web_Api.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        knight = 1,
        Mage = 2,

        Cleric = 3,

        Assasin = 4
    }
}