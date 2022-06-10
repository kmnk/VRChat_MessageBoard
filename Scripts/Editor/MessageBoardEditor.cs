using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kmnk.MessageBoard
{
    [CustomEditor(typeof(MessageBoard))]
    class MessageBoardEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var messageBoard = (MessageBoard)target;

            var idProperty = serializedObject.FindProperty("_id");
            var onlyWorldOwnerModeProperty = serializedObject.FindProperty("_onlyWorldOwnerMode");
            var titleProperty = serializedObject.FindProperty("_title");
            var timeFormatProperty = serializedObject.FindProperty("_timeFormat");
            var initialMessagesProperty = serializedObject.FindProperty("_initialMessages");
            var initialNameProperty = serializedObject.FindProperty("_initialName");
            var initialTimeProperty = serializedObject.FindProperty("_initialTime");

            serializedObject.Update();

            var labelStyle = new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
            };
            labelStyle.normal.textColor = Color.white;

            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.LabelField("Core", labelStyle);
                EditorGUILayout.PropertyField(idProperty);
            }

            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.LabelField("Option", labelStyle);
                EditorGUILayout.PropertyField(onlyWorldOwnerModeProperty);
                EditorGUILayout.PropertyField(titleProperty);
            }

            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.LabelField("Format", labelStyle);
                EditorGUILayout.PropertyField(timeFormatProperty);
            }

            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.LabelField("Initial Messages", labelStyle);
                EditorGUILayout.PropertyField(initialMessagesProperty);
                EditorGUILayout.PropertyField(initialNameProperty);
                EditorGUILayout.PropertyField(initialTimeProperty);
            }

            if (!serializedObject.ApplyModifiedProperties()) { return; }

            var initialMessages = Enumerable.Range(0, initialMessagesProperty.arraySize)
                .Select(x => initialMessagesProperty.GetArrayElementAtIndex(x))
                .Select(x => x.stringValue)
                .ToArray();

            var udon = messageBoard.GetChildUdonBehaviour<Udon.MessageBoard>();
            udon.SetPublicVariable("_id", idProperty.intValue);
            udon.SetPublicVariable("_onlyWorldOwnerMode", onlyWorldOwnerModeProperty.boolValue);
            udon.SetPublicVariable("_title", titleProperty.stringValue);
            udon.SetPublicVariable("_timeFormat", timeFormatProperty.stringValue);
            udon.SetPublicVariable("_initialMessages", initialMessages);
            udon.SetPublicVariable("_initialName", initialNameProperty.stringValue);
            udon.SetPublicVariable("_initialTime", initialTimeProperty.stringValue);
        }
    }
}