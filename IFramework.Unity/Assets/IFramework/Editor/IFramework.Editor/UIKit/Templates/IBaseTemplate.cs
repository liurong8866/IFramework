namespace IFramework.Editor
{
    public interface IBaseTemplate
    {
        void Generate(string generateFilePath, string behaviourName, string nameSpace, RootPanelInfo rootPanelInfo);
    }
}
