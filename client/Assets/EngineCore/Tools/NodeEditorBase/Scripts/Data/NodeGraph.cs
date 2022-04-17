using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using NodeBase = VortexGames.EngineCore.Tools.NodeEditorBase.Data.Nodes.NodeBase;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Data
{
    public class NodeGraph : ScriptableObject
    {
        public string GraphName
        {
            get { return _graphName; }
            private set { _graphName = value; }
        }

        public IEnumerable<NodeBase> Nodes { get { return _nodes; } }

        [SerializeField]
        private string _graphName;
        [SerializeField]
        private List<NodeBase> _nodes;

        [SerializeField] private int _connectionsIdCounter;

        public int GetNewConnectionId
        {
            get
            {
                var result = _connectionsIdCounter;
                _connectionsIdCounter++;
                return result;
            }
        }

        [NonSerialized]
        public NodeBase.NodeOutput ConnectionReadyOutput;
        [NonSerialized]
        public NodeBase SelectedNode;
        [NonSerialized]
        public NodeBase LastContextNode;

        private void OnEnable()
        {
            if(_nodes == null)
                _nodes = new List<NodeBase>();
        }

        public void InitGraph(string name)
        {
            GraphName = name;

            foreach (var node in _nodes)
                node.InitNode(this);
        }

        public void AddNode(NodeBase node)
        {
            _nodes.Add(node);
        }

        public void RemoveNode(NodeBase node)
        {
            _nodes.Remove(node);
        }

        public void UpdateGraph()
        {
            
        }

#if UNITY_EDITOR
        public void UpdateGraphGui(Vector2 mousePos, Rect viewRect, GUISkin skin)
        {
            if (_nodes.Count == 0)
                return;

            foreach(var node in _nodes)
                node.UpdateNodeGui(viewRect, skin);

            DrawConnectionToMouse(mousePos);
            DrawInputLines();
        }

        private void DrawConnectionToMouse(Vector2 mousePos)
        {
            if (ConnectionReadyOutput != null)
            {
                Handles.BeginGUI();
                Handles.color = Color.green;
                Handles.DrawLine(ConnectionReadyOutput.Rect.center, mousePos);
                Handles.EndGUI();
            }
        }

        private void DrawInputLines()
        {
            Handles.BeginGUI();
            Handles.color = Color.white;

            foreach (var node in Nodes)
            {
                foreach (var output in node.Outputs)
                {
                    if (output.ConnectedInputId != -1)
                    {
                        var connectedInput = FindInputById(output.ConnectedInputId);
                        if (connectedInput != null)
                            Handles.DrawLine(output.Rect.center, connectedInput.Rect.center);
                    }
                       
                }
            }

            Handles.EndGUI();
        }
#endif
        public NodeBase.NodeOutput FindOutputById(int id)
        {
            return Nodes.SelectMany(_ => _.Outputs).FirstOrDefault(_ => _.Id.Equals(id));
        }

        public NodeBase.NodeInput FindInputById(int id)
        {
            return Nodes.SelectMany(_ => _.Inputs).FirstOrDefault(_ => _.Id.Equals(id));
        }

        public void ProcessEvents(Event e, Rect viewRect)
        {
            if (viewRect.Contains(e.mousePosition))
            {
                foreach (var node in _nodes)
                    node.ProcessEvents(e, viewRect);

                if (e.button == 0)
                {
                    if (e.type == EventType.MouseDown)
                    {
                        bool nodeClicked = false;

                        foreach(var node in Nodes)
                            if (node.Contains(e.mousePosition))
                            {
                                nodeClicked = true;
                                break;
                            }

                        if (!nodeClicked)
                        {
                            ConnectionReadyOutput = null;
                        }
                    }
                }
            }


        }

    }
}
