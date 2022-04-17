using UnityEngine;
using VortexGames.DialogueNodeEditor.Editor.Views;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Windows;

namespace VortexGames.DialogueNodeEditor.Editor.Windows
{
    public class DialogueNodeEditorWindow : NodeEditorWindow
    {
        private static float _viewPercentage = 0.75f;

        private NodePropertyView _propertyView;
        private NodeWorkView _workView;

        public static void Init()
        {
            var window = GetWindow<DialogueNodeEditorWindow>();
            window.titleContent = new GUIContent("Node Editor");
            CreateViews(window);
            CurrentWindow = window;
        }

        private static void CreateViews(DialogueNodeEditorWindow window)
        {
            if (window != null)
            {
                window._propertyView = new NodePropertyView(window);
                window._workView = new NodeWorkView(window);
            }
            else
            {
                CurrentWindow = GetWindow<DialogueNodeEditorWindow>();
                CurrentWindow.titleContent  = new GUIContent("Node Editor");
            }
        }

        private bool AllViewsExist()
        {
            return _propertyView != null && _workView != null;
        }

        private void OnGUI()
        {
            if(CurrentGraph != null)
                CurrentGraph.UpdateGraph();

            if (AllViewsExist())
            {
                var e = Event.current;

                UpdateViews(e.mousePosition);
                ProcessEvents(e);
                ProcessViewEvents(e);
            }
            else
            {
                CreateViews(CurrentWindow as DialogueNodeEditorWindow);
                return;
            }

            Repaint();
        }

        private void UpdateViews(Vector2 mousePos)
        {
            var workPercentageRect = new Rect(0f, 0f, _viewPercentage, 1f);
            _workView.UpdateView(mousePos, position, workPercentageRect);

            var propertyEditorRect = new Rect(position.width, position.y, position.width, position.height);
            var propertyPercentageRect = new Rect(_viewPercentage, 0f, 1f - _viewPercentage, 1f);
            _propertyView.UpdateView(mousePos, propertyEditorRect, propertyPercentageRect);
        }

        private void ProcessEvents(Event e)
        {
            if (e.type == EventType.KeyDown)
            {
                switch (e.keyCode)
                {
                    case KeyCode.LeftArrow:
                        _viewPercentage -= 0.01f;
                        break;
                    case KeyCode.RightArrow:
                        _viewPercentage += 0.01f;
                        break;
                }
            }
        }

        private void ProcessViewEvents(Event e)
        {
            _workView.ProcessEvents(e);
            _propertyView.ProcessEvents(e);
        }

        private void OnEnable()
        {
        
        }

        private void OnDestroy()
        {
            NodeUtils.Uninit();
        }

        private void Update()
        {
        
        }

    

    }
}
