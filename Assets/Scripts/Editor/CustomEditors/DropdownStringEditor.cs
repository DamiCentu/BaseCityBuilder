using System;
using Reflection;
using SerializableObjects;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StringEnum), true)]
public class CustomDropdownDrawer : PropertyDrawer
{
    private int selectedIndex = 0;
 
    public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label)
    {
        SerializedProperty currentStringField = serializedProperty.FindPropertyRelative(nameof(StringEnum.CurrentString));
         
        Type serializedType = ReflectionUtils.GetType(serializedProperty.type);
        
        string[] availableChoices =  ((StringEnum)Activator.CreateInstance(serializedType)).AvailableChoices;

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