using System.Threading.Tasks;
using UnityEngine;
using WasderGQ.Sudoku.Generic;


namespace WasderGQ.Sudoku.BetweenScene
{
    public class AppSettings : Singleton<AppSettings>
    {
        
        public async Task<bool> InIt()
        {
            return await OnAwakeSetScreenSettings();
        }

        private async Task<bool >OnAwakeSetScreenSettings()
        {
            SetScreenSleepMode(ScreenStatus.NeverSleep);
            SetScreenFrameRate(Screen.currentResolution.refreshRate);
            SetSoundVolume(SoundStatus.On);
            return true;
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



