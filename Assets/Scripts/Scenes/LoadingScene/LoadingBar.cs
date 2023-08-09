using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace WasderGQ.Sudoku
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] Slider _loadingBar;
        [SerializeField] float _duration = 5f;
        

        public void UpdateLoadStatus(float targetValue)
        {
           StartCoroutine(IncreaseSliderValue(targetValue));
        }
        
        
        private IEnumerator IncreaseSliderValue(float targetValue)
        {
            float startValue = _loadingBar.value;
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
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
