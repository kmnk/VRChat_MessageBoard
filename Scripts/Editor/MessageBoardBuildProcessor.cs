using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

using Kmnk.Core;

namespace Kmnk.MessageBoard
{
    class MessageBoardBuildProcessor : IProcessSceneWithReport
    {
        public int callbackOrder => 0;

        public void OnProcessScene(UnityEngine.SceneManagement.Scene scene, BuildReport report)
        {
            Reapply<MessageBoard, MessageBoardEditor>();
            Reapply<InputBoard, InputBoardEditor>();
            Reapply<MessageBoardViewer, MessageBoardViewerEditor>();
        }

        void Reapply<TBehaviour, TEditor>()
            where TBehaviour : MonoBehaviour
            where TEditor : EditorBase<TBehaviour> 
        {
            var behaviours
                = Resources.FindObjectsOfTypeAll<TBehaviour>()
                    .Where(x => AssetDatabase.GetAssetOrScenePath(x).EndsWith(".unity"))
                    .ToArray();

            foreach (var behaviour in behaviours)
            {
                var editor = Editor.CreateEditor(behaviour, typeof(TEditor)) as TEditor;
                editor.ApplyModifiedProperties();
            }
        }
    }
}