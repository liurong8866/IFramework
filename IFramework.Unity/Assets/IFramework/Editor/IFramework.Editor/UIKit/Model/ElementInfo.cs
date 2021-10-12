using System.Collections.Generic;

namespace IFramework.Editor
{
    public class ElementInfo
    {
        public string GameObjectName;

        public BindInfo BindInfo;

        public Dictionary<string, string> NameToFullNameDic = new Dictionary<string, string>();

        public readonly List<BindInfo> BindInfoList = new List<BindInfo>();

        public readonly List<ElementInfo> ElementInfoList = new List<ElementInfo>();
    }
    
    /// <summary>
    /// 用于记录根节点的信息
    /// </summary>
    public class RootNodeInfo : ElementInfo
    {
        public string Identifier { get; set; }

        public bool Changed { get; set; }

        public IEnumerable<string> ForeignKeys { get; private set; }
    }

    /// <summary>
    /// 用于记录根ViewController的信息
    /// </summary>
    public class RootViewControllerInfo : RootNodeInfo { }

    /// <summary>
    /// 用于记录根Panel的信息
    /// </summary>
    public class RootPanelInfo : RootNodeInfo { }
}
