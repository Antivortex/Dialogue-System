using UnityEngine;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus
{
    public abstract class ContextMenuActionBase
    {
        public string Name { get; private set; }
        public bool On { get; private set; }
        public bool Enabled { get; set; }

        protected ContextMenuActionBase(string name, bool on)
        {
            Name = name;
            On = on;
            Enabled = true;
        }

        public abstract void Execute(Event e);


    }
}
