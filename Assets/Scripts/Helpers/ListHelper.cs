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

    public static T RandomElement<T>(this List<T> list)
    {
        if(list.IsEmpty()) return default(T);
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
}