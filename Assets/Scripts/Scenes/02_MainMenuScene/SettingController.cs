using System;
using UnityEngine;
using UnityEngine.UI;
using WasderGQ.Sudoku.Scenes.MainMenuScene;

namespace WasderGQ.Sudoku
{
    public class SettingController : MonoBehaviour
    {
        [SerializeField]  private Button _backButton;
        [SerializeField]  private SO_GameMode _gameMode;
        [SerializeField]  private Scrollbar _difficultySlider;
        
        
        
        
        public void InIt()
        {
            AddListenerToButton();
        }
        
        private void AddListenerToButton()
        {
            _backButton.onClick.AddListener(BackButton);
            _difficultySlider.onValueChanged.AddListener(ChangeValueSlider);
        }
        private void BackButton()
        {
            
          gameObject.SetActive(false);
          
        }

        private void ChangeValueSlider(float value)
        {
          int step = Convert.ToInt32(value / _difficultySlider.size);
          if (step == 0)                                                        //some bug happend fixed later. I setup the slider for 5 step but slider give me sixth value is 0 and   
                                                                                //I set 0 to 1 for extreme mode    
          {
              _gameMode.SetGameDifficulty(GameDifficulty.Extreme);
          }
          else
          {
              _gameMode.SetGameDifficulty((GameDifficulty) step); 
          }
            
        }
        
    }
}
