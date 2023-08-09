using System;
using System.Collections.Generic;
using UnityEngine;
using WasderGQ.Sudoku.AIs;
using WasderGQ.Utility.List_Array_Etc;
using Random = UnityEngine.Random;

namespace WasderGQ.Sudoku.Scenes.GameScene.Game.Boards
{
    public abstract class Board : MonoBehaviour
    {
        protected SolvedSudokuCreater SolvedSudokuCreater;
        protected Zone[,] _zones;
        [SerializeField] protected Parsel[] _parsels;
        [SerializeField] protected List<Zone> _sealedZones;
        [SerializeField] protected LayerMask UnInteractable;
        public Parsel[] Parsels { get => _parsels; }
        public Zone[,] Zones { get => _zones; }
        
        public virtual void InIt()
        {
            
        }

        protected virtual void ParselsInIt()
        {
            foreach (var parsel in _parsels)
            {
                parsel.init();
            } 
        }
       
        protected virtual void AddMapCreater()
        {
            try 
            {
                SolvedSudokuCreater = new SolvedSudokuCreater(Parsels,Zones);
            }
        
            catch
            {
                Debug.Log("Map Creater can't create on Boardx9 class !!!!");
            }
        
        }
        
        protected virtual async void StartMapCreater()
        {
            bool done = await SolvedSudokuCreater.Start();
        }

        protected virtual void ConvertParselZonesToZones()
        {
            
        }
        
        protected virtual void SetZonesID()
        {
            foreach (var zone in _zones)
            {
                int[] indexs = _zones.FindIndex(zone); 
                zone.SetZoneID(indexs);
            }
        }
        
        protected virtual void MakeZonesDefault()
        {
            foreach (var parsel in _parsels)
            {
                foreach (var zone in parsel.ZonesInParsel)
                {
                    zone.SetTrueValue(zone.MyValue);
                    zone.SetMyValueDefault();
                }
            }
        }

        protected virtual void SelectZoneFromBoard(int startValue ,int amount)
        {
            int counter = startValue;
            while (counter < amount)
            {
                Zone zone = TakeRandomZone();
                if (IsThereSameValue(zone))
                {
                    SelectZoneFromBoard(counter, amount);
                    break;
                }
                zone.WriteValue(zone.TrueValue);
                zone.SetLayer(UnInteractable);
                _sealedZones.Add(zone);
                counter++;
            }
        }

        private Zone TakeRandomZone()
        {
            int xValueIndex = Convert.ToInt32(Mathf.Floor(Random.Range(0 , _zones.GetLength(0) - 1)));
            int yValueIndex = Convert.ToInt32(Mathf.Floor(Random.Range(0 , _zones.GetLength(1) - 1)));
            return _zones[xValueIndex, yValueIndex];
        }
        
        private bool IsThereSameValue(Zone selectedZone)
        {
            foreach (var zone in _sealedZones)
            {
                if (zone == selectedZone)
                {
                    return true;
                }
            }
            return false;
        }

       
    }
}
