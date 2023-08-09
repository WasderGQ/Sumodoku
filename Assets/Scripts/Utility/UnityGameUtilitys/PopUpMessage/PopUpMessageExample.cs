using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace WasderGQ.Sudoku
{
    public class PopUpMessage : MonoBehaviour // TODO: Singleton<PopUpMessage> this have to be singleton !!!!
    {
        [SerializeField] Button _oKButton;
        [SerializeField] Button _yesButton;
        [SerializeField] Button _noButton;
        [SerializeField] Text _messageText; //TODO: Change this to you used text component
        private UnityAction _okeyButtonAction;
        private UnityAction _yesButtonAction;
        private UnityAction _noButtonAction;
        
        private void OnEnable()
        {
            _oKButton.onClick.AddListener(_okeyButtonAction);
            _yesButton.onClick.AddListener(_yesButtonAction);
            _noButton.onClick.AddListener(_noButtonAction);

        }
        private void OnDisable()
        {
            _oKButton.onClick.RemoveListener(_okeyButtonAction);
            _yesButton.onClick.RemoveListener(_yesButtonAction);
            _noButton.onClick.RemoveListener(_noButtonAction);
        }

        public void SetOkeyPopUpMessage(string message, [CanBeNull] UnityAction clickOk)
        {
            _messageText.text = message;
            gameObject.SetActive(true);
            void OKButtonClicked()
            {
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
            _messageText.text = message;
            gameObject.SetActive(true);
            _noButton.gameObject.SetActive(true);
            _yesButton.gameObject.SetActive(true);
            void YesButtonClicked()
            {
                if (clickYes != null)
                {
                    clickYes.Invoke();
                }
                Debug.Log("Yes Button Clicked");
                CloseYesNoPopUpMessage();
                
            }
            void NoButtonClicked()
            {
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
            gameObject.SetActive(false);
        }
        private void CloseOkeyPopUpMessage()
        {
            _okeyButtonAction = null;
            _oKButton.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        
        
        
    }
}
