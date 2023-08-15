using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using WasderGQ.Utility.Singleton;

namespace WasderGQ.Utility.Consol
{
public class ConsolInformation : Singleton<ConsolInformation>
{
    [SerializeField]private TextMeshProUGUI _infoText;
    [SerializeField]private float _updateInterval = 0.5f; //
    [SerializeField]private float _frames = 0;
    [SerializeField]private float _timePassed = 0.0f;
    [SerializeField]private float _oldFrame = 0.0f;
    [SerializeField]private bool _isConsolOpen;
    [SerializeField]public event Action StartFadeAnimation;
    private void OnEnable()
    {
        _isConsolOpen = true;
    }

    private void OnDisable()
    {
        _isConsolOpen = false;
        StartFadeAnimation.Invoke();
    }

    private string FpsCounter()
    {
        float currentFps = 1 / Time.deltaTime;
        return currentFps.ToString("0");
    }
    private string AverageFpsCounter()
    {
        _frames++;
        _timePassed += Time.deltaTime;

        if (_timePassed > _updateInterval)
        {
            _oldFrame = _frames / _timePassed;
            _frames = 0;
            _timePassed = 0.0f;
        }
        return _oldFrame.ToString("0");
    }
    private string CurrentHz()
    {
        return Screen.currentResolution.refreshRate.ToString();
    }
    private string CurrentResolution()
    {
        return Screen.currentResolution.ToString();
    }
    private string CurrentScreenSize()
    {
        return Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString();
    }
    private string CurrentScreenDPI()
    {
        return Screen.dpi.ToString();
    }
    private string CurrentScreenOrientation()
    {
        return Screen.orientation.ToString();
    }
    private string CurrentScreenFullScrean()
    {
        return Screen.fullScreen.ToString();
    }
    private string CurrentScreenBrightness()
    {
        return Screen.brightness.ToString();
    }
    private string CurrentScreenSleepTimeout()
    {
        return Screen.sleepTimeout.ToString();
    }
    private string CurrentScreenAutorotateToPortrait()
    {
        return Screen.autorotateToPortrait.ToString();
    }
    private string CurrentScreenAutorotateToPortraitUpsideDown()
    {
        return Screen.autorotateToPortraitUpsideDown.ToString();
    }
    private string CurrentScreenAutorotateToLandscapeLeft()
    {
        return Screen.autorotateToLandscapeLeft.ToString();
    }
    private string CurrentScreenAutorotateToLandscapeRight()
    {
        return Screen.autorotateToLandscapeRight.ToString();
    }
    private string CurrentScreenSafeArea()
    {
        return Screen.safeArea.ToString();
    }
    private string CurrentScreenCutouts()
    {
        return Screen.cutouts.ToString();
    }
    private string CurrentScreenResolution()
    {
        return Screen.resolutions.ToString();
    }
    
    private void Update()
    {
        if (_isConsolOpen)
        {
            _infoText.text = $"Current FPS = {FpsCounter()} \n Average FPS = {AverageFpsCounter()} \n Current HZ = {CurrentHz()} \n Current Resolution = {CurrentResolution()} \n Current Screen Size =   {CurrentScreenSize()}  \n  Current Screen DPI =  {CurrentScreenDPI()}  \n Current Screen Orientation =  {CurrentScreenOrientation()}  \n  Current Screen FullScrean =  {CurrentScreenFullScrean()}  \n  Current Screen Brightness =  {CurrentScreenBrightness()}  \n  Current Screen SleepTimeout =  {CurrentScreenSleepTimeout()}  \n  Current Screen AutorotateToPortrait =  {CurrentScreenAutorotateToPortrait()}  \n  Current Screen AutorotateToPortraitUpsideDown =  {CurrentScreenAutorotateToPortraitUpsideDown()}  \n  Current Screen AutorotateToLandscapeLeft =  {CurrentScreenAutorotateToLandscapeLeft()}  \n  Current Screen AutorotateToLandscapeRight =  {CurrentScreenAutorotateToLandscapeRight()}  \n  Current Screen SafeArea =  {CurrentScreenSafeArea()}  \n  Current Screen Cutouts =  {CurrentScreenCutouts()}  \n  Current Screen Resolution =  {CurrentScreenResolution()}  \n ";
        }
    }
}
    
    
}

