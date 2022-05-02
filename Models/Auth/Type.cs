using System.Text.Json.Serialization;

namespace CarRentalRestApi.Models.Auth
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Type
    {
        Car,
        Caravan
    }
}