namespace IFramework.Editor
{
    public interface ITemplate
    {
        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="generateInfo">代码生成信息</param>
        /// <param name="elementInfo"></param>
        /// <param name="overwrite"></param>
        public void Generate(GenerateInfo generateInfo, ElementInfo elementInfo = null, bool overwrite = false);
    }
}
