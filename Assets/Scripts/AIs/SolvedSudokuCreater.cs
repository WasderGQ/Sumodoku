using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WasderGQ.Sudoku.Scenes.GameScene.Game;
using Random = UnityEngine.Random;


namespace WasderGQ.Sudoku.AIs
{
    public class SolvedSudokuCreater
    {
        private Zone[,] _processedZones;
        private Parsel[] _processedParsels;
        private List<int> _firstFillParsels;


        public SolvedSudokuCreater(Parsel[] parsels,Zone[,] zones)
        {
            _processedZones = zones ;
            _processedParsels = parsels;
        }

        ///  ----<Region How to Work>----
        /// Metod Task<bool> Start() ///
        /// first step select parsel 0 or 2.
        /// second step filling chose parsel and opposite cornerside parsel (distance between 2 corner parsel : 6).
        /// third step this filled 2 corner parsel crossing 2 different cornel parsel this 2 parsel looking impossible value from filled parsels for its zones .
        /// fourth step this impossible value removing from possibleValues on zone.
        /// fifth step create zone list most possibleValue count to lowest possible value
        /// sixth step fill parsel's zone by created step fifth zone list
        ///
        ///
        ///
        ///
        ///
        ///
        /// Finally Done
        ///     ----<EndRegion>---

        public async Task<bool> Start()
        {
            bool isDone = await FillParsels();
            return isDone;
        }

        //step 1
        private async Task<bool> FillParsels() 
        {
            FillParsel(_processedParsels[0],false);
            FillParsel(_processedParsels[8],false);
            FillParsel(_processedParsels[2],true);
            FillParsel(_processedParsels[6],true);
            FillParsel(_processedParsels[1],true);
            FillParsel(_processedParsels[3],true);
            FillParsel(_processedParsels[5],true);
            FillParsel(_processedParsels[7],true);
            FillParsel(_processedParsels[4],true);
            return true;
        }
        
        private void RemoveImpossibleValuesFromParselZones(Parsel parsel)
        {
            foreach (var zone in parsel.ZonesInParsel)
            {
                for (int i = 0; i < _processedZones.GetLength(1); i++)
                {
                    if (_processedZones[zone.ZoneID[0], i].MyValue != 0)
                    {
                        zone.RemoveValueFromPossibleValues(_processedZones[zone.ZoneID[0], i]);
                    }
                    
                }
                for (int i = 0; i < _processedZones.GetLength(0); i++)
                {
                    if (_processedZones[i,zone.ZoneID[1]].MyValue != 0)
                    {
                        zone.RemoveValueFromPossibleValues(_processedZones[i,zone.ZoneID[1]]);
                    }
                    
                }
            }
            
        }
        private async void FillParsel(Parsel parsel,bool startWithLowestPossibleValue)
        {
            if (startWithLowestPossibleValue == false)
            {
                foreach (var zone in parsel.ZonesInParsel)
                {
                    int valueIndex = Convert.ToInt32(Mathf.Floor(Random.Range(0,zone.PossibleValueList.Count-1)));
                    int value = zone.GetValueOnPossibleValueList(valueIndex);
                    zone.WriteValue(value);
                    parsel.ParselRemovePossibleValueListOnZone(value);
                } 
            }
            else
            {
                RemoveImpossibleValuesFromParselZones(parsel);
                FindLowerSortArray(parsel.ZonesInParsel);
                foreach (var zone in parsel.ZonesInParsel)
                {
                    if (zone.PossibleValueList.Count != 0)
                    {
                        int valueIndex = Convert.ToInt32(Mathf.Floor(WasderGQRandom._random.Next(0,zone.PossibleValueList.Count-1)));
                        int value = zone.GetValueOnPossibleValueList(valueIndex);
                        zone.WriteValue(value);
                        parsel.ParselRemovePossibleValueListOnZone(value);
                    }
                }
            }
            if(!IsAllZoneTakeValue(parsel))
            {
                ReBootSudokuCreater();
            }
        }
        private bool IsAllZoneTakeValue(Parsel parsel)
        {
            foreach (var zone in parsel.ZonesInParsel)
            {
                if (zone.MyValue == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private void ReBootSudokuCreater()
        {
            foreach (var parsel in _processedParsels)
            {
                foreach (var zone  in parsel.ZonesInParsel)
                {
                    zone.SetPossiblyValues();
                    zone.SetMyValueDefault();
                }
            }
            FillParsels();
        }
        private void FindLowerSortArray(Zone[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
              int lowest= FindLowest(array,i);
              Swap(array,i,lowest);
            }
        }
        private int FindLowest(Zone[] arr,int selectedIndex)
        {
            int lowestValueIndex = selectedIndex;
            for (int i = selectedIndex; i < arr.Length; i++)
            {
                if (arr[lowestValueIndex].PossibleValueList.Count > arr[i].PossibleValueList.Count)
                {
                    lowestValueIndex = i;
                }
            }
            return lowestValueIndex;
            
        }
        private static void Swap(Zone[] arr, int i, int j)
        {
            Zone temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        private void ArrayDebugger(Zone[] arr)
        {
            Debug.Log(arr);
            foreach (var element in arr)
            {
                Debug.Log(element.PossibleValueList.Count);
            }
        }
        
    }
}




