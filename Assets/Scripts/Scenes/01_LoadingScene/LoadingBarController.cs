using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using WasderGQ.Sudoku.BetweenScene;
using WasderGQ.Sudoku.Enums;
using WasderGQ.Sudoku.SceneManagement;
using WasderGQ.Sudoku.Services.GoogleAds;

namespace WasderGQ.Sudoku
{
    public class LoadingBarController : MonoBehaviour
    {
        [SerializeField] private LoadingBar _loadingBar;
        [SerializeField] private CancellationTokenSource _cancellationToken;
        [SerializeField] private TextMeshProUGUI _loadingInfoText;
        private async void Start()
        {
            bool taskBool = false;
            CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = new CancellationTokenSource();
            if (!_cancellationToken.IsCancellationRequested)
            {
                await WriteTextInfo("Loading Application Settings");
                _loadingBar.UpdateLoadStatus(50f,_cancellationTokenSource);
                 taskBool = await AppSettings.Instance.InIt();
                 if(taskBool)
                     _cancellationTokenSource.Cancel();
                 else
                 {
                     _cancellationTokenSource.Cancel();
                     ErrorMessage();
                 }
                await Task.Delay(200);
                await WriteTextInfo(" ");
                if (taskBool)
                {
                    await WriteTextInfo("Loading Google Ads");
                    _cancellationTokenSource = new CancellationTokenSource();
                    _loadingBar.UpdateLoadStatus(100f,_cancellationTokenSource);
                    taskBool = await GoogleAdsService.Instance.Init();
                    if(taskBool)
                        _cancellationTokenSource.Cancel();
                    else
                    {
                        _cancellationTokenSource.Cancel();
                        ErrorMessage();
                    }
                    await Task.Delay(200);
                    await WriteTextInfo(" ");
                    if(taskBool)
                    {
                        await WriteTextInfo("Loading Complete");
                        await Task.Delay(200);
                        SceneLoader.Instance.LoadScene(EnumScenes.MainMenuScene);
                    }
                }
            }
        }

        private void OnApplicationQuit()
        {
            _loadingBar._stopAnimation = true;
            _cancellationToken.Cancel();
            
        }

        private async Task WriteTextInfo(string text)
        {
            _loadingInfoText.text = text;
        }

        private void ErrorMessage()
        {
            PopUpMessage msg =PopUpController.CreatePopUpMessage();
            msg.SetOkeyPopUpMessage("Same Thing Went Wrong. Please Try Again Later",() => Application.Quit());
        }
    }
}
