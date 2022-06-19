using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Components;

using Kmnk.Core;

namespace Kmnk.MessageBoard
{
    [CustomEditor(typeof(MessageBoardViewer))]
    [CanEditMultipleObjects]
    class MessageBoardViewerEditor : EditorBase<MessageBoardViewer>
    {
        SerializedProperty _idProperty;
        SerializedProperty _pickupableProperty;

        protected override void FindProperties()
        {
            _target = target as MessageBoardViewer;
            _idProperty = serializedObject.FindProperty("_id");
            _pickupableProperty = serializedObject.FindProperty("_pickupable");
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
                EditorGUILayout.PropertyField(_pickupableProperty);
            }
        }

        internal override void ApplyModifiedProperties()
        {
            FindProperties();

            var messageBoard = GetMessageBoard(_idProperty.intValue);

            if (messageBoard == null) { return; }

            var pickup = _target.GetComponentInChildren<VRCPickup>();
            pickup.pickupable = _pickupableProperty.boolValue;

            var udon = _target.GetChildUdonBehaviour<Udon.MessageBoardViewer>();
            udon.SetPublicVariable("_messageBoard", messageBoard.GetChildUdonBehaviour<Udon.MessageBoard>());
        }

        private static MessageBoard GetMessageBoard(int id)
        {
            return Resources.FindObjectsOfTypeAll<MessageBoard>()
                .Where(x => AssetDatabase.GetAssetOrScenePath(x).EndsWith(".unity"))
                .Where(x => x.GetId() == id)
                .FirstOrDefault();
        }
    }
}