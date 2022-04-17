using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VortexGames.EngineCore.Tools.DialogueNodeEditor.Data;
using VortexGames.EngineCore.Tools.NodeEditorBase.Utils;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Data.Nodes
{
    public abstract partial class NodeBase : ScriptableObject
    {

        public IEnumerable<NodeBase.NodeInput> Inputs { get { return NodeInputs; } }
        public IEnumerable<NodeBase.NodeOutput> Outputs { get { return NodeOutputs; } }
        
        public Vector2 Pos
        {
            get { return NodeRect.position; }
            set
            {
                NodeRect.position = value;
            }
        }

        [SerializeField]
        protected List<NodeBase.NodeInput> NodeInputs = new List<NodeBase.NodeInput>();
        [SerializeField]
        protected List<NodeBase.NodeOutput> NodeOutputs = new List<NodeBase.NodeOutput>();
        [SerializeField]
        protected Rect NodeRect;
        [SerializeField]
        protected NodeType NodeType;
        [SerializeField]
        protected NodeGraph ParentGraph;
        
        private bool _selected;
        private bool _leftMouseDown;

        public bool Contains(Vector2 pos)
        {
            return NodeRect.Contains(pos);
        }

        public virtual void InitNode(NodeGraph graph)
        {
            ParentGraph = graph;
        }

        public virtual void UpdateNode(Event e, Rect viewRect)
        {
            
        }

#if UNITY_EDITOR
        public virtual void UpdateNodeGui(Rect viewRect, GUISkin skin)
        {
            var style = _selected ? skin.GetStyle("NodeSelected") : skin.GetStyle("NodeDefault");
            GUI.Box(NodeRect, NodeType.ToString(), style);
            UpdateInputPositions();
            UpdateOutputPositions();
            HandleInputButtons();
            HandleOutputButtons();
        }

        private void UpdateInputPositions()
        {
            var count = NodeInputs.Count;
            for (int i = 0; i < NodeInputs.Count; i++)
            {
                float offset = (count - 1) / 2f - i;
                NodeInputs[i].Rect = NodeGUIUtility.GetInputButtonRect(NodeRect, offset);
            }
        }

        private void UpdateOutputPositions()
        {
            var count = NodeOutputs.Count;
            for (int i = 0; i < NodeOutputs.Count; i++)
            {
                float offset = (count - 1) / 2f - i;
                NodeOutputs[i].Rect = NodeGUIUtility.GetOutputButtonRect(NodeRect, offset);
            }
        }

        private void HandleInputButtons()
        {
            for (int i = 0; i < NodeInputs.Count; i++)
            {
                var nodeInput = NodeInputs[i];
                var buttonRect = nodeInput.Rect;
                if (GUI.Button(buttonRect, ">"))
                {
                    if (ParentGraph != null)
                    {
                        var connectionReadyOutput = ParentGraph.ConnectionReadyOutput;

                        if (connectionReadyOutput != null)
                        {
                            if (nodeInput.ConnectedOutputId != -1)
                            {
                                var output = ParentGraph.FindOutputById(nodeInput.ConnectedOutputId);
                                if (output != null)
                                    output.ConnectedInputId = -1;
                            }
                           
                            nodeInput.ConnectedOutputId = connectionReadyOutput.Id;
                            connectionReadyOutput.ConnectedInputId = nodeInput.Id;
                            ParentGraph.ConnectionReadyOutput = null;
                        }
                    }
                }
            }
        }

        private void HandleOutputButtons()
        {
            for (int i = 0; i < NodeOutputs.Count; i++)
            {
                var nodeOutput = NodeOutputs[i];
                var buttonRect = nodeOutput.Rect;
                if (GUI.Button(buttonRect, ">"))
                {
                    if (ParentGraph != null)
                    {
                        ParentGraph.ConnectionReadyOutput = nodeOutput;
                    }
                }
            }
        }

        public virtual void DrawProperties(IPropertiesModifier modifier)
        {

        }
#endif

        public void ProcessEvents(Event e, Rect viewRect)
        {
            switch(e.button)
            {
                case 0:
                    {
                        switch (e.type)
                        {
                            case EventType.MouseDown:
                                {
                                    if (NodeRect.Contains(e.mousePosition))
                                        OnLeftMouseDown(e);
                                    break;
                                }
                            case EventType.MouseUp:
                                {
                                    OnLeftMouseUp(e);
                                    break;
                                }
                            case EventType.MouseDrag:
                                {
                                    OnLeftMouseDrag(e);
                                    break;
                                }
                        }

                        break;
                    }
                case 1:
                    {
                        switch (e.type)
                        {
                            case EventType.MouseDown:
                                {
                                    if (NodeRect.Contains(e.mousePosition))
                                        OnRightMouseDown(e);
                                    break;
                                }
                            case EventType.MouseUp:
                                {
                                    OnRightMouseUp(e);
                                    break;
                                }
                        }
                        break;
                    }
            }           
        }
        
        protected virtual void OnLeftMouseDown(Event e)
        {
            foreach (var node in ParentGraph.Nodes.Where(_ => !_.Equals(this)))
            {
                node._selected = false;
                node._leftMouseDown = false;
            }

            _selected = true;
            _leftMouseDown = true;
            ParentGraph.SelectedNode = this;
        }

        protected virtual void OnLeftMouseUp(Event e)
        {
            _leftMouseDown = false;
        }

        protected virtual void OnLeftMouseDrag(Event e)
        {
            if (_leftMouseDown)
            {
                NodeRect.x += e.delta.x;
                NodeRect.y += e.delta.y;
            }
        }

        protected virtual void OnRightMouseDown(Event e)
        {
            ParentGraph.LastContextNode = this;
        }

        protected virtual void OnRightMouseUp(Event e)
        {
          
        }

    }
}
