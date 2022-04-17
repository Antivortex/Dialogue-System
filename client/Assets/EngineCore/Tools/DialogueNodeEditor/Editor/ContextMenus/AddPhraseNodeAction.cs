using System;
using UnityEngine;
using VortexGames.EngineCore.Tools.DialogueNodeEditor.Data;
using VortexGames.EngineCore.Tools.NodeEditorBase.Data;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils;

namespace VortexGames.EngineCore.Tools.DialogueNodeEditor.Editor.ContextMenus
{
    public class AddPhraseNodeAction : ContextMenuActionBase
    {
        private readonly Func<NodeGraph> _graphGetter;

        public AddPhraseNodeAction(Func<NodeGraph> graphGetter)
            : base("Add Phrase Node", false)
        {
            _graphGetter = graphGetter;
        }

        public override void Execute(Event e)
        {
            var graph = _graphGetter();
           NodeUtils.CreateNode(graph, NodeType.Phrase, e.mousePosition);
        }
    }
}