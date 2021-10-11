using UnityEngine;

namespace IFramework.Editor
{
    public delegate void ScriptKitCodeBind(GameObject uiPrefab, string filePath);
    
    public class ScriptGenInfo
    {
        // 热更新类型
        public int HotScriptType;
        
        // 热更新路径
        public string HotScriptFilePath;
        
        // 热更新代码前缀
        public string HotScriptSuffix;
        
        // 基础模板
        public IBaseTemplate[] Templates;
        
        // 代码绑定
        public ScriptKitCodeBind CodeBind;
    }
}
