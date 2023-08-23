using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace WasderGQ.Sudoku
{
    public class PopUpController
    {
        [CanBeNull]public static PopUpMessage CreatePopUpMessage()
        {
            PopUpMessage popUpMessage = CatchPrefabPopUpMessage();
            if (popUpMessage != null)
            {
                PopUpMessage popUpMessageInstance = GameObject.Instantiate<PopUpMessage>(popUpMessage);
                return popUpMessageInstance;
            }
            return popUpMessage;
        }
        
        [CanBeNull]private static PopUpMessage CatchPrefabPopUpMessage()
        {
            try
            {
               PopUpMessage temp = Resources.Load<PopUpMessage>("Prefabs/PopUpMessage/PopUpMessageCanvas");
               if(temp == null)
                   throw new Exception("PopUpMessage Prefab not found");
               return temp;
            }
            catch (Exception e)
            {
                Debug.Log("PopUpMessage Prefab not found "  + "Or /n" + "PopUpMessage doesnt have PopUpMessage Scripts" + e.Message );
                return null;
            }
        }
        
        
        
    }
}
