using UnityEditor;
using UnityEngine;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Windows.Popups
{
    public abstract class NodeEditorPopupWindowBase : EditorWindow
    {
        protected static NodeEditorPopupWindowBase CurrentPopup;

        protected abstract void DrawContent();
        private void OnGUI()
        {
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            GUILayout.BeginVertical();
            DrawContent();
            GUILayout.EndVertical();
            GUILayout.Space(20);
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
        }
        
        protected void OnCloseButton()
        {
            if(CurrentPopup != null)
                CurrentPopup.Close();
        }
    }
}
