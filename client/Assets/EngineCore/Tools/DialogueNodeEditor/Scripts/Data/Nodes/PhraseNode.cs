
using UnityEngine;
using VortexGames.EngineCore.Tools.DialogueNodeEditor.Data.Nodes.NodeStructures;
using VortexGames.EngineCore.Tools.NodeEditorBase.Data;
using VortexGames.EngineCore.Tools.NodeEditorBase.Data.Nodes;
using VortexGames.EngineCore.Tools.NodeEditorBase.Utils;
using NodeBase = VortexGames.EngineCore.Tools.NodeEditorBase.Data.Nodes.NodeBase;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VortexGames.EngineCore.Tools.DialogueNodeEditor.Data.Nodes
{
    public class PhraseNode : NodeBase
    {
        [SerializeField]
        private NodeText _nodeText;

        public override void InitNode(NodeGraph graph)
        {
            base.InitNode(graph);
            NodeType = NodeType.Phrase;
            NodeRect = new Rect(10f, 10f, 150f, 65f);

            NodeInputs.Add(new NodeInput
            {
                Parent = this,
                Id = graph.GetNewConnectionId
            });
            NodeOutputs.Add(new NodeOutput
            {
                Parent =  this,
                Id = graph.GetNewConnectionId
            });
        }


#if UNITY_EDITOR

        private Vector2 _scrollPos;
        public override void DrawProperties(IPropertiesModifier modifier)
        {
            EditorGUILayout.BeginVertical();

            NodeGUIUtility.DrawChangeableLabel("Key:", _nodeText.Key, () =>
            {
                modifier.ChangeLabel(_nodeText.Key, s => _nodeText.Key = s);
            });
            NodeGUIUtility.DrawChangeableLabel("Json String:", _nodeText.JsonString, () =>
            {
                modifier.ChangeLabel(_nodeText.JsonString, s => _nodeText.JsonString = s);
            });
            NodeGUIUtility.DrawChangeableLabel("Phrase:", _nodeText.Text, () =>
            {
                modifier.ChangeLabel(_nodeText.Text, s => _nodeText.Text = s);
            });

            EditorGUILayout.EndVertical();
        }

#endif
    }
}