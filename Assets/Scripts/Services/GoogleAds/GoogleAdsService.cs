using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;
using WasderGQ.Sudoku.Generic;

namespace WasderGQ.Sudoku.Services.GoogleAds
{
    public class GoogleAdsService : Singleton<GoogleAdsService>
    {
        private Action<InitializationStatus> _initStatus;
        public bool GoogleAdsServiceBool { get; private set; }
        public async Task<bool> Init()
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
           await InitializeGoogleAds(taskCompletionSource);
           await taskCompletionSource.Task;
           return taskCompletionSource.Task.Result;
        }
    

        private async Task InitializeGoogleAds(TaskCompletionSource<bool> taskCompletionSource)
        {
            MobileAds.Initialize(initStatus =>
            {
                Debug.Log("Google Ads Initialized");
                taskCompletionSource.SetResult(true);
            });
            
        }

        
    }
}
