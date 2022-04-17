using UnityEngine;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Windows.Popups;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus
{
    public class CreateGraphAction : ContextMenuActionBase
    {
        public CreateGraphAction() 
            : base("Create Graph", false)
        {
        }

        public override void Execute(Event e)
        {
            CreateGraphPopupWindow.InitPopup();
        }
    }
}