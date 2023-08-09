using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using WasderGQ.ThirdPartyUtility.DOTween.Modules;
using WasderGQ.Utility.UnityEditor;

namespace WasderGQ.Sudoku.Scenes.GameScene.Game
{
    public class Zone : MonoBehaviour
    {
        
        [SerializeField] private SpriteRenderer _myImage;
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private Tween _currentAnimation;
        
        [field: SerializeField,DisableValueChange] public int TrueValue { get; private set; }
        [field: SerializeField, DisableValueChange] public int[] ZoneID { get; private set; }
        
        [field: SerializeField, DisableValueChange] public int[] ParselID { get; private set; }

        [field: SerializeField, DisableValueChange] public int MyValue { get; private set; }

        [field: SerializeField, DisableValueChange] public bool IsHint { get; private set; }

        [field: SerializeField, DisableValueChange] public List<int> PossibleValueList { get; private set; }

        [field: SerializeField, DisableValueChange] public bool Selected { get; private set; }
        public int ComparableValue
        {
            get => PossibleValueList.Count;
        }

        private int _setMyValue //when value change trigger RefreshText func. #Property
        {
            get { return MyValue; }
            set
            {
                if (MyValue == value) return;
                if (MyValue != value)
                {
                    if (LayerMask.NameToLayer("Interactable") == this.gameObject.layer)
                    {
                        RefreshText(value);
                        MyValue = value; 
                    }
                }

            }
        }

        private bool _hint
        {
            get { return IsHint; }
            set
            {
                if (IsHint == value) return;
                if (IsHint != value)
                {
                    if (value == false && Selected == false)
                    {
                        DoToDefaultZoneAnimation();
                    }
                    else
                    {
                        if(MyValue != 0)
                        ShowTrueFalseZones(MyValue);
                    }
                    IsHint = value;
                }

            }
        }

        public void init()
        {
            SetDefaultVariableOnStart();
            SetPossiblyValues();
        }

        public void ChangeHintSetting(bool onOff)
        {
            _hint = onOff;
        }

        private void ShowTrueFalseZones(int value)
        {
            if (Selected == false)
            {
                if (TrueValue == value )
                {
                    Debug.Log("True triggered");
                    DoTrueAnimation();
                }
                else
                {
                    Debug.Log("False triggered");
                    DoFalseAnimation();
                }
            }
        }

        private void SetDefaultVariableOnStart()
        {
            ZoneID = new int[2];
            ParselID = new int[2];
            SetMyValueDefault();
            SetIsHintDefault();
            SetLayer(LayerMask.NameToLayer("Interactable"));
            
        }

        private void SetLayer(int layer)
        {
            this.gameObject.layer = layer;

        }
        public void SetLayer(LayerMask mask)
        {
            this.gameObject.layer = mask;

        } 
        private void DoTrueAnimation()
        {
            _currentAnimation.Kill();
            _currentAnimation = _myImage.DOColor(Color.green, 1f);
        }

        private void DoFalseAnimation()
        {
            _currentAnimation.Kill();
            _currentAnimation = _myImage.DOColor(Color.red, 1f);
        }

        public void DoSelected()
        {
            Selected = true;
            DoClickAnimation();
        }

        public void DoUnSelected(int value)
        {
            Selected = false;
            if (IsHint && value != 0)
            {
                if (value == TrueValue)
                {
                    DoTrueAnimation();
                }
                else
                {
                    DoFalseAnimation();
                }
            }
            else
            {
                DoToDefaultZoneAnimation();
            }
        }
        
        public void SetMyValueDefault()
        {
            WriteValue(0);
        }

        private void SetIsHintDefault()
        {
            IsHint = false;
        }

        public void SetTrueValue(int value)
        {
            TrueValue = value;
        }

        public void SetPossiblyValues()
        {
            PossibleValueList = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9
            };
        }

        public void RemoveValueFromPossibleValues(Zone zone)
        {
            PossibleValueList.Remove(zone.MyValue);
        }

        public void RemoveValueFromPossibleValues(int value)
        {
            PossibleValueList.Remove(value);
        }

        public int GetValueOnPossibleValueList(int valueIndex)
        {
            return PossibleValueList[valueIndex];
        }

        public void DoClickAnimation()
        {
            _currentAnimation.Kill();
            _currentAnimation = _myImage.DOColor(Color.yellow, 1f);
        }

        public void DoToDefaultZoneAnimation()
        {
            _currentAnimation.Kill();
            _currentAnimation = _myImage.DOColor(Color.white, 1f);
        }
        
        public void WriteValue(int givenvalue)
        {
            _setMyValue = givenvalue;
            DoUnSelected(givenvalue);
        }

        public void SetParselID(int[] parselID)
        {
            ParselID = parselID;
        }

        public void SetZoneID(int[] zoneID)
        {
            ZoneID = zoneID;
        }

        private void RefreshText(int value)
        {
            if (value != 0)
            {
                _text.text = value.ToString();
            }
            else
            {
                _text.text = " ";
            }
        }


    }
}

