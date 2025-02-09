using Newtonsoft.Json;

namespace DealUp.Utils;

public static class JsonUtils
{
    public static string ToJson<T>(this T data)
    {
        return JsonConvert.SerializeObject(data);
    }

    public static T? FromJson<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }
}