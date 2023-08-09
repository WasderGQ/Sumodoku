using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using WasderGQ.ThirdPartyUtility.DOTween.Modules;
using Random = UnityEngine.Random;

namespace WasderGQ.Sudoku
{
    public class BackGroundAnimation : MonoBehaviour
    {
        
        private readonly int _amount = 40;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private GameObject _numberPrefab;
        [SerializeField] private Transform _numberParent;
        [SerializeField] private List<Color> _colorList;
        [SerializeField] private List<Number> _numberList;
        [SerializeField] private RectTransform _canvasRectTransform;
        public bool _stopAnimation;
        private int _colorListCounter = 0;
        
        
        
        public void Init(List<Color> colorList)
        {
            _colorList = 
            _colorList = colorList;
            StartAnimations();
        }
        
        private void StartAnimations()
        {
            StartCoroutine(CreateNumberList());     
        }
        
        
        
        private IEnumerator CreateNumberList() // creating on the scene
        {
            int amountCounter = 0;
            while(amountCounter < _amount)
            {
                Number tempNumber = CreateNumber(amountCounter);
                amountCounter++;
                if(tempNumber == null)
                {
                    Debug.Log("Created Number is null because of that animation won't work");
                    yield return null;
                }
                else 
                {
                    _numberList.Add(tempNumber);
                    tempNumber.Init(_colorList,_canvasRectTransform.sizeDelta);  
                }
                float waitTime = RandomWaitTime();
                yield return new WaitForSeconds(waitTime);
                
            }
            yield return null;
        }
    
        private float RandomWaitTime()
        {
            return Random.Range(0.7f, 3);
        }
        
        [CanBeNull] private Number CreateNumber(int amountCounter)
        {
            if(amountCounter < _amount)
            {
                GameObject instantsGameObject = Instantiate(_numberPrefab,_numberParent);
                try
                {
                    return instantsGameObject.GetComponent<Number>();
                }
                catch (Exception e)
                {
                    Debug.Log("Prefab Object not include Number Component : " + e.Message);
                    return null;
                }
            }
            else
            {
                Debug.Log("Number List is full");
                return null;
            }
        }
        
        
       

        
        
        
        
        
        
        
        
        
        
        
    }
}
