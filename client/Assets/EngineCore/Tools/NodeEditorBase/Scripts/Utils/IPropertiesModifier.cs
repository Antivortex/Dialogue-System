namespace VortexGames.EngineCore.Tools.NodeEditorBase.Utils
{
    public interface IPropertiesModifier
    {
        void ChangeLabel(string currentLabel, System.Action<string> changeCallback);
    }
}