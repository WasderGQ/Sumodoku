
using System.Collections.Generic;
using UnityEngine;

namespace WasderGQ.Sudoku
{
    public class MainAnimationController : MonoBehaviour
    {
        public FrontObjectAnimationController _frontObjectAnimationController;
        public BackGroundAnimation _backGroundAnimationController;
        [SerializeField]private List<Color> _mainColorList = new List<Color>();

        public bool StopAnimation
        {
            set
            {
                _frontObjectAnimationController._stopAnimation = value;
                _backGroundAnimationController._stopAnimation = value;
            }
        }
        
        
        
        public void Init()
        {
            AddedColorToList();
            _frontObjectAnimationController.Init(_mainColorList);
            _backGroundAnimationController.Init(_mainColorList);
        }
        
        
        private void AddedColorToList()
        {
            _mainColorList.Add(new Color(210f/255f,39f/255f,48f/255f)); //Neon Fuschia
            _mainColorList.Add(new Color(199f/255f,36f/255f,177f/255f)); //Neon Purple
            _mainColorList.Add(new Color(77f/255f, 77f/255f, 225f/255f)); // Neon Blue
            _mainColorList.Add(Color.cyan); // Neon Cyan
            _mainColorList.Add(new Color(68f/255f,214f/255f,44f/255f)); //Neon Green
            _mainColorList.Add(new Color(224f/255f,231f/255f,34f/255f)); //Neon Yellow
        }
    }
}
