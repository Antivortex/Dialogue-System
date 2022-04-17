using UnityEditor;
using VortexGames.EngineCore.Tools.NodeEditorBase.Data;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Windows
{
    public abstract class NodeEditorWindow : EditorWindow, INodeWindow
    {
        protected static NodeEditorWindow CurrentWindow;
        public NodeGraph CurrentGraph { get; set; }

    }
}