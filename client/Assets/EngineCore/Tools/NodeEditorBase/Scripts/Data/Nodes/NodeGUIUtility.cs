#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Data.Nodes
{
    public static class NodeGUIUtility
    {
        private const float InputButtonWidth = 24f;
        private const float InputButtonHeight = 24f;

        public static Rect GetInputButtonRect(Rect nodeRect, float verOffset = 0f)
        {
            return new Rect(nodeRect.x - InputButtonWidth, 
                            nodeRect.y + nodeRect.height * 0.5f + (verOffset-0.5f)* InputButtonHeight, 
                            InputButtonWidth, InputButtonHeight);
        }

        public static Rect GetOutputButtonRect(Rect nodeRect, float verOffset = 0f)
        {
            return new Rect(nodeRect.xMax, 
                            nodeRect.y + nodeRect.height * 0.5f + (verOffset - 0.5f) * InputButtonHeight,
                            InputButtonWidth, InputButtonHeight);
        }

        public static void DrawChangeableLabel(string title, string label, Action onChangeButton, Action onRemoveButton = null)
        {
            EditorGUILayout.LabelField(title);
            EditorGUILayout.LabelField(label);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Change", GUILayout.Height(20), GUILayout.Width(100)))
                onChangeButton();

            if (onRemoveButton != null)
            {
                if (GUILayout.Button("Remove", GUILayout.Height(20), GUILayout.Width(100)))
                    onRemoveButton();
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif