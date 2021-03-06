using IFramework.Core;

namespace IFramework.Editor
{
    /// <summary>
    /// 代码模板抽象类
    /// </summary>
    public abstract class AbstractTemplate<T> : ITemplate, ISingleton where T : AbstractTemplate<T>
    {
        protected GenerateInfo generateInfo;
        protected ElementInfo elementInfo;

        public static T Instance => SingletonProperty<T>.Instance;

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="generateInfo">代码生成信息</param>
        /// <param name="elementInfo"></param>
        /// <param name="overwrite"></param>
        public void Generate(GenerateInfo generateInfo, ElementInfo elementInfo = null, bool overwrite = false)
        {
            this.generateInfo = generateInfo;
            this.elementInfo = elementInfo;

            // 如果文件不能覆盖，并且存在，则退出
            if (!overwrite && FileUtils.Exists(FullName)) {
                return;
            }
            Log.Info("生成脚本: 正在生成脚本 " + FullName);

            // 创建文件夹，如果有则忽略
            DirectoryUtils.Create(generateInfo.ScriptAssetsPath);

            // 写入文件
            FileUtils.Write(FullName, BuildScript());
        }

        /// <summary> 
        /// 文件全名
        /// </summary>
        protected abstract string FullName { get; }

        /// <summary>
        /// 拼接字符串
        /// </summary>
        protected abstract string BuildScript();

        public virtual void OnInit() { }
    }
}
