using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Extentions
{
    static Random rng = new Random();

    /// <summary>
    /// Shuffle any (I)List with an extension method based on the Fisher-Yates shuffle
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    /// <summary>
    /// Shuffle any (I)Array with an extension method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    public static T[] Shuffle<T>(this T[] array)
    {
        Random rnd = new Random();
        return array.OrderBy(x => rnd.Next()).ToArray();
    }

}
