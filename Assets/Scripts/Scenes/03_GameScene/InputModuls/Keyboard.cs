using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WasderGQ.Sudoku.InputsControllers;
using WasderGQ.Sudoku.Scenes.GameScene.GameElement;
using WasderGQ.Sudoku.Scenes.GameScene.GameElement.Boards;

namespace WasderGQ.Sudoku.Scenes.GameScene.InputModuls
{
    public class Keyboard : MonoBehaviour 
    {
        
        [SerializeField] private List<Zone> _selectedZones;
        [SerializeField] private Boardx9 _boardx9;
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
            if (_selectedZones.Count > 0 && key.MyValue <= 9)
            {
                foreach (var zone in _selectedZones)
                {
                    zone.WriteValue(key.MyValue);
                }
                _selectedZones.Clear();
                _sudoku.CheckWin();
            }
            else if(key.MyValue == 10)
            {
                PopUpMessage popUpMessage = PopUpController.CreatePopUpMessage();
                if(popUpMessage != null)
                {
                    PlayerInputController.IsInputControllerActive = false;
                    popUpMessage.SetYesNoPopUpMessage("Are u sure u want to clear all zones?", Yes,No);
                    void No()
                    {
                        PlayerInputController.IsInputControllerActive = true;
                    }

                    void Yes()
                    {
                        _selectedZones.Clear();
                        _boardx9.ClearAllZone();
                        PlayerInputController.IsInputControllerActive = true;
                    }
                }
                else
                {
                    Debug.Log("No Zone Selected");
                }
            }
            else
            {
                Debug.Log("No Zone Selected");
            }
        }
        
    }
}

