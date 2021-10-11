namespace IFramework.Editor
{
    public interface ITemplate
    {
        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="generateInfo">代码生成信息</param>
        /// <param name="rootNodeInfo">面板代码信息</param>
        public void Generate(GenerateInfo generateInfo, RootNodeInfo rootNodeInfo = null, bool overwrite = false);
    }
}
