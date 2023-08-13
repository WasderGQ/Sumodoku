using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace WasderGQ.Sudoku
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] private Slider _loadingBar;
        [SerializeField] private float _duration;
        [SerializeField] private float _increaseAmount = 10f;
        [SerializeField] public bool _stopAnimation;

        private void Start()
        {
            _duration = 2f;
        }

        public async Task<bool> UpdateLoadStatus(float targetValue,CancellationTokenSource cancellationTokenSource)
        {
            bool taskBool = await TaskIncreaseSliderValue(targetValue,cancellationTokenSource);
            return taskBool;
        }
        
        
        private async Task<bool> TaskIncreaseSliderValue (float targetValue,CancellationTokenSource cancellationTokenSource)
        {
            float startValue = _loadingBar.value;
            float elapsedTime = 0f;
            while (elapsedTime < _duration )
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / _duration);
                _loadingBar.value = Mathf.Lerp(startValue, targetValue, t);
                await Task.Delay(Convert.ToInt32(Time.deltaTime*1000));
                if (_stopAnimation)
                {
                    return false;
                }
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    _loadingBar.value = targetValue;
                    return false;
                }
            }
            _loadingBar.value = targetValue;
            while(!cancellationTokenSource.IsCancellationRequested)
            {
                await Task.Delay(200);
            }
            return true;
        }
        
        
        private IEnumerator CoroutineIncreaseSliderValue(float targetValue)
        {
            float startValue = _loadingBar.value;
            float elapsedTime = 0f;
            while (elapsedTime < _duration && !_stopAnimation)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / _duration);
                _loadingBar.value = Mathf.Lerp(startValue, targetValue, t);
                yield return null;
                
            }
            _loadingBar.value = targetValue;
        }
        
        
        
        
        
        
        
      
        
        
    }
}
