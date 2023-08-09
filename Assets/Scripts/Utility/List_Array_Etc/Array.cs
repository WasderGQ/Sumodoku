using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace WasderGQ.Utility.List_Array_Etc
{
    public static class Array
    {
        /// <summary>
        /// QuickSort lineuper
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        public static void QuickSortArray(this int[] arr, int low, int high)
        {
            if (low < high)
            {
                int partitionIndex = Partition(arr, low, high);
            
                QuickSortArray(arr, low, partitionIndex - 1);
                QuickSortArray(arr, partitionIndex + 1, high);
            }
        }

        private static int Partition(int[] arr, int low, int high) 
        {
            int pivot = arr[high];
            int i = low - 1;
        
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
        
            Swap(arr, i + 1, high);
        
            return i + 1;
        }
        
        private static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        static public void ArrayDebugger(int[] arr)
        {
            foreach (var element in arr)
            {
                Debug.Log(element);
            }
        }




    }
    
    
}
