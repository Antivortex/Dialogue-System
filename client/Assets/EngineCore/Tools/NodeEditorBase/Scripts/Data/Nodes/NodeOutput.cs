using System;
using UnityEngine;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Data.Nodes
{
    public abstract partial class NodeBase
    {
        [Serializable]
        public class NodeOutput 
        {
            public EngineCore.Tools.NodeEditorBase.Data.Nodes.NodeBase Parent;
            public int Id = -1;
            public int ConnectedInputId = -1;
            public Rect Rect;

        }
    }
}