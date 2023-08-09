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
        public void Init()
        {
            SetStatusFunc();
            InitializeGoogleAds();
        }
    
        private void SetStatusFunc()
        {
            _initStatus = StatusChooser;
        }

        private async Task<bool> InitializeGoogleAds()
        { 
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            MobileAds.Initialize((_initStatus) =>
            {
                tcs.SetResult(true);
            }); 
           
            bool result = await tcs.Task;
            return result;
        }

        private void StatusChooser(InitializationStatus status)
        {
            Dictionary<string, AdapterStatus> adapterStatesDic = status.getAdapterStatusMap();
            if (adapterStatesDic.Count > 0)
            {
                Success();
            }
            else
            {
                Failed();
            }
        }
    
        private void Failed()
        {
            GoogleAdsServiceBool = false;
            Debug.LogError("GoogleAds Unsuccessfully Init");

        }

        private void Success()
        {
            GoogleAdsServiceBool = true;
            Debug.Log($"GoogleAds Successfully Init");
            
        }
    }
}
