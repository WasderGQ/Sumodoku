using UnityEngine;
using WasderGQ.Sudoku.Generic;


namespace WasderGQ.Sudoku.BetweenScene
{
    public class AppSettings : Singleton<AppSettings>
    {
        
        public void InIt()
        {
            OnAwakeSetScreenSettings();
         
        }

        private void OnAwakeSetScreenSettings()
        {
            SetScreenSleepMode(ScreenStatus.NeverSleep);
            SetScreenFrameRate(Screen.currentResolution.refreshRate);
            SetSoundVolume(SoundStatus.On);

        }
        public void SetScreenSleepMode(ScreenStatus screenStatus)
        {
            Screen.sleepTimeout = (int)screenStatus;
        }
        public void SetScreenFrameRate(int frameRate)
        {
            Application.targetFrameRate = frameRate;
        }
        public void SetSoundVolume(SoundStatus soundStatus)
        {
            AudioListener.volume = (int)soundStatus;
        }
        
    }
}



