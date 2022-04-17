using UnityEditor;
using VortexGames.DialogueNodeEditor.Editor.Windows;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils;

namespace VortexGames.DialogueNodeEditor.Editor.Menus
{
    public class NodeMenus
    {
        [MenuItem("Vortex Games/Node Editor/Launch Editor")]
        public static void InitNodeEditor()
        {

            NodeUtils.Init(new NodeUtilParams
            {
                GraphAssetsFolder = "NodeEditorAssets"
            });
            DialogueNodeEditorWindow.Init();
        }
    }
}
