using System;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;
using WasderGQ.Sudoku.Generic;
using Debug = UnityEngine.Debug;

namespace WasderGQ.Sudoku.Services.GoogleAds
{
    public class GoogleAdsService : Singleton<GoogleAdsService>
    {
        private Action<InitializationStatus> _initStatus;
        public bool GoogleAdsServiceBool { get; private set; }
        private GameObject _googleAdsService;
        private GameObject _adaptiveBanner;

        public async Task<bool> Init()
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            InitializeGoogleAds(taskCompletionSource);
            await taskCompletionSource.Task;
            return taskCompletionSource.Task.Result;
        }


        private void InitializeGoogleAds(TaskCompletionSource<bool> taskCompletionSource)
        {
            Debug.Log("Google Ads Initialized");
            MobileAds.Initialize(initStatus =>
            {
                OnInitializationComplete();
                taskCompletionSource.SetResult(true);
            });

        }

        private void OnInitializationComplete()
        {
            Debug.Log("Google Ads Initialized Done !!!");
            MainThreadDispatcher.RunOnMainThread(() =>
            {
                BannerController.Instance.Init();
                MoveInitializedGoogleAdsServices();
            });
            
        }

        private void MoveInitializedGoogleAdsServices()
        {
            _googleAdsService = GameObject.Find("New Game Object");
            _adaptiveBanner = GameObject.FindGameObjectWithTag("ADAPTIVE_Banner");
            if (_googleAdsService != null && _adaptiveBanner != null)
            {

                _googleAdsService.transform.SetParent(transform);
                _adaptiveBanner.transform.SetParent(transform);
                _googleAdsService.name = "GoogleAdsService";
                _googleAdsService.tag = "GoogleAdsService";
                _adaptiveBanner.name = "ADAPTIVE_Banner";
                SetShowAds(false, AdsType.Banner);
            }
            else
            {
                Debug.LogError("GoogleAdsService can't find");
            }
        }

        public bool SetShowAds(bool show, AdsType type)
        {
            switch (type)
            {
                case AdsType.Banner:
                    if (_adaptiveBanner == null)
                    {
                        Debug.Log("AdaptiveBanner is null");
                        return false;
                    }
                    _adaptiveBanner.SetActive(show);
                    Debug.Log("AdaptiveBanner is " + show);
                    return true;
                case AdsType.Interstitial:
                    Debug.Log("Not Created Rewarded");
                    return true;
                case AdsType.Rewarded:
                    Debug.Log("Not Created Rewarded");
                    return true;
                case AdsType.RewardedInterstitial:
                    Debug.Log("Not Created RewardedInterstitial");
                    return true;
                default:
                    Debug.LogError("AdsType not found");
                    return false;
            }

        }
    }
}
