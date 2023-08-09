using UnityEditor;
using UnityEngine;

namespace WasderGQ.Sudoku.Utility
{
#if UNITY_EDITOR
    public class LockVariableOnEditor : PropertyAttribute
    {
    }    
   
    
    [CustomPropertyDrawer(typeof(LockVariableOnEditor))]
    public class LockVariable : PropertyDrawer
    {
        public override void OnGUI(Rect location, SerializedProperty property, GUIContent line)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(location, property, line, true);
            GUI.enabled = true;
        }
    }
#endif
    


    
}
