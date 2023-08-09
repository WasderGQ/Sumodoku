using UnityEditor;
using UnityEngine;
using WasderGQ.Sudoku.Utility;

namespace WasderGQ.Utility.UnityEditor
{

    public class DisableValueChange : PropertyAttribute
    {
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DisableValueChange))]
    public class DisableValueChangeOnTheEditor : PropertyDrawer
    {

        private bool valueChanged = false;

        public override void OnGUI(Rect location, SerializedProperty property, GUIContent line)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(location, property, line, true);

            if (EditorGUI.EndChangeCheck())
            {
                valueChanged = true;
            }

            if (valueChanged || IsArrayElementChanged(property))
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(location, property, line, true);
                GUI.enabled = true;
            }
        }
        
        
        private bool IsArrayElementChanged(SerializedProperty property)
        {
            if (property.isArray && property.arraySize > 0)
            {
                for (int i = 0; i < property.arraySize; i++)
                {
                    SerializedProperty elementProperty = property.GetArrayElementAtIndex(i);
                    if (elementProperty != null && EditorGUI.GetPropertyHeight(elementProperty) > 0f && EditorGUI.GetPropertyHeight(elementProperty, GUIContent.none) > 0f)
                    {
                        if (EditorGUI.EndChangeCheck())
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

    }
    
#endif

}
