using System.Collections.Generic;

public static class ListHelper
{
    public static bool IsEmpty<T>(this IList<T> list)
    {
        return list.Count == 0;
    }

    public static bool IsNotEmpty<T>(this IList<T> list)
    {
        return list.Count > 0;
    }
}