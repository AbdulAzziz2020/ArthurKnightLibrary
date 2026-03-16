using Newtonsoft.Json;

namespace ArthurKnight.Core
{
    public static class JsonExtension
    {
        public static T FromJSON<T>(this string json, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static string ToJson(this object obj, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}