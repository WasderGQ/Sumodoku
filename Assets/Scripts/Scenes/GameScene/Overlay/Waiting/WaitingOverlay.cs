using UnityEngine;

namespace WasderGQ.Sudoku
{
    public class WaitingOverlay : MonoBehaviour
    {
        [SerializeField] private GameObject _logo;
        [SerializeField] private int _logoSpeed = 2;
        void Update()
        {
            SpinLogo();
        }

        private void SpinLogo()
        {
            _logo.transform.Rotate(Vector3.forward * Time.deltaTime * _logoSpeed * 25f);
        }
        
       
    }
}
