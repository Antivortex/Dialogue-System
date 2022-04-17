using System.Collections.Generic;
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
    public class ChoiceNode : NodeBase
    {
        [SerializeField]
        private List<NodeText> _textList = new List<NodeText>();
        
        public override void InitNode(NodeGraph graph)
        {
            base.InitNode(graph);
            NodeType = NodeType.Choice;
            NodeRect = new Rect(10f, 10f, 150f, 65f);

            NodeInputs.Add(new NodeInput
            {
                Parent = this,
                Id = graph.GetNewConnectionId
            });
        }

#if UNITY_EDITOR

        public override void DrawProperties(IPropertiesModifier modifier)
        {
            EditorGUILayout.BeginVertical();

            for (int i = 0; i < _textList.Count; i++)
            {
                var nodeText = _textList[i];

                NodeGUIUtility.DrawChangeableLabel(i + " key:", nodeText.Key,
                    () =>
                    {
                        modifier.ChangeLabel(nodeText.Key, s => nodeText.Key = s);
                    },
                    () =>
                    {
                        _textList.RemoveAt(i);
                        NodeOutputs.RemoveAt(i);
                        i--;
                    }
                );

                NodeGUIUtility.DrawChangeableLabel(i + " json string:", nodeText.JsonString,
                    () =>
                    {
                        modifier.ChangeLabel(nodeText.JsonString, s => nodeText.JsonString = s);
                    }
                );

                NodeGUIUtility.DrawChangeableLabel(i+":", nodeText.Text, 
                    () =>
                    {
                        modifier.ChangeLabel(nodeText.Text, s => nodeText.Text = s);
                    }
               );
            }

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add"))
            {
                _textList.Add(new NodeText());

                NodeOutputs.Add(new NodeOutput
                {
                    Parent = this,
                    Id = ParentGraph.GetNewConnectionId
                });
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();


        }
#endif
    }
}