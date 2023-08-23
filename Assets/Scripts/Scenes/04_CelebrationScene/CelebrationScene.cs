using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WasderGQ.Sudoku;
using WasderGQ.Sudoku.Enums;
using WasderGQ.Sudoku.SceneManagement;

public class CelebrationScene : MonoBehaviour
{
    [SerializeField] private BackGroundAnimationController _backGroundAnimationController;
    [SerializeField] private Button _backToMainMenuButton;
    [SerializeField] private TextMeshProUGUI _celebrationText;
    [SerializeField] private List<Color> _colorList;
    private bool _stopAnimation;
    void Start()
    {
        AddButtonListener();
        _backGroundAnimationController.Init(_colorList);
        CelebrateTextAnimation();
    }
    void CelebrateTextAnimation()
    {
        StartCoroutine(CelebrateTextAnimationCoroutine());
    }

    private IEnumerator CelebrateTextAnimationCoroutine()
    {
        while (!_stopAnimation)
        {
            _celebrationText.DOColor(_colorList[Random.Range(0, _colorList.Count - 1)], 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    
    
    void AddButtonListener()
    {
        _backToMainMenuButton.onClick.AddListener(BackToMainMenu);
    }
    void BackToMainMenu()
    {
        _backGroundAnimationController.StopAnimation = true;
        _stopAnimation = true;
        SceneLoader.Instance.WLoadScene(EnumScenes.MainMenuScene);
    }
    void RemoveButtonListener()
    {
        _backToMainMenuButton.onClick.RemoveAllListeners();
    }
    void OnDestroy()
    {
        RemoveButtonListener();
    }
    
}
