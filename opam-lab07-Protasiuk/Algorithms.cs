using System;
using System.Collections.Generic;

namespace OpamLab07;

public static class Algorithms
{
    public static List<T> Filter<T>(List<T> src, Func<T, bool> keep)
    {
        var result = new List<T>();
        foreach (T item in src)
            if (keep(item))
                result.Add(item);
        return result;
    }

    public static List<TOut> Map<TIn, TOut>(List<TIn> src, Func<TIn, TOut> convert)
    {
        var result = new List<TOut>();
        foreach (TIn item in src)
            result.Add(convert(item));
        return result;
    }

    public static TAcc Aggregate<T, TAcc>(List<T> src, TAcc start, Func<TAcc, T, TAcc> step)
    {
        TAcc state = start;
        foreach (T item in src)
            state = step(state, item);
        return state;
    }

    public static Dictionary<TKey, List<T>> GroupBy<T, TKey>(
        List<T> src, Func<T, TKey> keyOf) where TKey : notnull
    {
        var result = new Dictionary<TKey, List<T>>();
        foreach (T item in src)
        {
            TKey key = keyOf(item);
            if (!result.ContainsKey(key))
                result[key] = new List<T>();
            result[key].Add(item);
        }
        return result;
    }
}
