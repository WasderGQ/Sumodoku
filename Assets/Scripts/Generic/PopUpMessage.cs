using System;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WasderGQ.Sudoku.Generic;

namespace WasderGQ.Sudoku
{
    public class PopUpMessage : Singleton<PopUpMessage>
    {
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private GameObject _assets;
        [SerializeField] private TextMeshProUGUI _messageText;  //TODO: Change this to you used text component
        private Action _okeyButtonAction;
        private Action _yesButtonAction;
        private Action _noButtonAction;
        

        public void SetOkeyPopUpMessage(string message, [CanBeNull] Action clickOk)
        {
            PopUpEnable(true);
            SetOkButtonAction(true);
            _messageText.text = message;
            _okeyButtonAction += OKButtonClicked;
            void OKButtonClicked()
            {
                SetOkButtonAction(false);
                clickOk?.Invoke();
                Debug.Log("OK Button Clicked");
                CloseOkeyPopUpMessage();
            }
            
        }
        
        public void SetYesNoPopUpMessage(string message, [CanBeNull] Action clickYes,[CanBeNull] Action clickNo)
        {
            PopUpEnable(true);
            _messageText.text = message;
            SetYesNoButtonAction(true);
            _yesButtonAction += YesButtonClicked;
            _noButtonAction += NoButtonClicked;
            void YesButtonClicked()
            {
                SetYesNoButtonAction(false);
                clickYes?.Invoke();
                Debug.Log("Yes Button Clicked");
                CloseYesNoPopUpMessage();
                
            }
            void NoButtonClicked()
            {
                SetYesNoButtonAction(false);
                clickNo?.Invoke();
                Debug.Log("No Button Clicked");
                CloseYesNoPopUpMessage();
            }
        }
        
        private void PopUpEnable(bool state)
        {
            if (state)
            {
                _assets.gameObject.SetActive(true);
            }
            else
            {
                _assets.gameObject.SetActive(false);
            }
        }
        private void SetOkButtonAction(bool state)
        {
            if (!state)
            {
                _okButton.onClick.RemoveAllListeners();
            }
            _okButton.gameObject.SetActive(state);
            if (state)
            {
                _okButton.onClick.AddListener(InvokeOkeyAction);
            }
                
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
                _yesButton.onClick.AddListener(InvokeYesAction);
                _noButton.onClick.AddListener(InvokeNoAction);
            }
                
        }
        private void CloseYesNoPopUpMessage()
        {
            _yesButtonAction = null;
            _noButtonAction = null;
            _noButton.gameObject.SetActive(false);
            _yesButton.gameObject.SetActive(false);
            PopUpEnable(false);
            
        }
        private void CloseOkeyPopUpMessage()
        {
            _okeyButtonAction = null;
            _okButton.gameObject.SetActive(false);
            PopUpEnable(false);
        }

        private void InvokeOkeyAction()
        {
            _okeyButtonAction?.Invoke();
        }
        private void InvokeYesAction()
        {
            _yesButtonAction?.Invoke();
        }
        private void InvokeNoAction()
        {
            _noButtonAction?.Invoke();
        }
    }
}
