using System.Text.Json;

namespace DealUp.Utils;

public static class JsonUtils
{
    public static string ToJson<T>(this T data)
    {
        return JsonSerializer.Serialize(data);
    }

    public static T? FromJson<T>(string jsonData)
    {
        return JsonSerializer.Deserialize<T>(jsonData);
    }
}