using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WasderGQ.Sudoku.Scenes.GameScene.Game;
using WasderGQ.Sudoku.Scenes.GameScene.Game.Boards;

namespace WasderGQ.Sudoku.Scenes.GameScene.InputModuls
{
    public class Keyboard : MonoBehaviour 
    {
        
        [SerializeField] private List<Zone> _selectedZones;
        [SerializeField] private Boardx9 boardx9;
        [SerializeField] private Sudoku _sudoku;


        public void init()
        {
       

        }
    
        public void SaveZoneToList(Zone zone)
        {
            _selectedZones.Add(zone);
        }
        public void RemoveZoneToList(Zone zone)
        {
            _selectedZones.Remove(zone);
        }

        public void GiveValueToZone(KeyboardKey key)
        {
            if (_selectedZones.Count > 0)
            {
                foreach (var zone in _selectedZones)
                {
                    zone.WriteValue(key.MyValue);
                }
                _selectedZones.Clear();
                _sudoku.CheckWin();
            }
       
       
        
        
        }
    }
}

