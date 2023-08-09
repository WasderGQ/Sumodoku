using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WasderGQ.Sudoku
{
    public class LoadScene : MonoBehaviour
    {
        private void Start()
        {
            PopUpMessage.Instance.SetOkeyPopUpMessage("Load Scene",null);
        }
    }
}
