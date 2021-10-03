using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// 包管理，Package.asset所在目录下被标记的资源，打包为一个单独目录，可用于升级包
    /// </summary>
    [CreateAssetMenu]
    public class Package : ScriptableObject
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace;
    }
}
