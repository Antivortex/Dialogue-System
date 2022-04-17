using UnityEngine;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus
{
    public class UnloadGraphAction : ContextMenuActionBase
    {
        public UnloadGraphAction()
            : base("Unload Graph", false)
        {
        }

        public override void Execute(Event e)
        {
            NodeUtils.UnloadGraph();
        }
    }
}