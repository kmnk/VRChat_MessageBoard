using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kmnk.MessageBoard
{
    [CustomEditor(typeof(MessageBoard))]
    class MessageBoardEditor : EditorBase<MessageBoard>
    {
        SerializedProperty _idProperty;
        SerializedProperty _onlyWorldOwnerModeProperty;
        SerializedProperty _titleProperty;
        SerializedProperty _timeFormatProperty;
        SerializedProperty _initialMessagesProperty;
        SerializedProperty _initialNameProperty;
        SerializedProperty _initialTimeProperty;

        protected override void FindProperties()
        {
            _target = target as MessageBoard;
            _idProperty = serializedObject.FindProperty("_id");
            _onlyWorldOwnerModeProperty = serializedObject.FindProperty("_onlyWorldOwnerMode");
            _titleProperty = serializedObject.FindProperty("_title");
            _timeFormatProperty = serializedObject.FindProperty("_timeFormat");
            _initialMessagesProperty = serializedObject.FindProperty("_initialMessages");
            _initialNameProperty = serializedObject.FindProperty("_initialName");
            _initialTimeProperty = serializedObject.FindProperty("_initialTime");
        }

        protected override void LayoutGUI()
        {
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.LabelField("Core", BoxTitleStyle());
                EditorGUILayout.PropertyField(_idProperty);
            }

            EditorGUILayout.Space();

            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.LabelField("Option", BoxTitleStyle());
                EditorGUILayout.PropertyField(_onlyWorldOwnerModeProperty);
                EditorGUILayout.PropertyField(_titleProperty);
                EditorGUILayout.PropertyField(_timeFormatProperty);
            }

            EditorGUILayout.Space();

            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.LabelField("Initial Messages", BoxTitleStyle());
                EditorGUILayout.PropertyField(_initialMessagesProperty);
                EditorGUILayout.PropertyField(_initialNameProperty);
                EditorGUILayout.PropertyField(_initialTimeProperty);
            }
        }

        internal override void ApplyModifiedProperties()
        {
            FindProperties();

            var initialMessages = Enumerable.Range(0, _initialMessagesProperty.arraySize)
                .Select(x => _initialMessagesProperty.GetArrayElementAtIndex(x))
                .Select(x => x.stringValue)
                .ToArray();

            var udon = _target.GetChildUdonBehaviour<Udon.MessageBoard>();
            udon.SetPublicVariable("_onlyWorldOwnerMode", _onlyWorldOwnerModeProperty.boolValue);
            udon.SetPublicVariable("_title", _titleProperty.stringValue);
            udon.SetPublicVariable("_timeFormat", _timeFormatProperty.stringValue);
            udon.SetPublicVariable("_initialMessages", initialMessages);
            udon.SetPublicVariable("_initialName", _initialNameProperty.stringValue);
            udon.SetPublicVariable("_initialTime", _initialTimeProperty.stringValue);
        }
    }
}