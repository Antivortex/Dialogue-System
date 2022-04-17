using UnityEngine;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus
{
    public class LoadGraphAction : ContextMenuActionBase
    {
        public LoadGraphAction() 
            : base("Load Graph", false)
        {
        }

        public override void Execute(Event e)
        {
            NodeUtils.LoadGraph();
        }
    }
}