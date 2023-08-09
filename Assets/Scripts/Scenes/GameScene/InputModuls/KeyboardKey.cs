using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WasderGQ.ThirdPartyUtility.DOTween.Modules;

namespace WasderGQ.Sudoku.Scenes.GameScene.InputModuls
{
    public class KeyboardKey : MonoBehaviour
    {
        [SerializeField] private int _myValue;
        [SerializeField] private SpriteRenderer _filter;
        public Button Button { get; private set; }

        public int MyValue
        {
            get => _myValue;
        }

        public void Init()
        {
            SetButton();
        }

        private void SetButton()
        {
            try
            {
                Button = GetComponent<Button>();
            }
            catch (Exception e)
            {
                Debug.LogError($"This keyboard button doesnt have button component!!! {e.Message}");

            }
        }

        public async void DoClickAnimation()
        {
            _filter.DOFade(1f, 0.5f);
            await Task.Delay(200);
            _filter.DOFade(0f, 0.5f);
        }
    }
}
