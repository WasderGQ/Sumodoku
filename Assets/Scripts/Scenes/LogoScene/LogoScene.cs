using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WasderGQ.Sudoku.SceneManagement;
using WasderGQ.Sudoku.Enums;
namespace WasderGQ.Sudoku.Scenes.LogoScene
{
    public class LogoScene : MonoBehaviour
    {
        [SerializeField] private Image _logo = null;
        [SerializeField] private GameObject _LogoScene;
        public TaskCompletionSource<bool> TaskCompleteSourceAnimation;
        private void Start()
        {
            TaskCompleteSourceAnimation = new TaskCompletionSource<bool>();
            StartCoroutine(LogoAnimation());
        }

        private IEnumerator LogoAnimation()
        {
            float timer = 0;
            Color logoColor = _logo.color;
            Color transparentColor = new Color(logoColor.r, logoColor.g, logoColor.b, 0f);
            while (timer <= 1)
            {
                timer += Time.deltaTime;
                _logo.color = Color.Lerp(transparentColor, logoColor, timer);
                yield return null;
            }

            yield return new WaitForSeconds(1);
            
            while (timer >= 0)
            {
                timer -= Time.deltaTime;
                _logo.color = Color.Lerp(transparentColor, logoColor, timer);
                yield return null;
            }
            _LogoScene.SetActive(false);
            TaskCompleteSourceAnimation.SetResult(true);
            SceneLoader.Instance.LoadScene(EnumScenes.LoadingScene);
            yield return null;
        }
    }

}
