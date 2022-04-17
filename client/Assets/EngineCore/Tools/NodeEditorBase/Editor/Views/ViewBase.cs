using System;
using UnityEngine;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.ContextMenus;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Views
{
    [Serializable]
    public class ViewBase
    {
        protected string ViewTitle { get; set; }
        protected Rect ViewRect { get; private set; }
        protected GUISkin ViewSkin { get; private set; }
        protected readonly ContextMenuHandler ContextMenuHandler = new ContextMenuHandler();

        public ViewBase(string title)
        {
            ViewTitle = title;
            GetEditorSkin();
        }

        public virtual void UpdateView(Vector2 mousePos, Rect editorRect, Rect percentageRect)
        {
            if (ViewSkin == null)
            {
                GetEditorSkin();
                return;
            }

            ViewRect = new Rect(editorRect.x * percentageRect.x, 
                editorRect.y * percentageRect.y, 
                editorRect.width * percentageRect.width,
                editorRect.height * percentageRect.height);


            var style = ViewSkin.GetStyle("ViewBG");
            GUI.Box(ViewRect, ViewTitle, style);
        }

        public virtual void ProcessEvents(Event e)
        {
            if (ViewRect.Contains(e.mousePosition))
            {
                switch (e.button)
                {
                    case 0: //left mouse
                    {
                        switch (e.type)
                        {
                            case EventType.MouseDown:
                            {
                                OnLeftMouseDown(e);
                                break;
                            }
                            case EventType.MouseUp:
                            {
                                OnLeftMouseUp(e);
                                break;
                            }
                            case EventType.MouseDrag:
                            {
                                OnLeftMouseDrag(e);
                                break;
                            }
                        }
                        break;
                    }
                    case 1: //right mouse
                    {
                        switch (e.type)
                        {
                            case EventType.MouseDown:
                            {
                                OnRightMouseDown(e);
                                break;
                            }
                            case EventType.MouseUp:
                            {
                                OnRightMouseUp(e);
                                break;
                            }
                            case EventType.MouseDrag:
                            {
                                OnRightMouseDrag(e);
                                break;
                            }
                        }
                        break;
                    }
                   
                }
            }
        }

        protected virtual void OnLeftMouseDown(Event e)
        {
        
        }

        protected virtual void OnLeftMouseUp(Event e)
        {
        
        }

        protected virtual void OnLeftMouseDrag(Event e)
        {
        
        }

        protected virtual void OnRightMouseDown(Event e)
        {
            ContextMenuHandler.OpenMenu(e);
        }

        protected virtual void OnRightMouseUp(Event e)
        {

        }

        protected virtual void OnRightMouseDrag(Event e)
        {

        }

        private void GetEditorSkin()
        {
            ViewSkin = Resources.Load<GUISkin>("GUISkins/EditorSkins/NodeEditorSkin");
        }
    }
}
