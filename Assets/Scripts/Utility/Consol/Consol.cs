using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WasderGQ.Utility.Consol;
using WasderGQ.Utility.Singleton;

public class Consol : Singleton<Consol>
{
    [SerializeField] private bool _isConsolOpen;
    [SerializeField] private bool _isFaiding;
    [SerializeField] private Button _consolOpenButton;
    [SerializeField] private Image _consolOpenButtonImage;
    [SerializeField] private Button _consolStopButton;
    [SerializeField] private ConsolInformation _consolPanel;
    [SerializeField] private float fadeDuration = 0.5f; // Solma süresi (saniye)

    private void Start()
    {
            AddEvent();
            StartFadeAnimation();
            SubscribeToAction();
            Debug.Log("Consol Started");
    }
    private void SubscribeToAction()
    {
        _consolPanel.StartFadeAnimation += StartFadeAnimation;
    }


    private void StartFadeAnimation()
    {
        StartCoroutine(FadeRoutine());
    }
    
    
    private IEnumerator FadeRoutine()
    {
        float timer = 0;
        Color logoColor = _consolOpenButtonImage.color;
        Color transparentColor = new Color(logoColor.r, logoColor.g, logoColor.b, 0f);
        while (_isFaiding)
        {
            while (timer <= fadeDuration)
            {
                timer += Time.deltaTime;
                _consolOpenButtonImage.color = Color.Lerp(transparentColor, logoColor, timer);
                yield return null;
            }

            yield return new WaitForSeconds(1);
            
            while (timer >= 0)
            {
                timer -= Time.deltaTime;
                _consolOpenButtonImage.color = Color.Lerp(transparentColor, logoColor, timer);
                yield return null;
            }
            yield return null;
        }
    }
    

    private void AddEvent()
    {
        _consolOpenButton.onClick.AddListener(OpenConsol);
        _consolStopButton.onClick.AddListener(CloseConsol);
    }

    private void OpenConsol()
    {
        _consolPanel.gameObject.SetActive(true);
        _isConsolOpen = true;
        _isFaiding = false;
    }

    private void CloseConsol()
    {
        _consolPanel.gameObject.SetActive(false);
        _isConsolOpen = false;
        _isFaiding = true;
    }

    private void DeleteEvent()
    {
        _consolOpenButton.onClick.RemoveListener(OpenConsol);
        _consolStopButton.onClick.RemoveListener(CloseConsol);
    }

    private void OnDestroy()
    {
        DeleteEvent();
    }

}
