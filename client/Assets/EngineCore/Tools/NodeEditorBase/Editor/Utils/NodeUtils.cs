using System.IO;
using UnityEditor;
using UnityEngine;
using VortexGames.EngineCore.Tools.DialogueNodeEditor.Data;
using VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Windows;
using VortexGames.EngineCore.Tools.NodeEditorBase.Data;

namespace VortexGames.EngineCore.Tools.NodeEditorBase.Editor.Utils
{
    public static class NodeUtils
    {
        private static NodeUtilParams _utilParams;

        public static void Init(NodeUtilParams utilParams)
        {
            _utilParams = utilParams;
        }

        public static void Uninit()
        {
            _utilParams = null;
        }

        public static void CreateNewGraph(string name)
        {
            if(_utilParams == null)
                Debug.LogErrorFormat("NodeUtils: params are not set");

            var newGraph = ScriptableObject.CreateInstance<NodeGraph>();

            if (newGraph == null)
            {
                EditorUtility.DisplayDialog("Node Message", "Unable to create new graph", "OK");
                return;
            }

            newGraph.InitGraph(name);

            if(!AssetDatabase.IsValidFolder("Assets/" + _utilParams.GraphAssetsFolder))
                AssetDatabase.CreateFolder("Assets", _utilParams.GraphAssetsFolder);

            AssetDatabase.CreateAsset(newGraph, "Assets/" + _utilParams.GraphAssetsFolder + "/" + name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (newGraph != null)
            {
                var editorWindow = EditorWindow.GetWindow<NodeEditorWindow>();
                if (editorWindow != null)
                    editorWindow.CurrentGraph = newGraph;
            }
        }

        public static void UnloadGraph()
        {
            if (_utilParams == null)
                Debug.LogErrorFormat("NodeUtils: params are not set");

            var editorWindow = EditorWindow.GetWindow<NodeEditorWindow>();

            if(editorWindow != null)
                editorWindow.CurrentGraph = null;
        }

        public static void SaveGraph()
        {
            if (_utilParams == null)
                Debug.LogErrorFormat("NodeUtils: params are not set");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void LoadGraph()
        {
            if (_utilParams == null)
                Debug.LogErrorFormat("NodeUtils: params are not set");

            var graphPath = EditorUtility.OpenFilePanel("Load Graph",
                Path.Combine(Application.dataPath, _utilParams.GraphAssetsFolder), string.Empty);

            var index = graphPath.IndexOf("Assets");
            if (index < 0)
                return;

            var finalPath = graphPath.Substring(index);

            var graph = AssetDatabase.LoadAssetAtPath<NodeGraph>(finalPath);

            if (graph != null)
            {
                var editorWindow = EditorWindow.GetWindow<NodeEditorWindow>();

                if (editorWindow != null)
                    editorWindow.CurrentGraph = graph;
            }
        }

        public static void CreateNode(NodeGraph graph, NodeType type, Vector2 mousePos)
        {
            if (_utilParams == null)
                Debug.LogErrorFormat("NodeUtils: params are not set");

            if (graph != null)
            {
                var newNode = NodeFactory.CreateNode(type);
                 
                if (newNode != null)
                {
                    newNode.InitNode(graph);
                    newNode.Pos = mousePos;
                    graph.AddNode(newNode);

                    AssetDatabase.AddObjectToAsset(newNode, graph);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }

        public static void RemoveContextNode(NodeGraph graph)
        {
            if (_utilParams == null)
                Debug.LogErrorFormat("NodeUtils: params are not set");

            var contextNode = graph.LastContextNode;
            graph.RemoveNode(contextNode);
            Object.DestroyImmediate(graph.LastContextNode, true);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
