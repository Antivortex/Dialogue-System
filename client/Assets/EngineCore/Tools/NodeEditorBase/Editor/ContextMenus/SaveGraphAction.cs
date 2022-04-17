using UnityEngine;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus
{
    public class SaveGraphAction : ContextMenuActionBase
    {
        public SaveGraphAction()
            : base("Save Graph", false)
        {
        }

        public override void Execute(Event e)
        {
            NodeUtils.SaveGraph();
        }
    }
}