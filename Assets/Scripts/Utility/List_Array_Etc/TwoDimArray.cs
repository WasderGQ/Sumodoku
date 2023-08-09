using System;
using System.Collections.Generic;
using UnityEngine;

namespace WasderGQ.Utility.List_Array_Etc
{
    public static class TwoDimArray 
    {
        public static int[] FindIndex<T>(this T[,] array,T searchElement) //Find your elemant index on 2DimArray
        {
            int rowIndex = default(int);
            int columnIndex = default(int);
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (comparer.Equals(array[i, j], searchElement))
                    {
                        rowIndex = i;
                        columnIndex = j;
                        return new int[2] { i, j };
                    }
                }
            }
            Debug.LogWarning("This element can't find on Array");
            return new int[2] { -1, -1 };
        }
   
    }
}
