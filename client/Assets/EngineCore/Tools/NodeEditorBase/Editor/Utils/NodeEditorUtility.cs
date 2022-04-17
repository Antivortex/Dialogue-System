using UnityEditor;
using UnityEngine;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils
{
    public static class NodeEditorUtility
    {
        public static string TextField(string label, string text, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(label);
            var result = EditorGUILayout.TextField(text, options);
            EditorGUILayout.EndVertical();
            return result;
        }
    }
}