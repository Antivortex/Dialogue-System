using System;
using UnityEngine;
using VortexGames.DialogueNodeEditor.Editor.ContextMenus;
using VortexGames.EngineCore.Tools.DialogueNodeEditor.Editor.ContextMenus;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Views;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Windows;
using VortexGames.EngineCore.Tools.NodeEditorBase.Data;

namespace VortexGames.DialogueNodeEditor.Editor.Views
{
    public class NodeWorkView : ViewBase
    {
        private readonly INodeWindow _nodeWindow;
        private Vector2 _scrollPos;
        private readonly ContextMenuHandler _nodeContextMenuHandler = new ContextMenuHandler();

        public NodeWorkView(INodeWindow nodeWindow) : base("Work View")
        {
            _nodeWindow = nodeWindow;

            ContextMenuHandler.AddAction(new CreateGraphAction());
            ContextMenuHandler.AddAction(new LoadGraphAction());
            ContextMenuHandler.AddAction(new SaveGraphAction());

            Func<NodeGraph> nodeGetter = () => nodeWindow.CurrentGraph;
            ContextMenuHandler.AddAction(new AddPhraseNodeAction(nodeGetter));
            ContextMenuHandler.AddAction(new AddChoiceNodeAction(nodeGetter));
            _nodeContextMenuHandler.AddAction(new RemoveNodeAction(nodeGetter));
        }
    
        public override void UpdateView(Vector2 mousePos, Rect editorRect, Rect percentageRect)
        {
            var graph = _nodeWindow.CurrentGraph;
            ContextMenuHandler.SetMenuEnabled("Save Graph", graph != null);
            ContextMenuHandler.SetMenuEnabled("Add Phrase Node", graph != null);
            ContextMenuHandler.SetMenuEnabled("Add Choice Node", graph != null);

            base.UpdateView(mousePos, editorRect, percentageRect);
        
            ViewTitle = graph != null ? graph.GraphName : "No Graph";

            GUILayout.BeginArea(ViewRect);
            if (graph != null)
            {
                _scrollPos = GUI.BeginScrollView(ViewRect, _scrollPos, GetScrollViewRect(ViewRect), true, true);
                graph.UpdateGraphGui(mousePos, ViewRect, ViewSkin);
                GUI.EndScrollView();
            }

            GUILayout.EndArea();

        }

        private Rect GetScrollViewRect(Rect viewRect)
        {
            var scrollViewRect = viewRect;
            scrollViewRect.height = scrollViewRect.height * 10f;
            scrollViewRect.width = scrollViewRect.width * 10f;
            return scrollViewRect;
        }

        public override void ProcessEvents(Event e)
        {
            var graph = _nodeWindow.CurrentGraph;
            if (graph != null)
            {
                graph.ProcessEvents(e, ViewRect);
            }

            base.ProcessEvents(e);
        }

        protected override void OnRightMouseDown(Event e)
        {
            var graph = _nodeWindow.CurrentGraph;

            bool overNode = false;
            if (graph != null)
            {
                foreach (var node in _nodeWindow.CurrentGraph.Nodes)
                {
                    if (node.Contains(e.mousePosition))
                    {
                        overNode = true;
                        _nodeContextMenuHandler.OpenMenu(e);
                        break;
                    }
                }
            }

            if(!overNode)
                base.OnRightMouseDown(e);
        }
    }
}
