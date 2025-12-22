using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void Shuffle<T>(this Queue<T> queue)
    {
        var list = new List<T>(queue);
        queue.Clear();

        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }

        foreach (var item in list)
        {
            queue.Enqueue(item);
        }
    }
     public static void Shuffle<T>(this List<T> queue)
    {
        var list = new List<T>(queue);
        queue.Clear();

        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }

        foreach (var item in list)
        {
            queue.Add(item);
        }
    }

    public static string DistanceFormat(float distanceInMeters)
    {
        if (distanceInMeters < 1000f)
        {
            return $"{Mathf.FloorToInt(distanceInMeters)} m";
        }

        float km = distanceInMeters / 1000f;
        return $"{km:F1} km";
    }
}
