using System;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WasderGQ.Sudoku.Generic;

namespace WasderGQ.Sudoku
{
    public class PopUpMessage : Singleton<PopUpMessage>
    {
        [SerializeField] private Button _oKButton;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private GameObject _assets;
        [SerializeField] private TextMeshProUGUI _messageText;  //TODO: Change this to you used text component
        private UnityAction _okeyButtonAction;
        private UnityAction _yesButtonAction;
        private UnityAction _noButtonAction;

        
        private void OnAssetEnable()
        {
            _assets.gameObject.SetActive(true);
        }
        private void OnAssetDisable()
        {
            _assets.gameObject.SetActive(false);
        }

        private void SetOkButtonAction(bool state)
        {
            if (!state)
                _oKButton.onClick.RemoveAllListeners();

            _oKButton.gameObject.SetActive(state);

            if (state)
                _oKButton.onClick.AddListener(_okeyButtonAction);
        }
        private void SetYesNoButtonAction(bool state)
        {
            if (!state)
            {
                _yesButton.onClick.RemoveAllListeners();
                _noButton.onClick.RemoveAllListeners();
            }
            _yesButton.gameObject.SetActive(state);
            _noButton.gameObject.SetActive(state);
            if (state)
            {
                _yesButton.onClick.AddListener(_okeyButtonAction);
                _noButton.onClick.AddListener(_okeyButtonAction);
            }
                
        }


        public void SetOkeyPopUpMessage(string message, [CanBeNull] UnityAction clickOk)
        {
            OnAssetEnable();
            SetOkButtonAction(true);
            _messageText.text = message;
            
            void OKButtonClicked()
            {
                SetOkButtonAction(false);
                if (clickOk != null)
                {
                    clickOk.Invoke();
                }
                Debug.Log("OK Button Clicked");
                CloseOkeyPopUpMessage();
            }
            _okeyButtonAction = OKButtonClicked;
        }
        
        public void SetYesNoPopUpMessage(string message, [CanBeNull] UnityAction clickYes,[CanBeNull] UnityAction clickNo)
        {
            OnAssetEnable();
            _messageText.text = message;
            SetYesNoButtonAction(true);
            void YesButtonClicked()
            {
                SetYesNoButtonAction(false);
                if (clickYes != null)
                {
                    clickYes.Invoke();
                }
                Debug.Log("Yes Button Clicked");
                CloseYesNoPopUpMessage();
                
            }
            void NoButtonClicked()
            {
                SetYesNoButtonAction(false);
                if (clickNo != null)
                {
                    clickNo.Invoke();
                }
                Debug.Log("No Button Clicked");
                CloseYesNoPopUpMessage();
            }
            _yesButtonAction = YesButtonClicked;
        }
        
        
        private void CloseYesNoPopUpMessage()
        {
            _yesButtonAction = null;
            _noButtonAction = null;
            _noButton.gameObject.SetActive(false);
            _yesButton.gameObject.SetActive(false);
            OnAssetDisable();
            
        }
        private void CloseOkeyPopUpMessage()
        {
            _okeyButtonAction = null;
            _oKButton.gameObject.SetActive(false);
            OnAssetDisable();
        }
        
        
        
    }
}
