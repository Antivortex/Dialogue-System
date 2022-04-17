using System;
using UnityEngine;
using VortexGames.EngineCore.Tools.DialogueNodeEditor.Data.Nodes;
using NodeBase = VortexGames.EngineCore.Tools.NodeEditorBase.Data.Nodes.NodeBase;

namespace VortexGames.EngineCore.Tools.DialogueNodeEditor.Data
{
    public static class NodeFactory
    {
        public static NodeBase CreateNode(NodeType type)
        {
            switch (type)
            {
                case NodeType.Phrase:
                {
                    var result = ScriptableObject.CreateInstance<PhraseNode>();
                    result.name = type.ToString();
                    return result;
                }
                case NodeType.Choice:
                {
                   var result = ScriptableObject.CreateInstance<ChoiceNode>();
                    result.name = type.ToString();
                    return result;
                }
                default:
                {
                    throw new ArgumentException(string.Format("NodeType {0} not found in node factory", type));
                }
            }

           
        }
    }
}