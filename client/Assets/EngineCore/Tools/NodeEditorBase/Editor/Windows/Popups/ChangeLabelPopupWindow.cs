using UnityEditor;
using UnityEngine;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Windows.Popups
{
    public class ChangeLabelPopupWindow : NodeEditorPopupWindowBase
    {
        private const string ChangeLabelString = "Change Label";
        private string _wantedLabel;
        private System.Action<string> _changeCallback;

        public static void InitPopup(string currentLabel, System.Action<string> changeCallback)
        {
            var popup = GetWindow<ChangeLabelPopupWindow>();
            popup.position = new Rect(500, 500, 800, 400);
            popup.titleContent = new GUIContent(ChangeLabelString);
            popup._wantedLabel = currentLabel;
            popup._changeCallback = changeCallback;
            CurrentPopup = popup;
        }

        protected override void DrawContent()
        {
            EditorGUILayout.LabelField(ChangeLabelString, EditorStyles.boldLabel);
            EditorStyles.textField.wordWrap = true;
            _wantedLabel = EditorGUILayout.TextField("Enter: ", _wantedLabel, GUILayout.Height(300));
            EditorStyles.textField.wordWrap = false;
            GUILayout.Space(10);
            DrawButtons();
        }

        private void DrawButtons()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Change", GUILayout.Height(40)))
                OnChangeButton();
            if (GUILayout.Button("Cancel", GUILayout.Height(40)))
                OnCloseButton();
            GUILayout.EndHorizontal();
        }

        private void OnChangeButton()
        {
            _changeCallback(_wantedLabel);
            OnCloseButton();
        }
    }
}