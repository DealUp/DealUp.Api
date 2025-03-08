namespace DealUp.Utils;

public static class EnumUtils
{
    public static T ToEnum<T>(this string enumString, bool ignoreCase = false) where T : Enum
    {
        if (!Enum.TryParse(typeof(T), enumString, ignoreCase: ignoreCase, out object? result))
        {
            throw new Exception($"{enumString} can't parse to {typeof(T)}");
        }
        return (T)result;
    }

    public static T? ToEnumOrDefault<T>(this string enumString, bool ignoreCase = false) where T : Enum
    {
        if (!Enum.TryParse(typeof(T), enumString, ignoreCase: ignoreCase, out object? result))
        {
            return default;
        }
        return (T)result;
    }
}