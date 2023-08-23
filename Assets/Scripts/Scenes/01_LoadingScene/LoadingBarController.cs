using System.Collections;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using WasderGQ.Sudoku.BetweenScene;
using WasderGQ.Sudoku.Enums;
using WasderGQ.Sudoku.SceneManagement;
using WasderGQ.Sudoku.Services.GoogleAds;

namespace WasderGQ.Sudoku.Scenes._01_LoadingScene
{
    public class LoadingBarController : MonoBehaviour
    {
        [SerializeField] private LoadingBar _loadingBar;
        [SerializeField] private CancellationTokenSource _cancellationToken;
        [SerializeField] private TextMeshProUGUI _loadingInfoText;
        [SerializeField] private bool _showLoadingInfoText;
        [SerializeField] private Transform _notStuckIcon;
        [SerializeField] private bool _stopAnimation;
        [SerializeField] private GameObject _sceneObjects;
        private async void Start()
        {
            StartLoadingNotStuckAnimation();
            bool taskBool = false;
            CancellationTokenSource _animationCancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = new CancellationTokenSource();
            if (!_cancellationToken.IsCancellationRequested)
            {
                await WriteTextInfo("Loading Application Settings");
                _loadingBar.UpdateLoadStatus(50f,_animationCancellationTokenSource);
                 taskBool = await AppSettings.Instance.InIt();
                 if(taskBool)
                     _animationCancellationTokenSource.Cancel();
                 else
                 {
                     _animationCancellationTokenSource.Cancel();
                     ErrorMessage();
                 }
                await Task.Delay(200);
                await WriteTextInfo(" ");
                if (taskBool)
                {
                    await WriteTextInfo("Loading Google Ads");
                    _animationCancellationTokenSource = new CancellationTokenSource();
                    _loadingBar.UpdateLoadStatus(100f,_animationCancellationTokenSource);
                    taskBool = await GoogleAdsService.Instance.Init();
                    if(taskBool)
                        _animationCancellationTokenSource.Cancel();
                    else
                    {
                        _animationCancellationTokenSource.Cancel();
                        ErrorMessage();
                    }
                    await Task.Delay(200);
                    await WriteTextInfo(" ");
                    if(taskBool)
                    {
                        await WriteTextInfo("Loading Complete");
                        await Task.Delay(200);
                        _stopAnimation = true;
                        DestroyLoadSceneObject();
                        SceneLoader.Instance.WLoadScene(EnumScenes.MainMenuScene);
                    }
                }
            }
        }

        private void OnApplicationQuit()
        {
            _loadingBar._stopAnimation = true;
            _cancellationToken.Cancel();
            
        }

        private void DestroyLoadSceneObject()
        {
            Destroy(_sceneObjects);
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            _loadingBar._stopAnimation = true;
            _cancellationToken.Cancel();
        }
        private async Task WriteTextInfo(string text)
        {
            if(_showLoadingInfoText)
            _loadingInfoText.text = text;
        }

        private void StartLoadingNotStuckAnimation()
        {
            if(!_showLoadingInfoText)
            StartCoroutine(LoadingNotStuckAnimation());
        }

        private IEnumerator LoadingNotStuckAnimation()
        {
            while (!_stopAnimation)
            {
                _notStuckIcon.DORotate(new Vector3(0,0,-360),01f,RotateMode.FastBeyond360).SetEase(Ease.Linear);
                yield return new WaitForSeconds(1f);
            }
        }
        private void ErrorMessage()
        {
            PopUpMessage msg =PopUpController.CreatePopUpMessage();
            msg.SetOkeyPopUpMessage("Same Thing Went Wrong. Please Try Again Later",() => Application.Quit());
        }
    }
}
