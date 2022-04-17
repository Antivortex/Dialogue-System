using UnityEditor;
using UnityEngine;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Windows.Popups
{
    public class CreateGraphPopupWindow : NodeEditorPopupWindowBase
    {
        private const string CreateNewGraphString = "Create New Graph";
        private const string InitialName = "Enter a name...";
        private string _wantedName = InitialName;

        public static void InitPopup()
        {
            CurrentPopup = GetWindow<CreateGraphPopupWindow>();
            CurrentPopup.titleContent = new GUIContent(CreateNewGraphString);
        }

        protected override void DrawContent()
        {
            EditorGUILayout.LabelField(CreateNewGraphString, EditorStyles.boldLabel);
            _wantedName = EditorGUILayout.TextField("Enter Name: ", _wantedName);
            GUILayout.Space(10);
            DrawButtons();
        }

        private void DrawButtons()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Graph", GUILayout.Height(40)))
                OnCreateGraphButton();
            if (GUILayout.Button("Cancel", GUILayout.Height(40)))
                OnCloseButton();
            GUILayout.EndHorizontal();
        }

        private void OnCreateGraphButton()
        {
            if (string.IsNullOrEmpty(_wantedName) || _wantedName == InitialName)
            {
                EditorUtility.DisplayDialog("Node Message:", "Please enter a valid graph name!", "OK");
            }
            else
            {
                NodeUtils.CreateNewGraph(_wantedName);
            }
        }

    }
}