using System;
using UnityEngine;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils;
using VortexGames.EngineCore.Tools.NodeEditorBase.Data;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus
{
    public class RemoveNodeAction : ContextMenuActionBase
    {
        private readonly Func<NodeGraph> _graphGetter;

        public RemoveNodeAction(Func<NodeGraph> graphGetter)
            : base("Remove Node", false)
        {
            _graphGetter = graphGetter;
        }

        public override void Execute(Event e)
        {
            var graph = _graphGetter();
            NodeUtils.RemoveContextNode(graph);
        }
    }
}