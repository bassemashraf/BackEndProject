using System.Text.Json.Serialization;
namespace BackEnd.models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RGPClaSS
    {
        Knight =1 ,
        mage = 2 ,
        Cleric = 3 
    }

}
