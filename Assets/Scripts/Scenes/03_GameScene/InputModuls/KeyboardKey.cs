using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace WasderGQ.Sudoku.Scenes.GameScene.InputModuls
{
    public class KeyboardKey : MonoBehaviour
    {
        [SerializeField] private int _myValue;
        [SerializeField] private SpriteRenderer _background;
        public Button Button { get; private set; }

        public int MyValue
        { 
            get => _myValue;
        }
        
        public async void DoClickAnimation()
        {
            _background.DOColor(Color.red, 0.5f);
            await Task.Delay(200);
            _background.DOColor(Color.white, 0.5f);
        }

        
    }
}
