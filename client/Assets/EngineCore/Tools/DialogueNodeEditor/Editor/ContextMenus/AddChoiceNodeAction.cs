using System;
using UnityEngine;
using VortexGames.EngineCore.Tools.DialogueNodeEditor.Data;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils;
using VortexGames.EngineCore.Tools.NodeEditorBase.Data;

namespace VortexGames.DialogueNodeEditor.Editor.ContextMenus
{
    public class AddChoiceNodeAction : ContextMenuActionBase
    {
        private readonly Func<NodeGraph> _graphGetter;

        public AddChoiceNodeAction(Func<NodeGraph> graphGetter)
            : base("Add Choice Node", false)
        {
            _graphGetter = graphGetter;
        }

        public override void Execute(Event e)
        {
            var graph = _graphGetter();
            NodeUtils.CreateNode(graph, NodeType.Choice, e.mousePosition);
        }
    }
}