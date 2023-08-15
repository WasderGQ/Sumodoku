using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WasderGQ.Sudoku.Generic;

namespace WasderGQ.Sudoku
{
    public class PopUpMessage : MonoBehaviour
    {
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private GameObject _popUpPanel;
        [SerializeField] private TextMeshProUGUI _messageText;  //TODO: Change this to you used text component
        private event Action _okeyButtonAction;
        private event Action _yesButtonAction;
        private event Action _noButtonAction;
        

        public void SetOkeyPopUpMessage(string message, [CanBeNull] Action clickOk)
        {
            ShowPopUp(true);
            SetUnityActionOkButton(true);
            _messageText.text = message;
            _okeyButtonAction += OKButtonClicked;
            void OKButtonClicked()
            {
                SetUnityActionOkButton(false);
                clickOk?.Invoke();
                Debug.Log("OK Button Clicked");
                CloseOkeyPopUpMessage();
            }
            
        }
        
        public void SetYesNoPopUpMessage(string message, [CanBeNull] Action clickYes,[CanBeNull] Action clickNo)
        {
            ShowPopUp(true);
            _messageText.text = message;
            SetUnityActionForYesNoButton(true);
            _yesButtonAction += YesButtonClicked;
            _noButtonAction += NoButtonClicked;
            void YesButtonClicked()
            {
                SetUnityActionForYesNoButton(false);
                clickYes?.Invoke();
                Debug.Log("Yes Button Clicked");
                CloseYesNoPopUpMessage();
            }
            void NoButtonClicked()
            {
                SetUnityActionForYesNoButton(false);
                clickNo?.Invoke();
                Debug.Log("No Button Clicked");
                CloseYesNoPopUpMessage();
            }
        }
        
        private void ShowPopUp(bool state)
        {
            if (state)
            {
                _popUpPanel.gameObject.SetActive(true);
            }
            else
            {
                _popUpPanel.gameObject.SetActive(false);
            }
        }
        private void SetUnityActionOkButton(bool state)
        {
            _okButton.gameObject.SetActive(state);
            if (state)
            {
                _okButton.onClick.AddListener(InvokeOkeyAction);
            }
            else
            {
                _okButton.onClick.RemoveAllListeners();
            }
            
                
        }
        private void SetUnityActionForYesNoButton(bool state)
        {
            _yesButton.gameObject.SetActive(state);
            _noButton.gameObject.SetActive(state);
            if (state)
            {
                _yesButton.onClick.AddListener(InvokeYesAction);
                _noButton.onClick.AddListener(InvokeNoAction);
            }
            else
            {
                _yesButton.onClick.RemoveAllListeners();
                _noButton.onClick.RemoveAllListeners();
            }
            
                
        }
        private void CloseYesNoPopUpMessage()
        {
            _yesButtonAction = null;
            _noButtonAction = null;
            _noButton.gameObject.SetActive(false);
            _yesButton.gameObject.SetActive(false);
            ShowPopUp(false);
            Destroy(this.gameObject);
            
        }
        private void CloseOkeyPopUpMessage()
        {
            _okeyButtonAction = null;
            _okButton.gameObject.SetActive(false);
            ShowPopUp(false);
            Destroy(this.gameObject);
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
