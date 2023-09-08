using System;
using System.Collections.Generic;
using UnityEngine;
using WasderGQ.Sudoku.AIs;
using WasderGQ.Sudoku.Scenes.MainMenuScene;
using WasderGQ.Utility.List_Array_Etc;
using Random = UnityEngine.Random;

namespace WasderGQ.Sudoku.Scenes.GameScene.GameElement.Boards
{
    public abstract class Board : MonoBehaviour
    {
        protected SolvedSudokuCreator SolvedSudokuCreator;
        protected Zone[,] _zones;
        [SerializeField] protected Parsel[] _parsels;
        [SerializeField] protected List<Zone> _sealedZones;

        [field: SerializeField] protected Color _knowedZoneTextColor { get; set; }
        //[SerializeField] protected LayerMask UnInteractable;
        public Parsel[] Parsels { get => _parsels; }
        public Zone[,] Zones { get => _zones; }
        
        public virtual void InIt(SO_GameMode gameMode) //this function structure varies in inherited clusters
        {
            
        }
        
        
        protected virtual void ParselsInIt()
        {
            foreach (var parsel in _parsels)
            {
                parsel.init();
            } 
        }
        
        
        protected virtual  void StartMapCreater()
        {
                SolvedSudokuCreator = new SolvedSudokuCreator(Parsels,Zones);
                SolvedSudokuCreator.Start();
        }

        protected virtual void ConvertParselZonesToZones()  //conver to 2d array and make this for every each board (this func overrideable)
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

        protected virtual void SelectZoneFromBoard(int amount)
        {
            int counter = 0;
            while (counter < amount)
            {
                Zone zone;
                do
                {
                    zone = TakeRandomZone();
                } while (IsThereSameValue(zone));
                zone.WriteValue(zone.TrueValue);
                zone.ChangeTextColor(_knowedZoneTextColor);
                zone.SetInterecable(false);
                zone._isSelectable = false;  //**no necessary because of that zone in the beginning is not selectable**
                //zone.SetLayer(UnInteractable); In UI not working
                _sealedZones.Add(zone);
                counter++;
            }
        }

        protected Zone TakeRandomZone()
        {
            int xValueIndex = Convert.ToInt32(Mathf.Floor(Random.Range(0 , _zones.GetLength(0) )));
            int yValueIndex = Convert.ToInt32(Mathf.Floor(Random.Range(0 , _zones.GetLength(1) )));
            return _zones[xValueIndex, yValueIndex];
        }
        
        protected bool IsThereSameValue(Zone selectedZone)
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

        public virtual void ClearAllZone()
        {
            foreach (var parsel in _parsels)
            {
                foreach (var zone in parsel.ZonesInParsel)
                {
                    if(zone.IsInteractable)
                        zone.SetMyValueDefault();
                }
            }
        }
        protected virtual void SetAllZoneInteractable()
        {
            foreach (var parsel in _parsels)
            {
                foreach (var zone in parsel.ZonesInParsel)
                {
                    if (!_sealedZones.Contains(zone))
                    {
                        Debug.Log(zone.ZoneID + " : " + _sealedZones.Contains(zone));
                        zone._isSelectable = true;
                    }
                }
            }
        }
        
        protected  virtual int CalculateAmountOfSealedZones(SO_GameMode gameMode)
        {
            return 14;
        }
    }
}
