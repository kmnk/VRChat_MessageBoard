using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VRC.Udon;
using VRC.SDK3.Components;

using Kmnk.Core;

namespace Kmnk.MessageBoard
{
    [CustomEditor(typeof(InputBoard))]
    [CanEditMultipleObjects]
    class InputBoardEditor : EditorBase<InputBoard>
    {
        SerializedProperty _idProperty;
        SerializedProperty _pickupableProperty;
        SerializedProperty _templateMessagesProperty;
        SerializedProperty _templateMessagesTransformProperty;
        SerializedProperty _templateMessageButtonOriginProperty;

        protected override void FindProperties()
        {
            _target = target as InputBoard;
            _idProperty = serializedObject.FindProperty("_id");
            _pickupableProperty = serializedObject.FindProperty("_pickupable");
            _templateMessagesProperty = serializedObject.FindProperty("_templateMessages");
            _templateMessagesTransformProperty = serializedObject.FindProperty("_templateMessagesTransform");
            _templateMessageButtonOriginProperty = serializedObject.FindProperty("_templateMessageButtonOrigin");
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
                EditorGUILayout.PropertyField(_templateMessagesProperty);
            }

            EditorGUILayout.Space();

            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                EditorGUILayout.LabelField("System", BoxTitleStyle());
                EditorGUILayout.PropertyField(_templateMessagesTransformProperty);
                EditorGUILayout.PropertyField(_templateMessageButtonOriginProperty);
            }
        }

        internal override void ApplyModifiedProperties()
        {
            FindProperties();

            var messageBoard = GetMessageBoard(_idProperty.intValue);

            if (messageBoard == null) { return; }

            var templateMessages = Enumerable.Range(0, _templateMessagesProperty.arraySize)
                .Select(x => _templateMessagesProperty.GetArrayElementAtIndex(x))
                .Select(x => x.stringValue)
                .ToArray();

            var pickup = _target.GetComponentInChildren<VRCPickup>();
            pickup.pickupable = _pickupableProperty.boolValue;

            var udon = _target.GetChildUdonBehaviour<Udon.InputBoard>();
            udon.SetPublicVariable("_messageBoard", messageBoard.GetChildUdonBehaviour<Udon.MessageBoard>());
            udon.SetPublicVariable("_templateMessages", templateMessages);

            if (IsActiveInHierarchy() && !IsInPrefabMode())
            {
                ResetTemplateMessages(
                    templateMessages,
                    _templateMessagesTransformProperty.objectReferenceValue as Transform,
                    _templateMessageButtonOriginProperty.objectReferenceValue as GameObject
                );
            }
        }

        private void ResetTemplateMessages(
            string[] templateMessages,
            Transform templateMessagesTransform,
            GameObject templateMessageButtonOrigin
        )
        {
            // clean current buttons
            foreach (var t in templateMessagesTransform.GetComponentsInChildren<UdonBehaviour>(true))
            {
                if (t.gameObject == templateMessageButtonOrigin) { continue; }
                DestroyImmediate(t.gameObject);
            }

            // create new buttons
            foreach (var templateMessage in templateMessages)
            {
                var o = Instantiate(templateMessageButtonOrigin);
                o.SetActive(true);
                o.transform.SetParent(templateMessagesTransform);
                o.transform.localScale = Vector3.one;
                o.transform.localPosition = Vector3.zero;
                o.transform.localRotation = Quaternion.identity;

                var buttonText = o.GetComponentInChildren<Text>();
                buttonText.text = templateMessage;
            }

            // hide original button
            templateMessageButtonOrigin.SetActive(false);
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