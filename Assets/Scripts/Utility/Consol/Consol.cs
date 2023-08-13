using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Consol : MonoBehaviour
{
    [SerializeField] private bool _isConsolOpen;
    [SerializeField] private bool _isFaiding;
    [SerializeField] private Button _consolOpenButton;
    [SerializeField] private Image _consolOpenButtonImage;
    [SerializeField] private Button _consolStopButton;
    [SerializeField] private GameObject _consolPanel;
    [SerializeField] private float fadeDuration = 1.0f; // Solma süresi (saniye)

    private void Start()
    {
        if (_isConsolOpen)
        {
            AddEvent();
            StartCoroutine(FadeRoutine());
        }
    }

    private IEnumerator FadeRoutine()
    {
        float timer = 0;
        Color logoColor = _consolOpenButtonImage.color;
        Color transparentColor = new Color(logoColor.r, logoColor.g, logoColor.b, 0f);
        while (_isFaiding)
        {
            while (timer <= 0.5f)
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
        _consolStopButton.onClick.AddListener(StopConsol);
    }

    private void OpenConsol()
    {
        _consolPanel.SetActive(true);
    }

    private void StopConsol()
    {
        _consolPanel.SetActive(false);
    }

    private void DeleteEvent()
    {
        _consolOpenButton.onClick.RemoveListener(OpenConsol);
        _consolStopButton.onClick.RemoveListener(StopConsol);
    }

    private void OnDestroy()
    {
        DeleteEvent();
    }

}
