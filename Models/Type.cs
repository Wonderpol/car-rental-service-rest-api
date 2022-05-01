using System.Text.Json.Serialization;

namespace CarRentalRestApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Type
    {
        Car,
        Caravan
    }
}