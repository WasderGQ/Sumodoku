using System;
using System.Collections;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using WasderGQ.Utility.Singleton;


namespace WasderGQ.Sudoku.Services.GoogleAds
{
    public class BannerController : Singleton<BannerController>
    {
        private BannerView _bannerView;
        private AdSize adaptiveSize;
        private GameObject _adaptiveBanner;
        
    #if UNITY_EDITOR
        private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
    #elif UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-3306653392214615/8650743807";  // real used : ca-app-pub-3306653392214615/8650743807
    #elif UNITY_IPHONE
        private string _adUnitId = "ca-app-pub-3940256099942544/2934735716"; // not active
    #else
        private string _adUnitId = "unused";
    #endif
        // android Test : _adUnitId = "ca-app-pub-3940256099942544/6300978111"
        
        public void  Init()
        {
            GetAdaptiveSize();
            LoadBanner();
            ListenToAdEvents();
            DestroyOnSceneSwitch();
        }

        private void GetAdaptiveSize()
        {
            adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        }
        private void LoadBanner()
        {
                StartCoroutine(LoadAd());
        }
        
        private IEnumerator LoadAd()
        {
            while(SceneManager.sceneCount > 1)
            {
                yield return null;
            }
            CreateBannerView();
            AdRequest adRequest = new AdRequest.Builder().Build();
            adRequest.Keywords.Add("unity-admob-sample");
            Debug.Log("Loading banner ad.");
            _bannerView.LoadAd(adRequest);
        }
        private void CreateBannerView()
        {
            //Create a Adaptive Banner
            _bannerView = new BannerView(_adUnitId, adaptiveSize, AdPosition.Bottom);
        }
        private void DestroyAd()
        {
                Debug.Log("Destroying banner ad.");
                _bannerView.Destroy();
        }
        private void DestroyOnSceneSwitch()
        {
            SceneManager.activeSceneChanged += (current, next) =>
            {
                DestroyAd();
            };
        }
        private void ListenToAdEvents()
        {
            // Raised when an ad is loaded into the banner view.
            _bannerView.OnBannerAdLoaded += () =>
            {
                Debug.Log("Banner view loaded an ad with response : "
                          + _bannerView.GetResponseInfo());
            };
            // Raised when an ad fails to load into the banner view.
            _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : "
                               + error);
            };
            // Raised when the ad is estimated to have earned money.
            _bannerView.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            _bannerView.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Banner view recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            _bannerView.OnAdClicked += () =>
            {
                Debug.Log("Banner view was clicked.");
            };
            // Raised when an ad opened full screen content.
            _bannerView.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Banner view full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            _bannerView.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Banner view full screen content closed.");
            };
        }
    }
    
}
