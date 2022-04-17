using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus
{
    [Serializable]
    public class ContextMenuHandler
    {
        private readonly Dictionary<string, ContextMenuActionBase> _actions = new Dictionary<string, ContextMenuActionBase>();

        public void AddAction(ContextMenuActionBase action)
        {
            _actions.Add(action.Name, action);
        }

        public void SetMenuEnabled(string name, bool value)
        {
            ContextMenuActionBase action = null;
            if (_actions.TryGetValue(name, out action))
                action.Enabled = value;

        }

        public void OpenMenu(Event e)
        {
            var menu = new GenericMenu();
            foreach (var action in _actions.Values.Where(action => action.Enabled))
            {
                var a = action;
                menu.AddItem(new GUIContent(action.Name), action.On, () => a.Execute(e));
            }
                
            menu.ShowAsContext();
            e.Use();
        }
    }
}