using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogToggle))]
public class DialogToggleDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(property.isExpanded, label);
        if (property.isExpanded)
        {
            SerializedProperty characterProperty = property.FindPropertyRelative("character");
            SerializedProperty dialogNameProperty = property.FindPropertyRelative("dialogName");
            Character character = (Character)characterProperty.objectReferenceValue;
            EditorGUILayout.PropertyField(characterProperty, new GUIContent("Character"));
            if (character != null)
            {
                List<string> dialogsNames = new List<string>();
                for (int i = 0; i < character.Dialogs.Length; i++)
                {
                    if (character.Dialogs[i].scriptableObject != null)
                    {
                        dialogsNames.Add(character.Dialogs[i].scriptableObject.name);   
                    }
                }
                if (dialogsNames.Count > 0)
                {
                    int index = 0;
                    index = EditorGUILayout.Popup("Dialog Name", index, dialogsNames.ToArray());
                    dialogNameProperty.stringValue = dialogsNames[index];   
                }
                else
                {
                    GUIStyle errorStyle = new GUIStyle(EditorStyles.label);
                    errorStyle.normal.textColor = Color.red;
                    EditorGUILayout.LabelField("Character doesn't have dialog scriptable object!", errorStyle);
                }
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
