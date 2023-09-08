using System.Collections.Generic;
using UnityEngine;
using WasderGQ.Sudoku.Enums;
using WasderGQ.Sudoku.SceneManagement;
using WasderGQ.Sudoku.Scenes.GameScene.GameElement;
using WasderGQ.Sudoku.Scenes.GameScene.GameElement.Boards;
using WasderGQ.Sudoku.Scenes.GameScene.InputModuls;

namespace WasderGQ.Sudoku.InputsControllers
{
    public class PlayerInputController : MonoBehaviour
    {
        //[SerializeField]private LayerMask _gameLayer;
        //[SerializeField]private LayerMask _interactable;
        [SerializeField]private Keyboard _keyboard;
        [SerializeField]private Board _board;
        public static bool IsInputControllerActive { get; set; }
        private void Start()
        {
            IsInputControllerActive = true;
        }
        
        private void Update()
        {
            MouseClick();
        }

        private void ChangeHintBoolOnZones()
        {
            foreach (var parsel in _board.Parsels)
            {
                foreach (var zone in parsel.ZonesInParsel)
                {
                    if (zone.IsHint)
                    {
                        zone.ChangeHintSetting(false);
                    }
                    else
                    {
                        zone.ChangeHintSetting(true);
                    }
                }
            }
        }
        
        
        private void MouseClick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && IsInputControllerActive)
            {
                Vector3 position = TakeMousePosition();
                RayThrowTakeRaycastHit(out bool isCatch,out RaycastHit raycastHit,position);
                if(isCatch)
                Interact(raycastHit);
            }
        }

        private void RayThrowTakeRaycastHit(out bool iscatch,out RaycastHit raycastHit,Vector3 postion)
        {
            Ray ray = new Ray(postion, Vector3.forward);
            List<RaycastHit> raycastHitList = new List<RaycastHit>(Physics.RaycastAll(ray, float.MaxValue));
            //raycasting not working on UI elements .Changed this line cod .Early it was : Physics.RaycastAll(ray, float.MaxValue, _interactable);.
            // and raycaster select object on that layer (old version).
            if (raycastHitList.Count == 0)
            {
                iscatch = false;
                raycastHit = new RaycastHit();
                return;
            }
            iscatch = true;
            raycastHit = raycastHitList[0];
        }

        private Vector3 TakeMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void Interact(RaycastHit raycastHit)
        {
            if (raycastHit.collider.CompareTag("Zone"))
            {
                 
                try
                {
                    Zone zone = raycastHit.collider.GetComponentInParent<Zone>();
                    if (zone._isSelectable)
                    {
                        if (zone.Selected)
                        {
                            zone.DoUnSelected(zone.MyValue);
                            _keyboard.RemoveZoneToList(zone);
                        }
                        else
                        {
                            zone.DoSelected();
                            _keyboard.SaveZoneToList(zone);
                        } 
                    }
                    
                }
                catch 
                {
                    Debug.LogError("There is no !! MB_Zone !! script on the reached GameObject.");
                }

                
                
            }

            else if (raycastHit.collider.CompareTag("KeyboardKey"))
            {
                try
                {
                    KeyboardKey key = raycastHit.collider.GetComponent<KeyboardKey>();
                    key.DoClickAnimation();
                    _keyboard.GiveValueToZone(key);
                }
                catch 
                {
                    Debug.LogError("There is no !! KeyboardKey !! script on the reached GameObject.");
                }
            }
            else if(raycastHit.collider.CompareTag("HintButton"))
            {
                ChangeHintBoolOnZones();
            }
            else if (raycastHit.collider.CompareTag("GoBackToMenu"))
            {
                PopUpMessage popUpMessage = PopUpController.CreatePopUpMessage();
                IsInputControllerActive = false;
                popUpMessage.SetYesNoPopUpMessage("Are you sure you want to go back to the main menu?", () => { SceneLoader.Instance.WLoadScene(EnumScenes.MainMenuScene);},()=> IsInputControllerActive = true);
            }
            else if(raycastHit.collider.CompareTag("RefreshBoard"))
            {
                PopUpMessage popUpMessage = PopUpController.CreatePopUpMessage();
                IsInputControllerActive = false;
                popUpMessage.SetYesNoPopUpMessage("Are you sure you want to recreate the board ?", () => { SceneLoader.Instance.WLoadScene(EnumScenes.GameSceneSudoku);},()=> IsInputControllerActive = true);
            }
            else
            {
                Debug.LogWarning("No Tag on GameObject");
            }
            

        }
        
    }
}
