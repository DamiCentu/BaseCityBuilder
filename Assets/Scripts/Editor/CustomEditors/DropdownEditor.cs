using System;
using SerializableObjects;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomPropertyDrawer(typeof(StringEnum), true)]
public class CustomDropdownDrawer : PropertyDrawer
{
    private int selectedIndex = 0;
    private static string[] availableChoices = new string[]{ "User1", "User2" };
 
    public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
    {
        SerializedProperty currentStringField = serializedProperty.FindPropertyRelative(nameof(StringEnum.CurrentString));

        EditorGUI.BeginChangeCheck();
        
        selectedIndex = EditorGUI.Popup(position, Array.IndexOf(availableChoices, currentStringField.stringValue), availableChoices);

        if(EditorGUI.EndChangeCheck())
        {
            currentStringField.stringValue = availableChoices[selectedIndex];
            EditorUtility.SetDirty(serializedProperty.serializedObject.targetObject);
            AssetDatabase.SaveAssets();
        }
    }
}