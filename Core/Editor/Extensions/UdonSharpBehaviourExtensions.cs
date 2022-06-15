using System;
using System.Linq;
using UdonSharp;
using UdonSharpEditor;
using UnityEngine;
using VRC.Udon;

namespace Kmnk.Core
{
    public static class UdonSharpBehaviourExtensions
    {
        internal static UdonBehaviour GetChildUdonBehaviour<T>(this Component component) where T : UdonSharpBehaviour
        {
            return component.GetChildUdonBehaviours()
                .Where(x => UdonSharpEditorUtility.GetUdonSharpBehaviourType(x) == typeof(T))
                .FirstOrDefault();
        }

        internal static UdonBehaviour[] GetChildUdonBehaviours(this Component component)
        {
            return component.GetComponentsInChildren<UdonBehaviour>();
        }
    }
}