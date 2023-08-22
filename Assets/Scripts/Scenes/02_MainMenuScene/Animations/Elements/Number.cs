using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;



namespace WasderGQ.Sudoku
{
    public class Number : MonoBehaviour
    {
        private Vector2 numbersStartPoint;
        private Vector2 numbersEndPoint;
        [SerializeField] private List<Color> _colorList ;
        [SerializeField] private bool StopAnimation;
        [SerializeField] private int _colorListCounter;
        [SerializeField] private TextMeshProUGUI _numberText;
        [SerializeField] private float _fadeValue;
        [SerializeField] private float _moveTime;
        
        
        public void SetStopAnimationForNumber(bool value)
        {
            StopAnimation = value;
        }
        
        public void Init(List<Color> colorList,Vector2 screenSize)
        {
            CalculateStartAndEndPosition(screenSize);
            SetVariable(colorList);
            SetStartPosition(SelectRandomPosition());
            SetRandomNumber();
            SetRandomFontSize();
            _fadeValue = CalculateFadeValue();
            StartCoroutine(StartTextColorAnimationForNumber());
            StartCoroutine(MoveToEnd());
            _moveTime = 20;
        }
        private void CalculateStartAndEndPosition(Vector2 screenSize)
        {
            numbersStartPoint = new Vector2(0, 1f);
            numbersEndPoint = new Vector2((screenSize.x - numbersStartPoint.x)  , (screenSize.y + numbersStartPoint.y) * -1f);
        }
        
        private void SetRandomNumber()
        {
            _numberText.text = WasderGQRandom._random.Next(1, 9).ToString();
        }

        private void SetRandomFontSize()
        {
            double range = 1.2d - 0.5d ;
            double sample = WasderGQRandom._random.NextDouble();
            double scaled = (sample * range) + 0.5d;
            _numberText.fontSize = (float) scaled;
        }

        private float CalculateFadeValue()
        {
            return (_numberText.fontSize - 0.5f) / 0.5f;
        }

        private Vector2 SelectRandomPosition()
        {
            return new Vector2(UnityEngine.Random.Range(numbersStartPoint.x, numbersEndPoint.x) ,numbersStartPoint.y);
        }

        
        private void SetStartPosition(Vector2 startPosition)
        {
            transform.localPosition = startPosition;
        }

        private void SetVariable(List<Color> colorList)
        {
            _colorList = colorList;
            
        }
        
        private IEnumerator StartTextColorAnimationForNumber()
        {
            while (!StopAnimation)
            {
                Color currentColor = new Color(_colorList[_colorListCounter].r, _colorList[_colorListCounter].g,
                    _colorList[_colorListCounter].b, _fadeValue);
                _numberText.DOColor(currentColor, 1f).SetEase(Ease.OutElastic);
                yield return new WaitForSeconds(1f);
                _colorListCounter++;
                if(_colorListCounter == _colorList.Count)
                {
                    _colorListCounter = 0;
                }
            }

            yield return null;
            
        }
        private IEnumerator MoveToEnd()
        {
            
            
            float speed = 0;
            while (!StopAnimation)
            {
                transform.DOLocalMoveY(numbersEndPoint.y,_moveTime);
                yield return new WaitForSeconds(_moveTime);
                SetStartPosition(SelectRandomPosition());
                SetRandomNumber();
                SetRandomFontSize();
                _fadeValue = CalculateFadeValue();
                
                /*
                speed = _moveSpeed * Time.deltaTime;
                Debug.LogWarning(speed);
                transform.localPosition = new Vector2(transform.localPosition.x,transform.localPosition.y - speed);
                yield return new WaitForSeconds(Time.deltaTime);
                if(transform.localPosition.y < endPosition)
                {
                    SetStartPosition(SelectRandomPosition());
                    SetRandomNumber();
                    SetRandomFontSize();
                    _fadeValue = CalculateFadeValue();
                }*/
            }

            yield return null;
        }
        
        
        
        
    }
}
