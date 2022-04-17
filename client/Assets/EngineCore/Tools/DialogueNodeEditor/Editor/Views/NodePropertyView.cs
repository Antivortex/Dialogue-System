using System;
using UnityEditor;
using UnityEngine;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Views;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Windows;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Windows.Popups;
using VortexGames.EngineCore.Tools.NodeEditorBase.Utils;

namespace VortexGames.DialogueNodeEditor.Editor.Views
{
    public class NodePropertyView : ViewBase, IPropertiesModifier
    {
        private readonly INodeWindow _nodeWindow;

        public NodePropertyView(INodeWindow nodeWindow) : base("Property View")
        {
            _nodeWindow = nodeWindow;
        }

        public override void UpdateView(Vector2 mousePos, Rect editorRect, Rect percentageRect)
        {
            base.UpdateView(mousePos, editorRect, percentageRect);

            var graph = _nodeWindow.CurrentGraph;
            ViewTitle = graph != null ? graph.GraphName : "No Graph";
            GUILayout.BeginArea(ViewRect);
            GUILayout.Space(40);
            if (graph != null)
            {
                var selectedNode = graph.SelectedNode;
                if (selectedNode != null)
                {
                    EditorStyles.label.wordWrap = true;
                    selectedNode.DrawProperties(this);
                    EditorStyles.label.wordWrap = false;
                }
            }
            GUILayout.EndArea();
        }
    
        protected override void OnRightMouseDown(Event e)
        {

        }

        public void ChangeLabel(string currentLabel, Action<string> changeCallback)
        {
            ChangeLabelPopupWindow.InitPopup(currentLabel, changeCallback);
        }
    }
}
