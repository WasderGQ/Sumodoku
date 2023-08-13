using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace WasderGQ.Sudoku
{
    public class FrontObjectAnimationController : MonoBehaviour
    {
        [SerializeField] List<Color> _animationColorList = new List<Color>();
        [SerializeField] public bool StopAnimation = false;
        [SerializeField] private TextMeshProUGUI _mainText;
        [SerializeField] private Material _textIconMaterial;
        [SerializeField] private Material _iconAlertMaterial;
        [SerializeField] private List<Image> _iconAlert;
        [SerializeField] private Transform _iconAll;
        private int _colorListCounter = 0;
        
        public void Init(List<Color> colorList)
        {
            _animationColorList = colorList;
            StartAnimations();
        }
        
        


        
        private void StartAnimations()
        {
            StartCoroutine(ShackTextAnimation());
            StartCoroutine(TextColorAnimation());
            StartCoroutine(RotateTextIconAnimation());

        }

        
        
        private IEnumerator ShackTextAnimation()
        {
            while (!StopAnimation)
            {
                _mainText.transform.DOShakePosition(1f, 10f, 10, 90f, false, true).SetEase(Ease.InOutBounce);
                yield return new WaitForSeconds(1.0f);
            }
            yield return null;
        }

        private IEnumerator RotateTextIconAnimation()
        {
            while (!StopAnimation)
            {
                IconAlertFadeAnimation(true);
                _iconAll.transform.DORotate(new Vector3(0, 0, 15f), 0.5f,RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine);
                yield return new WaitForSeconds(0.5f);
                IconAlertFadeAnimation(false);
                _iconAll.transform.DORotate(new Vector3(0, 0, -15f), 0.5f,RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine);
                yield return new WaitForSeconds(0.5f);
                IconAlertFadeAnimation(true);
                _iconAll.transform.DORotate(new Vector3(0, 0, -15f), 0.5f,RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine);
                yield return new WaitForSeconds(0.5f);
                IconAlertFadeAnimation(false);
                _iconAll.transform.DORotate(new Vector3(0, 0, 15f), 0.5f,RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine);
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
        private void IconAlertFadeAnimation(bool Status)
        {
            if (Status)
            {
                _iconAlert[0].DOFade(1f, 0.5f).SetEase(Ease.InOutSine);
                _iconAlert[1].DOFade(1f, 0.5f).SetEase(Ease.InOutSine);
            }
            else
            {
                _iconAlert[0].DOFade(0f, 0.5f).SetEase(Ease.InOutSine);
                _iconAlert[1].DOFade(0f, 0.5f).SetEase(Ease.InOutSine);
            }
           
        }
        
        
        
        private IEnumerator TextColorAnimation()
        {
            while (!StopAnimation)
            {
                Color reversToColor = ReversToColor(_animationColorList[_colorListCounter]);
                Color HDRMainTextColor = AddHDRIntensityToColor(reversToColor, 1.65f);
                Color HDRIconColor = AddHDRIntensityToColor(reversToColor, 4f);
                _mainText.DOColor(HDRMainTextColor, 2f).SetEase(Ease.InOutSine);
                _textIconMaterial.DOColor(HDRIconColor,"_Sumo_Color", 2f).SetEase(Ease.InOutSine);
                _iconAlertMaterial.DOColor(HDRIconColor,"_AlertColor", 2f).SetEase(Ease.InOutSine);
                yield return new WaitForSeconds(2.0f);
                _colorListCounter++;
                if(_colorListCounter == _animationColorList.Count)
                {
                    _colorListCounter = 0;
                }
            }
            yield return null;
        }

            // Function to find the complementary color for a given color
            private Color ReversToColor(Color color)
            {
                float complementaryRed = 1f - color.r;
                float complementaryGreen = 1f - color.g;
                float complementaryBlue = 1f - color.b;

                // Create and return the new complementary color
                Color complementaryColor = new Color(complementaryRed, complementaryGreen, complementaryBlue);
                return complementaryColor;
            }
            private Color RevertToColorList(List<Color> colorList)
            {
                int revertedColorCounter = colorList.Count -_colorListCounter ;
                return colorList[revertedColorCounter];
            }
            
            public void CloseAnimations()
            {
                _colorListCounter = 0;
                StopAnimation = true;
            }
            private Color AddHDRIntensityToColor(Color color,float HDRintensity)
            {
                return color * HDRintensity ;
            }
            
        
    }
}
